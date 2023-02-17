using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private GameObject m_RepawnManager;
    [SerializeField] private float hitDuration = 0.2f;
    [SerializeField] private float hitCD = 0.4f;
    private bool canBeHit = true;
    private float hitCount;

    [SerializeField] private GameObject HPText;
    [SerializeField] private GameObject analytics;
    private int hp = 5;
    [SerializeField] private int initialHP = 3;

    void Start()
    {
        if (m_RepawnManager == null)
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("RespawnManager"))
            {
                m_RepawnManager = gameObject;
            }
        }

        if(HPText == null)
            HPText = GameObject.Find("HPCounts");

        if(analytics == null)
            analytics = GameObject.Find("Analystic");

        // Initialize HP
        hp = initialHP;
        HPText.GetComponent<TextMeshProUGUI>().text = "HP: " + hp;
    }

    private void FixedUpdate()
    {
        if (!canBeHit)
        {
            hitCount += Time.deltaTime;

            // Animation time ends, reset to white color
            if (hitCount > hitDuration)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }

            // Invincible time ends, reset canBeHit
            if (hitCount > hitCD)
            {
                canBeHit = true;
            }
        }
    }

    public void getHurt(float cd, GameObject reasonObj)
    {
        if (canBeHit)
        {
            // Set invincible time
            hitCD = cd;

            // Set up time count and animation
            canBeHit = false;
            hitCount = 0;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            
            // Decrease HP and UI
            hp--;
            HPText.GetComponent<TextMeshProUGUI>().text = "HP: " + hp;

            // trigger to collect data
            if(analytics != null)
                analytics.GetComponent<Analytics>().beHits++;

            // Respawn player if HP reduced to 0 or below
            if (hp <= 0)
            {
                m_RepawnManager.GetComponent<RespawnManager>().RespawnPlayer();
                hp = initialHP;
                HPText.GetComponent<TextMeshProUGUI>().text = "HP: " + hp;
            }
        }
    }
}
