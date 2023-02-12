using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public Text gameInstruction;

    // Start is called before the first frame update
    void Start()
    {
        gameInstruction.text = "Press D & A to move.";
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
}
