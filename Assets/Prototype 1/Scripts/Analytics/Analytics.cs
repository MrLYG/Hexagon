using System.Collections.Generic;
using UnityEngine;

public class Analytics : MonoBehaviour
{

    public float runningTime = 0.0f;
    public float beHits = 0.0f;
    public static GameObject curCP;
    //public Dictionary<string, AnalyticsEnemy> analyticsEnemiesDict;
    public GameObject[] allEnemies;
    public float playerNumOfBluelight = 0;
    public float playerNumOfGreenlight = 0;

    // Start is called before the first frame update
    void Start()
    {
        //GetAllEnemies();
        curCP = new GameObject();

        getAllEnemies();
    }

    void getAllEnemies()
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    

    // Update is called once per frame
    void Update()
    {
        runningTime = runningTime + Time.deltaTime;
    }



   
}
