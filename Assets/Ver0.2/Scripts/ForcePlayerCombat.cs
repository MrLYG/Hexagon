using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject WarningText;
    [SerializeField] private List<GameObject> Enemies;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool clear = true;
            foreach (GameObject enemy in Enemies)
            {
                if (enemy.activeSelf)
                {
                    clear = false;
                }
            }
            if (clear)
            {
                Destroy(gameObject);
            }
            else
            {
                WarningText.SetActive(true);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Invoke("removeText", 1);
    }
    private void removeText()
    {
        WarningText.SetActive(false);
    }
}
