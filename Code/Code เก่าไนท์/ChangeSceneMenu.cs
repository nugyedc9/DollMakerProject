using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneMenu : MonoBehaviour
{
    public string ScenceName;

    private void Update()
    {
        SceneManager.LoadScene(ScenceName);
    }
}
