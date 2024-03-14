using UnityEngine;
using UnityEngine.UI;


public class GameBoardPresenter : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup m_gridLayoutGroup;


    // ToDo: Refactoring
    [SerializeField]
    private NumberCellView m_numberCellViewPrefab;

    [SerializeField]
    private BaseCellView m_mineCellViewPrefab;


    private GameBoard _gameBoard;


    private void Awake()
    {
        _gameBoard = GameController.Instance.GameBoardRef;

        _gameBoard.onMapUpdated += OnMapSetup;
    }

    private void OnDestroy()
    {
        _gameBoard.onMapUpdated -= OnMapSetup;
    }


    private void OnMapSetup()
    {
        ClearMap();


        m_gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        m_gridLayoutGroup.constraintCount = _gameBoard.MapWidth;


        for (int y = 0; y < _gameBoard.MapHeight; y++)
        {
            for (int x = 0; x < _gameBoard.MapWidth; x++)
            {
                BaseCellView baseCellView = null;

                if (_gameBoard.CellMap[x, y] is NumberCell)
                {
                    baseCellView = Instantiate(m_numberCellViewPrefab, m_gridLayoutGroup.transform);
                }
                else if (_gameBoard.CellMap[x, y] is MineCell)
                {
                    baseCellView = Instantiate(m_mineCellViewPrefab, m_gridLayoutGroup.transform);
                }

                if (baseCellView != null)
                {
                    baseCellView.InjectCellData(_gameBoard.CellMap[x, y]);

                    if (_gameBoard.CellMap[x, y].IsFlagged)
                    {
                        baseCellView.DrawFlaggedCell();
                    }
                    else if (_gameBoard.CellMap[x, y].IsRevealed)
                    {
                        baseCellView.DrawRevealedCell();
                    }
                    else
                    {
                        baseCellView.DrawClosedCell();
                    }
                }

                baseCellView.name = $"Cell_x:{x}_y:{y}";
            }
        }
    }

    private void ClearMap()
    {
        for (int i = m_gridLayoutGroup.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(m_gridLayoutGroup.transform.GetChild(i).gameObject);
        }
    }
}
