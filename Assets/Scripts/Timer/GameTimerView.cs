using TMPro;
using UnityEngine;


public class GameTimerView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_timerText;


    private GameTimer _gameTimer;


    private void Awake()
    {
        _gameTimer = GameController.Instance.GameTimerRef;
    }

    private void Update()
    {
        if (m_timerText != null && _gameTimer != null)
        {
            m_timerText.text = _gameTimer.CurrentTime.ToString(@"hh\:mm\:ss");
        }
    }
}
