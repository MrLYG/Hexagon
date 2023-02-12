using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] RespawnManager m_RepawnManager;
    [SerializeField] private float hitDuration = 0.2f;
    [SerializeField] private float hitCD = 0.4f;
    private bool canBeHit = true;
    private float hitCount;

    [SerializeField] private GameObject HPText;
    [SerializeField] private GameObject analytics;
    private int hp = 5;
    [SerializeField] private int initialHP = 5;
    // Start is called before the first frame update
    void Start()
    {
        hp = initialHP;
    }

    private void FixedUpdate()
    {
        if (!canBeHit)
        {
            hitCount += Time.deltaTime;
            if (hitCount > hitDuration)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (hitCount > hitCD)
            {
                canBeHit = true;
            }
        }
    }

    public void getHurt(float cd, GameObject reasonObj)
    {
        hitCD = cd;
        if (canBeHit)
        {
            canBeHit = false;
            hitCount = 0;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            
            hp--;
            HPText.GetComponent<TextMeshProUGUI>().text = "HP: " + hp;

            // trigger to collect data
            analytics.GetComponent<Analytics>().beHits++;

            if (hp <= 0)
            {
                m_RepawnManager.GetComponent<RespawnManager>().RespawnPlayer();
                hp = initialHP;
            }
        }
    }
}
