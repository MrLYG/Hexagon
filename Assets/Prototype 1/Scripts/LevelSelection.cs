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
        switch (gameLevelScene) {
            case 1: PlayerPrefs.DeleteKey("PlayerWeapon");
                PlayerPrefs.DeleteKey("PlayerBlueLight");
                break;
            case 2:
                PlayerPrefs.DeleteKey("PlayerWeapon");
                PlayerPrefs.DeleteKey("PlayerBlueLight");
                break;
            case 3:
                PlayerPrefs.DeleteKey("PlayerWeapon");
                PlayerPrefs.DeleteKey("PlayerBlueLight");
                break;
            case 4:
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.DeleteKey("PlayerBlueLight");
                break;
            case 5:
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.DeleteKey("PlayerBlueLight");
                break;
            case 6:
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
        }
    }
}
