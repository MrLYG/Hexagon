using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMechanicsUI : MonoBehaviour
{
    public static GameMechanicsUI gameMechanic;
    public Image[] gameKeys;

    // Start is called before the first frame update
    void Awake()
    {
        gameMechanic = this;
    }

    // Update is called once per frame
    void Update()
    {
        GetKeyColor();
    }

    void GetInitColor()
    {
        for(int i = 0; i < gameKeys.Length; i++)
        {
            gameKeys[i].color = new Color32(255, 255, 255, 150);
        }
    }

    public void GetKeyColor()
    {
        Color keyUpColor = new Color32(255, 255, 255, 150);
        Color keyDownColor = new Color32(180, 180, 180, 150);


        // Default key colors
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            gameMechanic.gameKeys[0].color = keyUpColor;
            gameMechanic.gameKeys[3].color = keyUpColor;
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            gameMechanic.gameKeys[1].color = keyUpColor;
            gameMechanic.gameKeys[4].color = keyUpColor;
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            gameMechanic.gameKeys[2].color = keyUpColor;
            gameMechanic.gameKeys[5].color = keyUpColor;
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            gameMechanic.gameKeys[6].color = keyUpColor;
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            gameMechanic.gameKeys[7].color = keyUpColor;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            gameMechanic.gameKeys[8].color = keyUpColor;
        }

        // Keys change color when pressed
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameMechanic.gameKeys[0].color = keyDownColor;
            gameMechanic.gameKeys[3].color = keyDownColor;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameMechanic.gameKeys[1].color = keyDownColor;
            gameMechanic.gameKeys[4].color = keyDownColor;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameMechanic.gameKeys[2].color = keyDownColor;
            gameMechanic.gameKeys[5].color = keyDownColor;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            gameMechanic.gameKeys[6].color = keyDownColor;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            gameMechanic.gameKeys[7].color = keyDownColor;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            gameMechanic.gameKeys[8].color = keyDownColor;
        }
    }
}
