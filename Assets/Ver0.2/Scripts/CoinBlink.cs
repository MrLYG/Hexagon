using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlink : MonoBehaviour
{
    [SerializeField] private float acceleration = 0.01f;
    private float fadingV = 0f;
    private bool fading = true;

    // Update is called once per frame
    void Update()
    {
        if (fading) {
            fadingV += acceleration * Time.deltaTime;
            Color curColor = GetComponent<SpriteRenderer>().color;
            curColor.a -= fadingV;
            GetComponent<SpriteRenderer>().color = curColor;
            if (curColor.a <= 0)
            {
                fading = false;
            }
        }
        else
        {
            fadingV -= acceleration * Time.deltaTime;
            fadingV = Mathf.Max(fadingV, 0.01f);
            Color curColor = GetComponent<SpriteRenderer>().color;
            curColor.a += fadingV;
            GetComponent<SpriteRenderer>().color = curColor;
            if (curColor.a >= 1)
            {
                fadingV = 0f;
                fading = true;
            }
        }
    }
}
