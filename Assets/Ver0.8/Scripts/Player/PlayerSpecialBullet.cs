using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpecialBullet : MonoBehaviour
{
    [Tooltip("Initial distance")]
    [SerializeField] protected float initLaunchForce;

    [Tooltip("Max distance")]
    [SerializeField] protected float maxLaunchForce;

    [Tooltip("Charging speed while holding down button")]
    [SerializeField] protected float forceIncreasingSpeed;

    [Tooltip("Prefab of prediction dots")]
    [SerializeField] protected GameObject predicionDot;

    [Tooltip("Number of dots to have for prediction line")]
    [SerializeField] protected int numDots;

    // Reference of the list of prediction dots
    protected List<GameObject> predicionDots = new List<GameObject>();

    // Params related to throwing lights
    protected float curLaunchForce;
    protected bool charging = false;

    [Tooltip("All bullets prefabs for different lights")]
    [SerializeField] protected List<GameObject> BulletPrefabs;

    [Tooltip("Bullets prefabs that player currently have")]
    [SerializeField] protected List<GameObject> Bullets;

    // Prefab of the curret in-use bullet
    [SerializeField] protected int curBulletIndex = -1;

    [Tooltip("CD for using powers")]
    [SerializeField] protected float blueLightCD = 1f;
    [SerializeField] protected float greenLightCD = 10f;
    [SerializeField] protected float yellowLightCD = 5f;

    protected bool canUseBlueLight = true;
    protected bool canUseGreenLight = true;
    protected bool canUseYellowLight = true;

    protected float blueLightCDCout = 0f;
    protected float greenLightCDCout = 0f;
    protected float yellowLightCDCout = 0f;

    public List<Image> coolDownIcon;
    public Image highLight;

    protected GameObject YellowLightObj;

    // Start is called before the first frame update
    public virtual void Start()
    {
        //coolDownIcon.enabled = false;
        highLight.enabled = false;
        for(int i = 0; i < coolDownIcon.Count; i++)
        {
            Color color = coolDownIcon[i].color;
            color.a = 0.1f;
            coolDownIcon[i].color = color;
            //coolDownIcon[i].enabled = false;
        }

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

    public virtual void Update()
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

        if (curBulletIndex == 0 && Bullets.Count > 0)
        {
            BlueLight();
        }else if (curBulletIndex == 1 && Bullets.Count > 1)
        {
            GreenLight();
        }else if(curBulletIndex == 2 && Bullets.Count > 2)
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

        if (!canUseBlueLight)
        {
            coolDownIcon[0].fillAmount = blueLightCDCout / blueLightCD;
        }
        else
        {
            coolDownIcon[0].fillAmount = 1;
        }

        if (!canUseGreenLight)
        {
            coolDownIcon[1].fillAmount = greenLightCDCout / greenLightCD;
        }
        else
        {
            coolDownIcon[1].fillAmount = 1;
        }
        if (!canUseYellowLight)
        {
            coolDownIcon[2].fillAmount = yellowLightCDCout / yellowLightCD;
        }
        else
        {
            coolDownIcon[2].fillAmount = 1;
        }
        
        //
        if ((curBulletIndex == 0 && (GetComponent<ObjectGravity>().getCurrentGD() == GravityDirection.Up || haveTagObject("ReverseLight") || !canUseBlueLight)) 
            || (curBulletIndex == 1 && !canUseGreenLight))
        {
            Color color = coolDownIcon[0].color;
            color.a = 0.5f;
            coolDownIcon[0].color = color;
        }
        else if (Bullets.Count > 0)
        {
            Color color = coolDownIcon[0].color;
            color.a = 1f;
            coolDownIcon[0].color = color;
        }

        if (YellowLightObj && !YellowLightObj.GetComponent<YellowLightN>().activated)
        {
            if (curBulletIndex == 2 && canUseYellowLight)
                YellowLightObj.SetActive(true);
            else
                YellowLightObj.SetActive(false);
        }
    }

    protected void BlueLight() {
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

    protected void GreenLight() {
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.K))
        {
            // Can use power & no other green light exists
            if (canUseGreenLight)
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

    protected void YellowLight()
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

    protected void switchToNext() {
        int newIndex = curBulletIndex + 1;
        if (newIndex == 3)
        {
            newIndex = 0;
        }
        switchTo(newIndex);
    }
    protected void switchTo(int index) {
        curBulletIndex = index;
        switch (curBulletIndex)
        {
            case 0: 
                highLight.transform.position = coolDownIcon[0].transform.position + new Vector3(-6.5f, 6.3f, 0); break;
            case 1:
                highLight.transform.position = coolDownIcon[1].transform.position + new Vector3(-6.5f, 6.3f, 0);
                curLaunchForce = initLaunchForce;
                charging = false;
                showDots(false); break;
            case 2:
                highLight.transform.position = coolDownIcon[2].transform.position + new Vector3(-6.5f, 6.3f, 0);
                //YellowLightObj.SetActive(true);
                //YellowLightObj.transform.parent = transform;
                break;
        }
    }

    protected bool haveTagObject(string tag) {
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
            highLight.enabled = true;
            if (bullet.name.Equals("ReverseBullet"))
            {
                PlayerPrefs.SetInt("PlayerBlueLight", 1);
                Color color = coolDownIcon[0].color;
                color.a = 1f;
                coolDownIcon[0].color = color;
            }
            else if (bullet.name.Equals("GreenLight")) {
                PlayerPrefs.SetInt("PlayerGreenLight", 1);
                Color color = coolDownIcon[1].color;
                color.a = 1f;
                coolDownIcon[1].color = color;
            }
            else if (bullet.name.Equals("YellowBullet"))
            {
                PlayerPrefs.SetInt("PlayerYellowLight", 1);
                if (!YellowLightObj)
                {
                    YellowLightObj = Instantiate(BulletPrefabs[2], transform.position, Quaternion.identity);
                    YellowLightObj.transform.parent = transform;
                }
                Color color = coolDownIcon[2].color;
                color.a = 1f;
                coolDownIcon[2].color = color;
            }
            Bullets.Add(bullet);
            switchTo(Bullets.Count - 1);
        }
        //curBulletIndex = 0;
    }

    public void startPowerCD() {
        if (curBulletIndex == 0)
        {
            canUseBlueLight = false;
            blueLightCDCout = 0;
            coolDownIcon[0].fillAmount = 0.0f;
        }
        else if (curBulletIndex == 1) {
            canUseGreenLight = false;
            greenLightCDCout = 0;
            coolDownIcon[1].fillAmount = 0.0f;
        }
        else if (curBulletIndex == 2)
        {
            canUseYellowLight = false;
            yellowLightCDCout = 0;
            coolDownIcon[2].fillAmount = 0.0f;
        }
    }

    protected void GreenLightPowerEnd() {
        GetComponent<PlayerSlowFall>().stopSlowFall();
    }

    // For showing prediction line
    protected void showDots(bool show)
    {
        for (int i = 0; i < numDots; i++)
        {
            predicionDots[i].SetActive(show);
        }
    }

    protected void DrawProjectile()
    {
        for (int i = 0; i < numDots; i++)
        {
            predicionDots[i].transform.position = PredictLocation(i * 0.1f);
        }
    }

    protected Vector3 PredictLocation(float time)
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

    protected void OnApplicationQuit()
    {
        /*
        PlayerPrefs.DeleteKey("PlayerWeapon");
        PlayerPrefs.DeleteKey("PlayerBlueLight");
        PlayerPrefs.DeleteKey("PlayerGreenLight");
        PlayerPrefs.DeleteKey("PlayerYellowLight");
        */
    }
}
