using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private GameObject GravityManagerPrefab;
    [SerializeField] private GameObject RespawnManagerPrefab;
    [SerializeField] private GameObject BattleManagerPrefab;
    public static Core Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            if(GameObject.FindGameObjectsWithTag("GravityManager").Length == 0)
            {
                Instantiate(GravityManagerPrefab);
            }
            if (GameObject.FindGameObjectsWithTag("RespawnManager").Length == 0)
            {
                Instantiate(RespawnManagerPrefab);
            }
            if (GameObject.FindGameObjectsWithTag("BattleManager").Length == 0)
            {
                Instantiate(BattleManagerPrefab);
            }
        }
    }
}
