using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    [Tooltip("Reference of Respawn Manager")]
    [SerializeField] private GameObject m_RepawnManager;

    [Tooltip("Being hit coloring changing duration")]
    [SerializeField] private float hitDuration = 0.2f;

    [Tooltip("Invincible time after being hit")]
    [SerializeField] private float hitCD = 0.4f;

    private bool canBeHit = true;
    private float hitCount;

    //[Tooltip("Reference of the HP UI")]
    //[SerializeField] private GameObject HPText;

    [Tooltip("Reference of Analytic object")]
    [SerializeField] private GameObject analytics;
    
    [Tooltip("Initial HP")]
    [SerializeField] private float initialHP = 3;
    private float hp;

    [SerializeField] private Slider slider;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Gradient healthProgress;
    public Image fill;

    void Start()
    {
        if (m_RepawnManager == null)
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("RespawnManager"))
            {
                m_RepawnManager = gameObject;
            }
        }

        if(healthBar == null)
            healthBar = GameObject.Find("HPCounts");

        if(analytics == null)
            analytics = GameObject.Find("Analystic");

        // Initialize HP
        hp = initialHP;
        //HPText.GetComponent<TextMeshProUGUI>().text = "HP: " + hp;
        slider.maxValue = hp;
        slider.value = hp;
        fill.color = healthProgress.Evaluate(1f);
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
            //HPText.GetComponent<TextMeshProUGUI>().text = "HP: " + hp;
            slider.value = hp;
            fill.color = healthProgress.Evaluate(slider.normalizedValue);

            // Damage Pop
            if(GetComponent<DamagePop>() != null && hp > 0)
            {
                GetComponent<DamagePop>().PopDamage(1);
            }

            // trigger to collect data
            if(analytics != null)
                analytics.GetComponent<Analytics>().beHits++;

            Analytics.curCP.GetComponent<CheckPointTrack>().playerHpLost = Analytics.curCP.GetComponent<CheckPointTrack>().playerHpLost + 1;
            if (reasonObj.CompareTag("DeadZone"))
            {
                //Debug.Log(reasonObj.tag);
                Analytics.curCP.GetComponent<CheckPointTrack>().playerHpLostReasonDeadZone = Analytics.curCP.GetComponent<CheckPointTrack>().playerHpLostReasonDeadZone + 1;
            }
            if (reasonObj.CompareTag("Enemy"))
            {
                //Debug.Log(reasonObj.tag);
                Analytics.curCP.GetComponent<CheckPointTrack>().playerHpLostReasonEnemy = Analytics.curCP.GetComponent<CheckPointTrack>().playerHpLostReasonEnemy + 1;
            }
            // ----------------------------------------------------- collect data end -------------------------------



            // Respawn player if HP reduced to 0 or below
            if (hp <= 0)
            {
                m_RepawnManager.GetComponent<RespawnManager>().RespawnPlayer();
            }
        }
    }

    public void setHP(float newHP)
    {
        hp = newHP;
        //HPText.GetComponent<TextMeshProUGUI>().text = "HP: " + hp;
    }

    public void setHP() {
        hp = initialHP;
        slider.value = hp;
        fill.color = healthProgress.Evaluate(1f);
        //HPText.GetComponent<TextMeshProUGUI>().text = "HP: " + hp;
    }
}
