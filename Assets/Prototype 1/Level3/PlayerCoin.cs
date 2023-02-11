using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCoin : MonoBehaviour
{
    [SerializeField] private GameObject CoinText;
    private int coins = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            getCoin();
        }
    }

    public void getCoin()
    {
        coins++;
        CoinText.GetComponent<TextMeshProUGUI>().text = "Coins Collected: " + coins;
    }
}
