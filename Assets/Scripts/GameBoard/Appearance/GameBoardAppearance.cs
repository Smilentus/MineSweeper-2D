using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameBoardAppearance", menuName = "GameBoard/Appearance")]
public class GameBoardAppearance : ScriptableObject
{
    public Sprite m_emptyCell;
    public Sprite m_closedCell;

    public Sprite m_flagCell;
    public Sprite m_brokenFlagCell;

    public Sprite m_revealedMine;
    public Sprite m_explodedMine;

    public Sprite[] m_numberTiles;

    public Sprite GetNumberTile(int rawIndex) => m_numberTiles[
        Mathf.Clamp(
            rawIndex - 1, 
            0, m_numberTiles.Length - 1
        )];
}
