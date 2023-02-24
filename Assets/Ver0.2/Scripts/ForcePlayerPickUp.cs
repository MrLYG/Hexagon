using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForcePlayerPickUp : MonoBehaviour
{
    [SerializeField] private GameObject WarningText;
    [SerializeField] private string keyName;
    [SerializeField] private string Warning;

    private void Start()
    {
        WarningText.GetComponent<TextMeshProUGUI>().text = "";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerPrefs.HasKey(keyName))
        {
            Destroy(gameObject);
        }
        else
        {
            WarningText.GetComponent<TextMeshProUGUI>().text = Warning;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Invoke("removeText", 1);
    }

    private void removeText() {
        WarningText.GetComponent<TextMeshProUGUI>().text = "";
    }
}
