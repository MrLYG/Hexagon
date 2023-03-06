using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseLight : ILight
{
    [SerializeField] private float AffectTime;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D m_SphereLight;
    private float initIntensity;

    public override void Start()
    {
        base.Start();
        initIntensity = m_SphereLight.intensity;
    }

    private void Update()
    {
        m_SphereLight.intensity -= (initIntensity - 0.1f) / ApperanceTime * Time.deltaTime;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        // Affect Player / Enemy / Object(dead enemy)'s Gravity
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Object"))
        {
           if(collision.gameObject.GetComponent<ObjectGravity>() != null)
            {
                collision.gameObject.GetComponent<ObjectGravity>().ReverseGravityDirection();

                if (collision.gameObject.CompareTag("Player"))
                {
                    GameObject analytics = GameObject.FindWithTag("Analytics");
                    analytics.GetComponent<Analytics>().playerNumOfBluelight +=1;
                }
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    collision.gameObject.GetComponent<EnemyTrack>().numOfBluelight += 1;
                    collision.gameObject.GetComponent<EnemyTrack>().bluelight = true;
                }

                AffectedObjects.Add(collision.gameObject);
                Invoke("ResetGravity", AffectTime);

                // Stop really fast falling (Optional)
                if(collision.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }

                // Remove light when it affected 1 object
                RemoveLight();
            }
        }
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
