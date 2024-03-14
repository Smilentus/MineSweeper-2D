using UnityEngine;


public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController>();
            }

            return _instance;
        }
    }


    [field: SerializeField]
    public GameBoard GameBoardRef { get; set; }

    [field: SerializeField]
    public GameTimer GameTimerRef { get; set; }

    [field: SerializeField]
    public GameBoardSettingsApplicator GameBoardSettingsApplicatorRef { get; set; }



    private void Start()
    {
        StartNewGame();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartNewGame();
        }
    }


    public void StartNewGame()
    {
        GameBoardSettingsApplicatorRef.ApplyLoadableSettings();
        GameTimerRef.StartTimer();
    }
}
