using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public int gameLevelScene;

    public void StartGame()
    {
        SceneManager.LoadScene(gameLevelScene);
    }
}
