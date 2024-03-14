public class MineCellView : BaseCellView
{
    public override void DrawRevealedCell()
    {
        if ((_baseCell as MineCell).IsExploded)
        {
            m_cellImage.sprite = GameBoardAppearanceHandler.Instance.Appearance.m_explodedMine;
        }
        else
        {
            m_cellImage.sprite = GameBoardAppearanceHandler.Instance.Appearance.m_revealedMine;
        }
    }
}
