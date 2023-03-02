using System.Collections.Generic;
using UnityEngine;

public class Analytics : MonoBehaviour
{

    public float runningTime = 0.0f;
    public float beHits = 0.0f;
    public Dictionary<string, AnalyticsEnemy> analyticsEnemiesDict = new Dictionary<string, AnalyticsEnemy>();

    // Start is called before the first frame update
    void Start()
    {
        //GetAllEnemies();
    }

    

    // Update is called once per frame
    void Update()
    {
        runningTime = runningTime + Time.deltaTime;
    }



    // Get all enemies 
    private void GetAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e in enemies)
        {
            AnalyticsEnemy analyticsEnemie = new AnalyticsEnemy(e.GetInstanceID(), e.name, false, false, false, e.GetComponent<EnemyHP>().getHp(), e.GetComponent<EnemyHP>().getHp() > 0 ? "live" : "dead");
            analyticsEnemiesDict.Add(e.name, analyticsEnemie);
        }
    }

    public void GetAllEnemiesObject()
    {

        GameObject[] allEnemies = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject e in allEnemies)
        {
            if (e.CompareTag("Enemy"))
            {
                AnalyticsEnemy analyticsEnemie = new AnalyticsEnemy(e.GetInstanceID(), e.name, false, false, false, e.GetComponent<EnemyHP>().getHp(), e.GetComponent<EnemyHP>().getHp() > 0 ? "live" : "dead");
                analyticsEnemiesDict.Add(e.name, analyticsEnemie);
            }
        }
    }
}
