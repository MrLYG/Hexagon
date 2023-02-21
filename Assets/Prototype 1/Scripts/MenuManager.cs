using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Start()
    {
        string uniqueID = System.Guid.NewGuid().ToString();
        PlayerPrefs.SetString("UUID", uniqueID);
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
