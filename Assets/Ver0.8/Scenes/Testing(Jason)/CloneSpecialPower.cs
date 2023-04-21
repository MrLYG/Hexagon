using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSpecialPower : PlayerSpecialBullet
{
    [SerializeField] bool hasBlueLight;
    [SerializeField] bool hasGreenLight;
    [SerializeField] bool hasYellowLight;

    public override void Start()
    {
        coolDownIcon.enabled = false;

        // Create prediction dots and make them inivisible for now
        for (int i = 0; i < numDots; i++)
        {
            predicionDots.Add(Instantiate(predicionDot, transform.position, Quaternion.identity));
        }
        showDots(false);


        if (!YellowLightObj)
        {
            YellowLightObj = Instantiate(BulletPrefabs[2], transform.position, Quaternion.identity);
            YellowLightObj.transform.parent = transform;
            YellowLightObj.SetActive(false);
        }

        // If have blue lights
        if (hasBlueLight)
        {
            getPower(BulletPrefabs[0]);
        }
        else
        {
            Bullets.Add(null);
        }

        if (hasGreenLight)
        {
            getPower(BulletPrefabs[1]);
        }
        else
        {
            Bullets.Add(null);
        }

        if (hasYellowLight)
        {
            getPower(BulletPrefabs[2]);
        }
        else
        {
            Bullets.Add(null);
        }

        curBulletIndex = 2;
    }

    public override void Update()
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

        if (Bullets[curBulletIndex] != null)
        {
            if (curBulletIndex == 0)
            {
                BlueLight();
            }
            else if (curBulletIndex == 1)
            {
                GreenLight();
            }
            else if (curBulletIndex == 2)
            {
                YellowLight();
            }

            if (!canUseBlueLight)
            {
                blueLightCDCout += Time.deltaTime;
                if (blueLightCDCout > blueLightCD)
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
            else
            {
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
    }
}
