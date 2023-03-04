using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private int curBulletIndex = -1;

    [Tooltip("CD for using powers")]
    [SerializeField] private float powerCD = 1f;

    private bool canUsePower = true;
    public Image coolDownIcon;

    // Start is called before the first frame update
    void Start()
    {
        coolDownIcon.enabled = false;

        // If have blue lights
        if (PlayerPrefs.HasKey("PlayerBlueLight"))
        {
            getPower(BulletPrefabs[0]);
        }
        if (PlayerPrefs.HasKey("PlayerGreenLight"))
        {
            //getPower(BulletPrefabs[1]);
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {

        }

        if (curBulletIndex == 0)
        {
            BlueLight();
        }

        if (curBulletIndex == 1)
        {
            GreenLight();
        }

        if(!canUsePower)
        {
            coolDownIcon.fillAmount += 1.0f / powerCD * Time.deltaTime;
        }
        else
        {
            coolDownIcon.fillAmount = 1;
        }

        if (GetComponent<ObjectGravity>().getCurrentGD() == GravityDirection.Up || haveTagObject("ReverseLight") || !canUsePower)
        {
            Color color = coolDownIcon.color;
            color.a = 0.5f;
            coolDownIcon.color = color;
        }
        else
        {
            Color color = coolDownIcon.color;
            color.a = 1f;
            coolDownIcon.color = color;
        }
    }

    private void BlueLight() {
        // Start Charging
        if (Input.GetMouseButtonDown(1) && !charging)
        {
            // Can use power & Player not in reverse position & not other blue light exists
            if (canUsePower && GetComponent<ObjectGravity>().getCurrentGD() != GravityDirection.Up)
            {
                if (!haveTagObject("ReverseLight"))
                {
                    curLaunchForce = initLaunchForce;
                    charging = true;
                    showDots(true);
                }
            }
        }

        // Release
        if (Input.GetMouseButtonUp(1) && charging)
        {
            charging = false;
            showDots(false);

            GameObject bullet = Instantiate(Bullets[curBulletIndex], transform.position, Quaternion.identity);
            if (GetComponent<PlayerControl>().facingRight)
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1) * curLaunchForce;
            else
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1) * curLaunchForce;

            startPowerCD(powerCD);
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

    private void GreenLight() {
        if (Input.GetMouseButtonDown(1))
        {
            // Can use power & no other green light exists
            if (canUsePower)
            {
                if (!haveTagObject("GreenLight"))
                {
                    GameObject greenLight = Instantiate(Bullets[curBulletIndex], transform.position, Quaternion.identity);
                    greenLight.transform.parent = transform;

                    GetComponent<PlayerSlowFall>().startSlowFall();
                    Invoke("greenLightCD", Bullets[curBulletIndex].GetComponent<GreenLight>().ApperanceTime);
                }
            }
        }
    }

    private bool haveTagObject(string tag) {
        bool haveObject = false;
        if (GameObject.FindGameObjectsWithTag(tag).Length != 0)
        {
            foreach (GameObject lights in GameObject.FindGameObjectsWithTag(tag))
            {
                if (lights.GetComponent<CircleCollider2D>().enabled)
                {
                    haveObject = true;
                }
            }
        }
        return haveObject;
    }

    // Get a referene of the bullet prefab and include into player's bullet list
    public void getPower(GameObject bullet)
    {
        if(bullet.name.Equals("ReverseBullet"))
        {
            coolDownIcon.enabled = true;
            PlayerPrefs.SetInt("PlayerBlueLight", 1);
        }

        if (!Bullets.Contains(bullet))
        {
            Bullets.Add(bullet);
        }
        curBulletIndex = Bullets.Count - 1;
        //curBulletIndex = 0;
    }

    public void startPowerCD(float time) {
        canUsePower = false;
        coolDownIcon.fillAmount = 0.0f;

        Invoke("resetPowerCD", time);
    }

    // Invoked when CD for using power has passed
    private void resetPowerCD() {
        canUsePower = true;
    }

    private void greenLightCD() {
        GetComponent<PlayerSlowFall>().stopSlowFall();
        startPowerCD(powerCD);
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
