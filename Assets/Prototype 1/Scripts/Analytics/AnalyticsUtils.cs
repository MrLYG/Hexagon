using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsUtils
{
    // Get all enemies 
    private static Dictionary<string, AnalyticsEnemy> GetAllEnemies()
    {
        Dictionary<string, AnalyticsEnemy> analyticsEnemiesDict = new Dictionary<string, AnalyticsEnemy>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e in enemies)
        {
            AnalyticsEnemy analyticsEnemie = new AnalyticsEnemy(e.GetInstanceID(), e.name, false, false, false, e.GetComponent<EnemyHP>().getHp(), e.GetComponent<EnemyHP>().getHp() > 0 ? "live" : "dead");
            analyticsEnemiesDict.Add(e.name, analyticsEnemie);
        }
        return analyticsEnemiesDict;
    }

    public static Dictionary<string, AnalyticsEnemy> GetAllEnemiesObject()
    {
        Dictionary<string, AnalyticsEnemy> analyticsEnemiesDict = new Dictionary<string, AnalyticsEnemy>();
        GameObject[] allEnemies = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject e in allEnemies)
        {
            if (e.CompareTag("Enemy"))
            {
                AnalyticsEnemy analyticsEnemie = new AnalyticsEnemy(e.GetInstanceID(), e.name, false, false, false, e.GetComponent<EnemyHP>().getHp(), e.GetComponent<EnemyHP>().getHp() > 0 ? "live" : "dead");
                analyticsEnemiesDict.Add(e.GetInstanceID().ToString(), analyticsEnemie);
            }
        }
        return analyticsEnemiesDict;
    }
    public static Dictionary<string, AnalyticsCheckPoint> GetAllCheckpointsObject()
    {
        Dictionary<string, AnalyticsCheckPoint> analyticsCheckpointsDict = new Dictionary<string, AnalyticsCheckPoint>();

        GameObject[] allCheckpoints = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject c in allCheckpoints)
        {
            if (c.CompareTag("Respawn"))
            {
                //Debug.Log(c.ToString());
                AnalyticsCheckPoint analyticsCheckpoint = new AnalyticsCheckPoint(c.GetComponent<CheckPointTrack>().isPass, c.name,c.GetComponent<CheckPointTrack>().playerHpLost, c.GetComponent<CheckPointTrack>().playerHpLostReasonEnemy, c.GetComponent<CheckPointTrack>().playerHpLostReasonDeadZone);
                analyticsCheckpointsDict.Add(c.GetInstanceID().ToString(), analyticsCheckpoint);
            }
        }

        return analyticsCheckpointsDict;

    }

}
