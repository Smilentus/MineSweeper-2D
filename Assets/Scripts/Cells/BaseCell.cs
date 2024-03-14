using System;
using UnityEngine;

public class BaseCell
{
    public event Action<bool> onCellRevealed;
    public event Action<bool> onCellFlaged;
    public event Action onFlagBroken;


    public Vector3Int Position { get; set; }

    public bool IsFlagged { get; protected set; }
    public bool IsRevealed { get; protected set; }


    protected GameBoard _gameBoard;


    public virtual bool IsEmpty() => false;


    public void SetGameBoard(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
    }


    public void Setup() 
    {
        OnSetup();
    }
    public virtual void OnSetup() { }


    public void ToggleFlag()
    {
        if (IsFlagged)
        {
            UnFlagCell();
        }
        else
        {
            FlagCell();
        }
    }

    public void FlagCell() 
    {
        if (IsRevealed) return;

        IsFlagged = true;

        OnFlagCell();

        onCellFlaged?.Invoke(IsFlagged);
    }
    public virtual void OnFlagCell() { }

    public void UnFlagCell()
    {
        if (IsRevealed) return;

        IsFlagged = false;

        OnUnFlagCell();

        onCellFlaged?.Invoke(IsFlagged);
    }
    public virtual void OnUnFlagCell() { }

    public void BrokeFlag()
    {
        OnBrokeFlag();

        onFlagBroken?.Invoke();
    }
    public virtual void OnBrokeFlag() { }

    public void RevealCell()
    {
        if (IsFlagged || IsRevealed) return;

        IsRevealed = true;

        _gameBoard.AddRevealedFieldCounter();

        OnRevealCell();

        onCellRevealed?.Invoke(IsRevealed);
    }
    public virtual void OnRevealCell() { }


    public void HiddenRevealCell()
    {
        onCellRevealed?.Invoke(true);
    }
}
