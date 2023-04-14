using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level3Ins : MonoBehaviour
{
    private GameObject Player;
    private float count;
    public float timeToWait = 10f;
    bool startCounting = false;
    [SerializeField] private GameObject IntructionText;

    private float countText;
    public float timeToWaitText = 3f;
    bool startCountingText = false;

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
        if (startCounting)
        {
            count += Time.deltaTime;
            if(count >= timeToWait)
            {
                startCounting = false;

                if (Player != null)
                {
                    Player.transform.Find("Canvas").gameObject.SetActive(true);
                    Player.transform.Find("Canvas").transform.Find("Name").GetComponent<TextMeshProUGUI>().text = "Q/E";
                    countText = 0;
                    startCountingText = true;
                }
            }
        }
        if (startCountingText)
        {
            countText += Time.deltaTime;
            if (countText >= timeToWaitText)
            {
                startCountingText = false;
                if (Player != null)
                    Player.transform.Find("Canvas").transform.Find("Name").GetComponent<TextMeshProUGUI>().text = "Player";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Player == null) Player = collision.gameObject;
            count = 0;
            startCounting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            count = 0;
            startCounting = false;
        }
    }
}
