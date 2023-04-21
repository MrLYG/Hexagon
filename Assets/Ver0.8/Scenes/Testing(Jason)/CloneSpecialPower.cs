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
        //coolDownIcon.enabled = false;
        highLight.enabled = false;
        for (int i = 0; i < coolDownIcon.Count; i++)
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

        //curBulletIndex = 2;
    }

    public override void Update()
    {
        if (curBulletIndex > -1 && Bullets[curBulletIndex] != null)
        {
            if (curBulletIndex == 0 && Bullets.Count > 0)
            {
                BlueLight();
            }
            else if (curBulletIndex == 1 && Bullets.Count > 1)
            {
                GreenLight();
            }
            else if (curBulletIndex == 2 && Bullets.Count > 2)
            {
                YellowLight();
            }
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
        else if (curBulletIndex == 0 && Bullets.Count > 0)
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
}
