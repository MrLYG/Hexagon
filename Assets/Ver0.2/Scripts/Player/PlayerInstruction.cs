using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInstruction : MonoBehaviour
{
    private TextMeshProUGUI OverheadText;
    private float CountDown;
    private bool CoutingDown;

    private void Start()
    {
        OverheadText = transform.Find("OverheadText").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        resetText();
    }

    private void Update()
    {
        if (CoutingDown)
        {
            if (CountDown > 0)
            {
                CountDown -= Time.deltaTime;
                OverheadText.text = CountDown.ToString("0.0");
            }
            else
            {
                resetText();
                CoutingDown = false;
            }
        }
    }

    public void seteText(string newText)
    {
        OverheadText.text = newText;
    }

    public void resetText()
    {
        OverheadText.text = "Player";
        OverheadText.color = Color.white;
        OverheadText.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void StartBlueLightCountDown(float time)
    {
        OverheadText.text = time.ToString("0.0");
        OverheadText.color = new Color(0, 150, 255);
        OverheadText.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0,0,180));

        CountDown = time;
        CoutingDown = true;
    }
}
