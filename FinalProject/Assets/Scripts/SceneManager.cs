using UnityEngine;

public class SceneManager : MonoBehaviour
{
    GameManager manager;
    private void Awake()
    {
        manager = GetComponent<GameManager>();
    }
    public void OpenGameScene(int level)
    {
        PlayerPrefs.SetInt("Level", level);
      //  gameManager.SelectLevel(level);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

  
    public void OpenNextScene()
    {
       // manager.SelectLevel(currentLevel);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
