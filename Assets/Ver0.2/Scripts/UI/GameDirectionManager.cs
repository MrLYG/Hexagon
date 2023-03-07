using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameDirectionManager : MonoBehaviour
{
    [SerializeField] private GameObject gameFlow;
    private float click = 0.0f;
    private float clickTime = 0.0f;
    private float clickDelay = 0.0f;
    private bool active = true;

    private void Start()
    {
        gameFlow.SetActive(active);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && gameFlow.gameObject.CompareTag("Spacebar"))
        {
            gameFlow.SetActive(false);
        }
        else if(Input.GetMouseButtonDown(1) && gameFlow.gameObject.CompareTag("RightClickBlue"))
        {
            gameFlow.SetActive(false);
        }
        else if(Input.GetMouseButtonDown(1) && gameFlow.gameObject.CompareTag("RightClick"))
        {
            gameFlow.SetActive(false);
        }
    }

}
