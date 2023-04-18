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
        PlayerPrefs.DeleteKey("PlayerYellowLight");
        PlayerPrefs.DeleteKey("RespawnX");
        PlayerPrefs.DeleteKey("RespawnY");

        switch (gameLevelScene) {
            case "Level3":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                break;
            case "Level4-0":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
            case "Level4-1":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
            case "Level5":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
            case "Level6":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
            case "Level7-0":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
            case "Level7-1":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
            case "Level7-2":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
            case "Level7-3":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
            case "Level8":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                PlayerPrefs.SetInt("PlayerGreenLight", 1);
                break;
            case "Level9":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                PlayerPrefs.SetInt("PlayerGreenLight", 1);
                break;
            case "Level10-0":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                PlayerPrefs.SetInt("PlayerGreenLight", 1);
                PlayerPrefs.SetInt("PlayerYellowLight", 1);
                break;
            case "Level10-1":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                PlayerPrefs.SetInt("PlayerGreenLight", 1);
                PlayerPrefs.SetInt("PlayerYellowLight", 1);
                break;
            case "Level10-2":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                PlayerPrefs.SetInt("PlayerGreenLight", 1);
                PlayerPrefs.SetInt("PlayerYellowLight", 1);
                break;
            case "Level10-3":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                PlayerPrefs.SetInt("PlayerGreenLight", 1);
                PlayerPrefs.SetInt("PlayerYellowLight", 1);
                break;
            case "Level10-4":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                PlayerPrefs.SetInt("PlayerGreenLight", 1);
                PlayerPrefs.SetInt("PlayerYellowLight", 1);
                break;
            case "Level10-5":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                PlayerPrefs.SetInt("PlayerGreenLight", 1);
                PlayerPrefs.SetInt("PlayerYellowLight", 1);
                break;
            case "Level10-6":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                PlayerPrefs.SetInt("PlayerGreenLight", 1);
                PlayerPrefs.SetInt("PlayerYellowLight", 1);
                break;
        }

        SceneManager.LoadScene(gameLevelScene);
    }
}
