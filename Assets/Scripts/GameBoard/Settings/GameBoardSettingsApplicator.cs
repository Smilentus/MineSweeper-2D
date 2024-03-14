using UnityEngine;


public class GameBoardSettingsApplicator : MonoBehaviour
{
    [SerializeField]
    private GameBoardLoadableSettings m_loadableSettings;

    public void ApplyLoadableSettings()
    {
        GameController.Instance.GameBoardRef.SetupMap(
            m_loadableSettings.gameBoardSettings.MapWidth,
            m_loadableSettings.gameBoardSettings.MapHeight,
            m_loadableSettings.gameBoardSettings.Mines
        );
    }
}
