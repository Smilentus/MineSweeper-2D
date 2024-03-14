using UnityEngine;


public class GameBoardAppearanceHandler : MonoBehaviour
{
    private static GameBoardAppearanceHandler _instance;
    public static GameBoardAppearanceHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameBoardAppearanceHandler>(true);
            }

            return _instance;
        }
    }


    [field: SerializeField]
    public GameBoardAppearance Appearance { get; private set; }
}
