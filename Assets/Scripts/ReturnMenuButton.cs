using UnityEngine;
using UnityEngine.SceneManagement;


public class ReturnMenuButton : MonoBehaviour
{
    [SerializeField]
    private int m_menuSceneIndex;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(m_menuSceneIndex);
        }
    }
}
