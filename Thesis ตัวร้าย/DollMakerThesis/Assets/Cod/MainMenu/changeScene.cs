using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadSceneAsync("Scene1");
    }
    public void LoadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Scene1");
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}
