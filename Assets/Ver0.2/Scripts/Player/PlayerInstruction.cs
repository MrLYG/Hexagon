using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInstruction : MonoBehaviour
{
    private TextMeshProUGUI OverheadText;
    private float BLCountDown;
    private float GLCountDown;
    private bool BLCD;
    private bool GLCD;

    private void Start()
    {
        OverheadText = transform.Find("OverheadText").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        resetText();
    }

    private void Update()
    {
        if (BLCD)
        {
            if (BLCountDown > 0)
            {
                BLCountDown -= Time.deltaTime;
                OverheadText.text = BLCountDown.ToString("0.0");
            }
            else
            {
                BLCD = false;
                resetText();
            }
        }
        if (GLCD)
        {
            if (GLCountDown > 0)
            {
                GLCountDown -= Time.deltaTime;
                if(!BLCD)
                    OverheadText.text = "<color=purple> " + GLCountDown.ToString("0.0") + "</color>";
                else
                    OverheadText.text = OverheadText.text + "<color=purple> " + GLCountDown.ToString("0.0") + "</color>";
            }
            else
            {
                GLCD = false;
                resetText();
            }
        }
    }

    public void seteText(string newText)
    {
        OverheadText.text = newText;
    }

    public void resetText()
    {
        if (!BLCD && !GLCD)
        {
            OverheadText.text = "Player";
            OverheadText.color = Color.white;
        }
        OverheadText.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void StartBlueLightCountDown(float time)
    {
        OverheadText.color = new Color(0, 150, 255);
        OverheadText.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0,0,180));

        BLCountDown = time;
        BLCD = true;
    }

    public void StartGreenLightCountDown(float time) 
    {
        //OverheadText.color = new Color(255, 0, 255);
        //OverheadText.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));

        GLCountDown = time;
        GLCD = true;
    }
}
