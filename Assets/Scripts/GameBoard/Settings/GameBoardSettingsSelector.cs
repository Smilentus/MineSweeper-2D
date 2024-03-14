using UnityEngine;
using UnityEngine.SceneManagement;


public class GameBoardSettingsSelector : MonoBehaviour
{
    private static GameBoardSettingsSelector _instance;
    public static GameBoardSettingsSelector Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameBoardSettingsSelector>(); 
            }

            return _instance;
        }
    }


    [SerializeField]
    private int m_gameSceneIndex;

    [SerializeField]
    private GameBoardLoadableSettings m_loadableSettings;


    [SerializeField]
    private GameBoardSettings[] m_predefinedSettings;
    public GameBoardSettings[] PredefinedSettings => m_predefinedSettings;


    public void SelectSettings(int index)
    {
        index = Mathf.Clamp(index, 0, m_predefinedSettings.Length);

        m_loadableSettings.gameBoardSettings = m_predefinedSettings[index];
    }

    public void SetSettings(GameBoardSettings settings)
    {
        m_loadableSettings.gameBoardSettings = settings;
    }

    
    public void LoadGameScene()
    {
        SceneManager.LoadScene(m_gameSceneIndex);
    }
}

[System.Serializable]
public struct GameBoardSettings
{
    public string DifficultyTitle;
    public int MapWidth;
    public int MapHeight;
    public int Mines;
}