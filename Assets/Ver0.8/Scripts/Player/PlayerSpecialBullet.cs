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
    [SerializeField] private float blueLightCD = 1f;
    [SerializeField] private float greenLightCD = 10f;
    [SerializeField] private float yellowLightCD = 5f;

    private bool canUseBlueLight = true;
    private bool canUseGreenLight = true;
    private bool canUseYellowLight = true;

    private float blueLightCDCout = 0f;
    private float greenLightCDCout = 0f;
    private float yellowLightCDCout = 0f;

    public Image coolDownIcon;

    private GameObject YellowLightObj;

    // Start is called before the first frame update
    void Start()
    {
        coolDownIcon.enabled = false;

        // Create prediction dots and make them inivisible for now
        for (int i = 0; i < numDots; i++)
        {
            predicionDots.Add(Instantiate(predicionDot, transform.position, Quaternion.identity));
        }
        showDots(false);

        //PlayerPrefs.DeleteKey("PlayerYellowLight");
        if (PlayerPrefs.HasKey("PlayerYellowLight"))
        {
            if (!YellowLightObj)
            {
                YellowLightObj = Instantiate(BulletPrefabs[2], transform.position, Quaternion.identity);
                YellowLightObj.transform.parent = transform;
            }
        }

        // If have blue lights
        if (PlayerPrefs.HasKey("PlayerBlueLight"))
        {
            getPower(BulletPrefabs[0]);
        }
        if (PlayerPrefs.HasKey("PlayerGreenLight"))
        {
            getPower(BulletPrefabs[1]);
        }
        if (PlayerPrefs.HasKey("PlayerYellowLight"))
        {
            getPower(BulletPrefabs[2]);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.U))
        {
            switchToNext();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            switchTo(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            switchTo(1);
        }

        if (curBulletIndex == 0)
        {
            BlueLight();
        }else if (curBulletIndex == 1)
        {
            GreenLight();
        }else if(curBulletIndex == 2)
        {
            YellowLight();
        }

        if (!canUseBlueLight)
        {
            blueLightCDCout += Time.deltaTime;
            if(blueLightCDCout > blueLightCD)
            {
                canUseBlueLight = true;
            }
        }

        if (!canUseGreenLight)
        {
            greenLightCDCout += Time.deltaTime;
            if (greenLightCDCout > greenLightCD)
            {
                canUseGreenLight = true;
            }
        }

        if (!canUseYellowLight)
        {
            yellowLightCDCout += Time.deltaTime;
            if (yellowLightCDCout > yellowLightCD)
            {
                canUseYellowLight = true;
            }
        }

        if (curBulletIndex == 0 && !canUseBlueLight)
        {
            coolDownIcon.fillAmount = blueLightCDCout / blueLightCD;
        }
        else if (curBulletIndex == 1 && !canUseGreenLight)
        {
            coolDownIcon.fillAmount = greenLightCDCout / greenLightCD;
        }
        else if (curBulletIndex == 2 && !canUseYellowLight)
        {
            coolDownIcon.fillAmount = yellowLightCDCout / yellowLightCD;
        }
        else {
            coolDownIcon.fillAmount = 1;
        }

        if ((curBulletIndex == 0 && (GetComponent<ObjectGravity>().getCurrentGD() == GravityDirection.Up || haveTagObject("ReverseLight") || !canUseBlueLight)) 
            || (curBulletIndex == 1 && !canUseGreenLight))
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

        if (YellowLightObj && !YellowLightObj.GetComponent<YellowLightN>().activated)
        {
            if (curBulletIndex == 2 && canUseYellowLight)
                YellowLightObj.SetActive(true);
            else
                YellowLightObj.SetActive(false);
        }
    }

    private void BlueLight() {
        // Start Charging
        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.K)) && !charging)
        {
            // Can use power & Player not in reverse position & not other blue light exists
            if (canUseBlueLight && GetComponent<ObjectGravity>().getCurrentGD() != GravityDirection.Up)
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
        if ((Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.K)) && charging)
        {
            charging = false;
            showDots(false);

            GameObject bullet = Instantiate(Bullets[curBulletIndex], transform.position, Quaternion.identity);
            if (GetComponent<PlayerControl>().facingRight)
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1) * curLaunchForce;
            else
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1) * curLaunchForce;

            startPowerCD();
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
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.K))
        {
            // Can use power & no other green light exists
            if (canUseGreenLight)
            {
                if (!haveTagObject("GreenLight"))
                {
                    GameObject greenLight = Instantiate(Bullets[curBulletIndex], transform.position, Quaternion.identity);
                    greenLight.transform.parent = transform;

                    startPowerCD();
                    GetComponent<PlayerSlowFall>().startSlowFall();
                    Invoke("GreenLightPowerEnd", Bullets[1].GetComponent<GreenLight>().ApperanceTime);

                   GetComponent<PlayerInstruction>().StartGreenLightCountDown(Bullets[1].GetComponent<GreenLight>().ApperanceTime);

                    GameObject analytics = GameObject.FindWithTag("Analytics");
                    analytics.GetComponent<Analytics>().playerNumOfGreenlight += 1;
                }
            }
        }
    }

    private void YellowLight()
    {
        /*
        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.K)))
        {
            //showDots(true);
        }

        // Release
        if ((Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.K)) && canUseYellowLight)
        {
            GameObject bullet = Instantiate(Bullets[curBulletIndex], transform.position, Quaternion.identity);
            if (GetComponent<PlayerControl>().facingRight)
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
            else
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);

            startPowerCD();
        }
        */
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.K))
        {
            // Can use power & no other green light exists
            if (canUseYellowLight)
            {
                //YellowLightObj = Instantiate(Bullets[curBulletIndex], transform.position, Quaternion.identity);
                YellowLightObj.transform.parent = null;
                YellowLightObj.GetComponent<YellowLightN>().ActivateLight();

                startPowerCD();
            }
        }
    }

    private void switchToNext() {
        int newIndex = curBulletIndex + 1;
        if (newIndex == Bullets.Count)
        {
            newIndex = 0;
        }
        switchTo(newIndex);
    }
    private void switchTo(int index) { 
        if(index < Bullets.Count)
        {
            curBulletIndex = index;
        }
        switch (curBulletIndex)
        {
            case 0: coolDownIcon.color = new Color(85, 208, 255) / 255f; break;
            case 1: coolDownIcon.color = new Color(86, 255, 86) / 255f;
                curLaunchForce = initLaunchForce;
                charging = false;
                showDots(false); break;
            case 2: coolDownIcon.color = new Color(255, 255, 50) / 255f;
                //YellowLightObj.SetActive(true);
                //YellowLightObj.transform.parent = transform;
                break;
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

    public void resetCD() {
        blueLightCDCout = blueLightCD;
        greenLightCDCout = greenLightCD;
        yellowLightCDCout = yellowLightCD;
    }

    // Get a referene of the bullet prefab and include into player's bullet list
    public void getPower(GameObject bullet)
    {
        if (!Bullets.Contains(bullet))
        {
            if (bullet.name.Equals("ReverseBullet"))
            {
                coolDownIcon.enabled = true;
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
            }
            else if (bullet.name.Equals("GreenLight")) {
                PlayerPrefs.SetInt("PlayerGreenLight", 1);
            }else if (bullet.name.Equals("YellowBullet"))
            {
                PlayerPrefs.SetInt("PlayerYellowLight", 1);
                if (!YellowLightObj)
                {
                    YellowLightObj = Instantiate(BulletPrefabs[2], transform.position, Quaternion.identity);
                    YellowLightObj.transform.parent = transform;
                }
            }
            Bullets.Add(bullet);
            switchTo(Bullets.Count - 1);
        }
        //curBulletIndex = 0;
    }

    public void startPowerCD() {
        coolDownIcon.fillAmount = 0.0f;
        if (curBulletIndex == 0)
        {
            canUseBlueLight = false;
            blueLightCDCout = 0;
        }
        else if (curBulletIndex == 1) {
            canUseGreenLight = false;
            greenLightCDCout = 0;
        }else if (curBulletIndex == 2)
        {
            canUseYellowLight = false;
            yellowLightCDCout = 0;
        }
    }

    private void GreenLightPowerEnd() {
        GetComponent<PlayerSlowFall>().stopSlowFall();
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

    private void OnApplicationQuit()
    {
        /*
        PlayerPrefs.DeleteKey("PlayerWeapon");
        PlayerPrefs.DeleteKey("PlayerBlueLight");
        PlayerPrefs.DeleteKey("PlayerGreenLight");
        PlayerPrefs.DeleteKey("PlayerYellowLight");
        */
    }
}
