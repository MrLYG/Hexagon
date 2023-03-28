using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReverseLight : ILight
{
    [SerializeField] private float AffectTime;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D m_SphereLight;
    [SerializeField] private TextMeshProUGUI m_DurationText;
    private float initIntensity;
    private float appearanceTimeCount;

    public override void Start()
    {
        base.Start();
        initIntensity = m_SphereLight.intensity;
        appearanceTimeCount = 0;
    }

    private void Update()
    {
        if (AffectedObjects.Count == 0)
        {
            appearanceTimeCount += Time.deltaTime;
            m_DurationText.text = (ApperanceTime - appearanceTimeCount).ToString("0.0");
        }
        else
            m_DurationText.text = "";
        m_SphereLight.intensity -= initIntensity / ApperanceTime * Time.deltaTime;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        // Affect Player / Enemy / Object(dead enemy)'s Gravity
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Object"))
        {
            PreAffectedObjects.Add(collision.gameObject);
        }
    }

    private void LateUpdate()
    {
        foreach(GameObject obj in PreAffectedObjects)
        {
            if (obj.GetComponent<ObjectGravity>() != null)
            {
                if ((obj.CompareTag("Player") && PreAffectedObjects.Count == 1) || (!obj.CompareTag("Player")))
                    obj.GetComponent<ObjectGravity>().ReverseGravityDirection();
                else
                    continue;

                if (obj.CompareTag("Player"))
                {
                    obj.GetComponent<PlayerInstruction>().StartBlueLightCountDown(AffectTime);
                    GameObject analytics = GameObject.FindWithTag("Analytics");
                    analytics.GetComponent<Analytics>().playerNumOfBluelight +=1;
                }
                if (obj.CompareTag("Enemy"))
                {
                    obj.GetComponent<EnemyTrack>().numOfBluelight += 1;
                    obj.GetComponent<EnemyTrack>().bluelight = true;
                }

                AffectedObjects.Add(obj);
                Invoke("ResetGravity", AffectTime);

                // Stop really fast falling (Optional)
                if(obj.GetComponent<Rigidbody2D>() != null)
                {
                    obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }

                // Remove light when it affected 1 object
                RemoveLight();
            }
        }
        PreAffectedObjects.Clear();
    }

    // Reset gravity of object after affecting time
    private void ResetGravity()
    {
        if(AffectedObjects[0] != null)
        {
            // Ignore player when player is at his invinvible time (after respawn)
            if(!(AffectedObjects[0].CompareTag("Player") && AffectedObjects[0].GetComponent<PlayerRespawn>().Invincible))
                AffectedObjects[0].GetComponent<ObjectGravity>().ReverseGravityDirection();
            AffectedObjects.RemoveAt(0);
        }
    }

    public override void RemoveLight()
    {
        // If there are still affecting object in the list, disable collider and finish reversing gravity before destroy this light
        if (AffectedObjects.Count != 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<CircleCollider2D>().enabled = false;
            Invoke("RemoveLight", AffectTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
