using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject IntructionText;
    [SerializeField] private string m_Instruction;
    [SerializeField] private float lastingTime;
    [SerializeField] private bool repeat = false;
    private bool triggered = false;
    private bool counting = false;
    private float timeCount = 0;

    private void Start()
    {
        // Get reference of IntructionText 
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Instruction"))
        {
            IntructionText = gameObject;
        }
    }

    private void Update()
    {
        if (counting)
        {
            timeCount += Time.deltaTime;
            if(timeCount > lastingTime)
            {
                if(IntructionText.GetComponent<TextMeshProUGUI>().text == m_Instruction)
                {
                    IntructionText.GetComponent<TextMeshProUGUI>().text = "";
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                IntructionText.GetComponent<TextMeshProUGUI>().text = m_Instruction;
                if (lastingTime != -1)
                {
                    counting = true;
                    timeCount = 0;
                }
                if (!repeat)
                {
                    triggered = true;
                }
            }
        }
    }
}
