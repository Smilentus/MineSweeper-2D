using UnityEngine;


public class GameBoard : MonoBehaviour
{
    public event System.Action onMapUpdated;
    public event System.Action onExploded;
    public event System.Action onWinGame;


    private BaseCell[,] cellMap;
    public BaseCell[,] CellMap => cellMap;


    public int MapWidth { get; private set; }
    public int MapHeight { get; private set; }

    public int MapSize => MapWidth * MapHeight;

    public int Mines { get; private set; }


    private bool _isGameOver;
    public bool IsGameOver => _isGameOver;


    private int _revealedFields;
    public int RevealedFields => _revealedFields;

    private int _needToRevealFields;


    private bool _isFirstCheck;


    private void OnDestroy()
    {
        cellMap = null;
    }


    public void SetupMap(int width, int height, int mines)
    {
        _isFirstCheck = true;
        _isGameOver = false;

        MapWidth = width;
        MapHeight = height;
        Mines = mines;

        cellMap = new BaseCell[MapWidth, MapHeight];
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                cellMap[x, y] = new NumberCell();
                cellMap[x, y].Position = new Vector3Int(x, y, 0);
                cellMap[x, y].SetGameBoard(this);
            }
        }

        _revealedFields = 0;

        if (Mines >= MapSize) Mines = MapSize - 1;
        _needToRevealFields = MapSize - Mines;

        onMapUpdated?.Invoke();
    }


    private void GenerateMines(BaseCell exceptCell = null)
    {
        for (int i = 0; i < Mines; i++)
        {
            int safer = 0; // На всякий случай, не люблю без защиты :(

            int x = UnityEngine.Random.Range(0, MapWidth);
            int y = UnityEngine.Random.Range(0, MapHeight);

            while (cellMap[x, y].GetType().Equals(typeof(MineCell)))
            {
                x++;

                if (x >= MapWidth)
                {
                    x = 0;
                    y++;

                    if (y >= MapHeight)
                    {
                        y = 0;
                        safer++;
                    }
                }

                if (exceptCell != null)
                {
                    if (x == exceptCell.Position.x && y == exceptCell.Position.y)
                    {
                        x = UnityEngine.Random.Range(0, MapWidth);
                        y = UnityEngine.Random.Range(0, MapHeight);
                    }
                }

                if (safer >= 100) { break; }
            }

            cellMap[x, y] = new MineCell();
            cellMap[x, y].Position = new Vector3Int(x, y, 0);
            cellMap[x, y].SetGameBoard(this);
        }

        onMapUpdated?.Invoke();
    }

    private void SetupCells()
    {
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                cellMap[x, y].Setup();
            }
        }
    }

    public void ExplodeBoard()
    {
        _isGameOver = true;

        GameController.Instance.GameTimerRef.StopTimer();

        Debug.Log($"Мы взорвались =(");

        RevealAllMines(true);
        BrokeWrongFlags();

        onExploded?.Invoke();
    }
    private void WinGame()
    {
        _isGameOver = true;

        GameController.Instance.GameTimerRef.StopTimer();

        Debug.Log("ТЫ ПОБЕДИЛ! (не верю)");

        FlagAllMines();

        onWinGame?.Invoke();
    }


    public void CheckFirstInteraction()
    {
        if (_isFirstCheck)
        {
            _isFirstCheck = false;

            GenerateMines();
            SetupCells();
        }
    }

    public void RevealCell(BaseCell baseCell)
    {
        baseCell.RevealCell();

        if (_isGameOver) return;

        if (_revealedFields >= _needToRevealFields)
        {
            WinGame();
        }
    }

    public void ToggleFlagAtCell(BaseCell baseCell)
    {
        baseCell.ToggleFlag();
    }

    public void AddRevealedFieldCounter()
    {
        _revealedFields++;
    }


    private void FlagAllMines()
    {
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                CellMap[x, y].FlagCell();
            }
        }
    }

    private void BrokeWrongFlags()
    {
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                if (CellMap[x, y] is not MineCell)
                {
                    if (CellMap[x, y].IsFlagged)
                    {
                        CellMap[x, y].BrokeFlag();
                    }
                }
            }
        }
    }

    private void RevealAllMines(bool exceptExploded = false)
    {
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                if (CellMap[x, y] is MineCell)
                {
                    MineCell mineCell = CellMap[x, y] as MineCell;

                    if (exceptExploded && !mineCell.IsExploded)
                    {
                        CellMap[x, y].HiddenRevealCell();
                    }
                }
            }
        }
    }


    public bool IsCellInBounds(int x, int y)
    {
        return x >= 0 && x < MapWidth && y >= 0 && y < MapHeight;
    }

    public BaseCell GetCellAtPosition(int x, int y)
    {
        return cellMap[x, y];
    }

    public void RevealAllCellsAroundCell(BaseCell baseCell, bool includeItself = true)
    {
        for (int adX = -1; adX <= 1; adX++)
        {
            for (int adY = -1; adY <= 1; adY++)
            {
                if (!includeItself && (adX == 0 && adY == 0)) continue;

                int x = baseCell.Position.x + adX;
                int y = baseCell.Position.y + adY;

                if (!IsCellInBounds(x, y)) continue;

                if (!CellMap[x, y].IsRevealed && !CellMap[x, y].IsFlagged)
                {
                    CellMap[x, y].RevealCell();
                }
            }
        }
    }

    public void RevealAroundCellsByFlooding(BaseCell baseCell)
    {
        for (int adX = -1; adX <= 1; adX++)
        {
            for (int adY = -1; adY <= 1; adY++)
            {
                if (adX == 0 && adY == 0) continue;

                int x = baseCell.Position.x + adX;
                int y = baseCell.Position.y + adY;

                if (!IsCellInBounds(x, y)) continue;

                if (!CellMap[x, y].IsRevealed && !CellMap[x, y].IsFlagged && CellMap[x, y] is not MineCell)
                {
                    CellMap[x, y].RevealCell();
                }
            }
        }
    }

    public int CalculateMinesAroundCell(BaseCell baseCell)
    {
        int count = 0;

        for (int adX = -1; adX <= 1; adX++)
        {
            for (int adY = -1; adY <= 1; adY++)
            {
                if (adX == 0 && adY == 0) continue;

                int x = baseCell.Position.x + adX;
                int y = baseCell.Position.y + adY;

                if (!IsCellInBounds(x, y)) continue;

                if (cellMap[x, y] is MineCell)
                {
                    count++;
                }
            }
        }

        return count;
    }
}