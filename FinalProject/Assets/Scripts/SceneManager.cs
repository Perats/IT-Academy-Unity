using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void OpenGameScene(int level)
    {
        PlayerPrefs.SetInt("Level", level);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
  
    public void OpenNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
