using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public int sceneNum;

    public void playGame()
    {
        SceneManager.LoadSceneAsync("In House Scene");
    }
    public void LoadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("In House Scene");
    }
    public void CutScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Cutscene Prolouge");
    }

    public void NextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNum);
    }

    public void LoadScenename(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void Quitgame()
    {
        Application.Quit();
    }
}
