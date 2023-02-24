using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private string gameLevelScene;

    public void StartGame()
    {
        SceneManager.LoadScene(gameLevelScene);
        switch (gameLevelScene) {
            case "Level1": PlayerPrefs.DeleteKey("PlayerWeapon");
                PlayerPrefs.DeleteKey("PlayerBlueLight");
                break;
            case "Level2":
                PlayerPrefs.DeleteKey("PlayerWeapon");
                PlayerPrefs.DeleteKey("PlayerBlueLight");
                break;
            case "Level3":
                PlayerPrefs.DeleteKey("PlayerWeapon");
                PlayerPrefs.DeleteKey("PlayerBlueLight");
                break;
            case "Level4":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.DeleteKey("PlayerBlueLight");
                break;
            case "Level5":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.DeleteKey("PlayerBlueLight");
                break;
            case "Level6":
                PlayerPrefs.SetString("PlayerWeapon", "Weapon_Stick");
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                break;
        }
    }
}
