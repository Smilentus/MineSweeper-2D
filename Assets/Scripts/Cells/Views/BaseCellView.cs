using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BaseCellView : MonoBehaviour
{
    [SerializeField]
    protected Image m_cellImage;


    protected BaseCell _baseCell;
    public BaseCell BaseCellRef => _baseCell;


    private void OnDestroy()
    {
        _baseCell.onCellFlaged -= AdaptFlagEvent;
        _baseCell.onCellRevealed -= AdaptRevealEvent;
        _baseCell.onFlagBroken -= OnFlagBroken;
    }


    public void InjectCellData(BaseCell baseCell)
    {
        _baseCell = baseCell;

        _baseCell.onCellFlaged += AdaptFlagEvent;
        _baseCell.onCellRevealed += AdaptRevealEvent;
        _baseCell.onFlagBroken += OnFlagBroken;

        OnInject();
    }
    public virtual void OnInject() { }


    private void AdaptFlagEvent(bool isFlagged)
    {
        if (isFlagged)
        {
            DrawFlaggedCell();
        }
        else
        {
            DrawClosedCell();
        }
    }
    private void AdaptRevealEvent(bool isRevealed)
    {
        DrawRevealedCell();
    }


    private void OnFlagBroken()
    {
        m_cellImage.sprite = GameBoardAppearanceHandler.Instance.Appearance.m_brokenFlagCell;
    }


    public virtual void DrawClosedCell()
    {
        m_cellImage.sprite = GameBoardAppearanceHandler.Instance.Appearance.m_closedCell;
    }

    public virtual void DrawFlaggedCell()
    {
        m_cellImage.sprite = GameBoardAppearanceHandler.Instance.Appearance.m_flagCell;
    }

    public virtual void DrawRevealedCell()
    {
        m_cellImage.sprite = GameBoardAppearanceHandler.Instance.Appearance.m_emptyCell;
    }
}
