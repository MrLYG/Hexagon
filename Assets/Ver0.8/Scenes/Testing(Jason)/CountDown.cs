using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField] private float cd;
    private TextMeshProUGUI OverheadText;

    // Start is called before the first frame update
    void Start()
    {
        OverheadText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.gameObject.GetComponent<IEnemy>() && !transform.parent.gameObject.GetComponent<IEnemy>().enabled)
        {
            Destroy(gameObject);
        }
        if (cd > 0)
        {
            cd -= Time.deltaTime;
            OverheadText.text = cd.ToString("0.0");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
