using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BluePowerInstructions : MonoBehaviour
{
    [SerializeField] private TextMeshPro powerInstruction;

    // Start is called before the first frame update
    void Start()
    {
        powerInstruction.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            powerInstruction.enabled = true;
        }
    }

    /*
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    */
}
