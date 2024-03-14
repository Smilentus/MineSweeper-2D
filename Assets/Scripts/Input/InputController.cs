using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InputController : MonoBehaviour
{
    [SerializeField]
    private GameBoard m_gameBoard;


    private void Update()
    {
        if (m_gameBoard == null || m_gameBoard.IsGameOver) return;

        if (Input.GetMouseButtonDown(0))
        {
            BaseCell baseCell = RaycastToCell();

            if (baseCell != null)
            {
                m_gameBoard.CheckFirstInteraction();
                m_gameBoard.RevealCell(baseCell);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            BaseCell baseCell = RaycastToCell();

            if (baseCell != null)
            {
                m_gameBoard.CheckFirstInteraction();
                m_gameBoard.ToggleFlagAtCell(baseCell);
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            BaseCell baseCell = RaycastToCell();

            if (baseCell != null)
            {
                m_gameBoard.CheckFirstInteraction();
                m_gameBoard.RevealAllCellsAroundCell(baseCell);
            }
        }
    }


    private BaseCell RaycastToCell()
    {
        BaseCell raycastedCell = null;

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        foreach (RaycastResult raycast in raycastResults)
        {
            if (raycast.gameObject != null)
            {
                if(raycast.gameObject.TryGetComponent(out BaseCellView baseCellView))
                {
                    raycastedCell = baseCellView.BaseCellRef;
                }
            }
        }

        return raycastedCell;
    }
}