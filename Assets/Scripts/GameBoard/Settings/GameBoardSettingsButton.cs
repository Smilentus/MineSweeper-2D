using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GameBoardSettingsButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TMP_Text m_labelText;


    [field: SerializeField]
    public int SettingsIndex { get; private set; }


    private void Awake()
    {
        if (m_labelText != null)
        {
            GameBoardSettings settings = GameBoardSettingsSelector.Instance.PredefinedSettings[SettingsIndex];

            m_labelText.text = $"{settings.DifficultyTitle}\n\nПоле {settings.MapWidth}x{settings.MapHeight}";
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        GameBoardSettingsSelector.Instance.SelectSettings(SettingsIndex);
        GameBoardSettingsSelector.Instance.LoadGameScene();
    }
}
