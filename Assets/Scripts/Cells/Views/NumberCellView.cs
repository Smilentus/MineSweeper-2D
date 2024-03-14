using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NumberCellView : BaseCellView
{
    public NumberCell NumberCellRef => _baseCell as NumberCell;


    public override void DrawRevealedCell()
    {
        if (NumberCellRef.MinesAround == 0)
        {
            base.DrawRevealedCell();
        }
        else
        {
            m_cellImage.sprite = GameBoardAppearanceHandler.Instance.Appearance.m_numberTiles[NumberCellRef.MinesAround - 1];
        }
    }
}
