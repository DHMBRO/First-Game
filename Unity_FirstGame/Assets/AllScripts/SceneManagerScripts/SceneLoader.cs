using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader :  MonoBehaviour
{
    public void LoadScene(int NumberOfScene)
    {
        SceneManager.LoadScene(NumberOfScene);
        Time.timeScale = 1.0f;
    }

    public void QuitFromGame()
    {
        Application.Quit();
    }

    public void LoadSceneStartGameScene()
    {
        LoadScene(0);
    }



}
