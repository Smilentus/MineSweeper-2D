public class NumberCell : BaseCell
{
    public int MinesAround { get; private set; }


    public override bool IsEmpty()
    {
        return MinesAround == 0;
    }

    public override void OnSetup()
    {
        MinesAround = _gameBoard.CalculateMinesAroundCell(this);
    }

    public override void OnRevealCell()
    {
        if (IsEmpty())
        {
            _gameBoard.RevealAroundCellsByFlooding(this);
        }
    }
}