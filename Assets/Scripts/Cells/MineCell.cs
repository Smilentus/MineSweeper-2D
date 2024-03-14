public class MineCell : BaseCell
{
    protected bool _isExploded;
    public bool IsExploded => _isExploded;


    public override void OnRevealCell()
    {
        _isExploded = true;

        _gameBoard.ExplodeBoard();
    }
}