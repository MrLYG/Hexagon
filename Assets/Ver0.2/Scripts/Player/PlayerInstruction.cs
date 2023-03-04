using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInstruction : MonoBehaviour
{
    private GameObject OverheadText;

    private void Start()
    {
        OverheadText = transform.Find("OverheadText").transform.Find("Text").gameObject;
        resetText();
    }

    public void seteText(string newText)
    {
        OverheadText.GetComponent<TextMeshProUGUI>().text = newText;
    }

    public void resetText()
    {
        OverheadText.GetComponent<TextMeshProUGUI>().text = "Player";
    }
}
