using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private string gameLevelScene;

    public void StartGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.DeleteKey("PlayerWeapon");
        PlayerPrefs.DeleteKey("PlayerBlueLight");
        PlayerPrefs.DeleteKey("PlayerGreenLight");

        switch (gameLevelScene) {
            case "Level4":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                break;
            case "Level5":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                break;
            case "Level6":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
            case "Level7":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
            case "Level8":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
            case "Level9":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
            case "Level10":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                PlayerPrefs.SetInt("PlayerGreenLight", 1);
                break;
        }

        SceneManager.LoadScene(gameLevelScene);
    }
}
