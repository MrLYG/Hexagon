using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialBullet : MonoBehaviour
{
    [Tooltip("Initial distance")]
    [SerializeField] private float initLaunchForce;

    [Tooltip("Max distance")]
    [SerializeField] private float maxLaunchForce;

    [Tooltip("Charging speed while holding down button")]
    [SerializeField] private float forceIncreasingSpeed;

    [Tooltip("Prefab of prediction dots")]
    [SerializeField] private GameObject predicionDot;

    [Tooltip("Number of dots to have for prediction line")]
    [SerializeField] private int numDots;

    // Reference of the list of prediction dots
    private List<GameObject> predicionDots = new List<GameObject>();

    // Params related to throwing lights
    private float curLaunchForce;
    private bool charging = false;

    [Tooltip("All bullets prefabs for different lights")]
    [SerializeField] private List<GameObject> BulletPrefabs;

    [Tooltip("Bullets prefabs that player currently have")]
    [SerializeField] private List<GameObject> Bullets;

    // Prefab of the curret in-use bullet
    private GameObject curBullet;

    [Tooltip("CD for using powers")]
    [SerializeField] private float powerCD = 1f;

    private bool canUsePower = true;

    // Start is called before the first frame update
    void Start()
    {
        // If have blue lights
        if (PlayerPrefs.HasKey("PlayerBlueLight"))
        {
            getPower(BulletPrefabs[0]);
        }

        // Create prediction dots and make them inivisible for now
        for (int i = 0; i < numDots; i++)
        {
            predicionDots.Add(Instantiate(predicionDot, transform.position, Quaternion.identity));
        }
        showDots(false);
    }

    void Update()
    {
        if (curBullet != null)
        {
            // Start Charging
            if (Input.GetMouseButtonDown(1) && !charging && canUsePower)
            {
                curLaunchForce = initLaunchForce;
                charging = true;
                showDots(true);
            }

            // Release
            if (Input.GetMouseButtonUp(1) && charging)
            {
                charging = false;
                showDots(false);

                GameObject bullet = Instantiate(curBullet, transform.position, Quaternion.identity);
                if (GetComponent<PlayerControl>().facingRight)
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1) * curLaunchForce;
                else
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1) * curLaunchForce;

                canUsePower = false;
                Invoke("resetPowerCD", powerCD);
            }

            // Charge up launching force and upate projection line
            if (charging)
            {
                DrawProjectile();
                curLaunchForce += forceIncreasingSpeed * Time.deltaTime;
                if (curLaunchForce > maxLaunchForce)
                {
                    curLaunchForce = maxLaunchForce;
                }
            }
        }
    }

    // Get a referene of the bullet prefab and include into player's bullet list
    public void getPower(GameObject bullet)
    {
        if(bullet.name.Equals("ReverseBullet"))
        {
            PlayerPrefs.SetInt("PlayerBlueLight", 1);
        }

        if (!Bullets.Contains(bullet))
        {
            Bullets.Add(bullet);
        }
        curBullet = Bullets[Bullets.Count - 1];
    }

    // Invoked when CD for using power has passed
    private void resetPowerCD() {
        canUsePower = true;
    }

    // For showing prediction line
    private void showDots(bool show)
    {
        for (int i = 0; i < numDots; i++)
        {
            predicionDots[i].SetActive(show);
        }
    }

    private void DrawProjectile()
    {
        for (int i = 0; i < numDots; i++)
        {
            predicionDots[i].transform.position = PredictLocation(i * 0.1f);
        }
    }

    private Vector3 PredictLocation(float time)
    {
        Vector3 direction;
        if (GetComponent<PlayerControl>().facingRight)
        {
            direction = new Vector3(1, 1, 0);
        }
        else
        {
            direction = new Vector3(-1, 1, 0);
        }
        Vector2 gravityScale = GetComponent<ObjectGravity>().getCurrentGravity();
        return transform.position + direction * curLaunchForce * time + 0.5f * new Vector3(gravityScale.x, gravityScale.y, 0) * Mathf.Pow(time, 2);
    }
}
