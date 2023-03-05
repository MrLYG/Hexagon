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
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D) && gameInstruction.gameObject.CompareTag("D Key"))
        {
            Time.timeScale = 1.0f;
            Destroy(gameObject);
        }
        else if(Input.GetKey(KeyCode.A) && gameInstruction.gameObject.CompareTag("A Key"))
        {
            Time.timeScale = 1.0f;
            Destroy(gameObject);
        }
    }
}
