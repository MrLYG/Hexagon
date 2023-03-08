using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsUtils
{
    // Get all enemies 
    /*private static Dictionary<string, AnalyticsEnemy> GetAllEnemies()
    {
        Dictionary<string, AnalyticsEnemy> analyticsEnemiesDict = new Dictionary<string, AnalyticsEnemy>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e in enemies)
        {
            AnalyticsEnemy analyticsEnemie = new AnalyticsEnemy(e.GetInstanceID(), e.name, false, false, false, e.GetComponent<EnemyHP>().getHp(), e.GetComponent<EnemyHP>().getHp() > 0 ? "live" : "dead", e.GetComponent<EnemyTrack>().harm);
            analyticsEnemiesDict.Add(e.name, analyticsEnemie);
        }
        return analyticsEnemiesDict;
    }*/

    /*public static Dictionary<string, AnalyticsEnemy> GetAllEnemiesObject()
    {
        Dictionary<string, AnalyticsEnemy> analyticsEnemiesDict = new Dictionary<string, AnalyticsEnemy>();
        GameObject[] allEnemies = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject e in allEnemies)
        {
            if (e.CompareTag("Enemy"))
            {
                AnalyticsEnemy analyticsEnemie = new AnalyticsEnemy(e.GetInstanceID(), e.name, false, false, false, e.GetComponent<EnemyHP>().getHp(), e.GetComponent<EnemyHP>().getHp() > 0 ? "live" : "dead", e.GetComponent<EnemyTrack>().harm);
                analyticsEnemiesDict.Add(e.GetInstanceID().ToString(), analyticsEnemie);
            }
        }
        return analyticsEnemiesDict;
    }*/
    public static Dictionary<string, AnalyticsEnemy> GetAllEnemiesObjectByInital(GameObject[] allEnemies)
    {
        //Debug.Log(allEnemies[0].ToString());
        Dictionary<string, AnalyticsEnemy> analyticsEnemiesDict = new Dictionary<string, AnalyticsEnemy>();

        foreach (GameObject e in allEnemies)
        {
            if(e != null)
            {
                //Debug.Log(e.ToString());
                if (e.GetComponent<EnemyHP>())
                {
                    AnalyticsEnemy analyticsEnemie = new AnalyticsEnemy(e.GetInstanceID(), e.name, e.GetComponent<EnemyTrack>().bluelight, e.GetComponent<EnemyTrack>().deadZone, e.GetComponent<EnemyTrack>().weapon, e.GetComponent<EnemyHP>().getHp(), e.GetComponent<EnemyHP>().getHp() > 0 ? "live" : "dead", e.GetComponent<EnemyTrack>().harm, e.GetComponent<EnemyTrack>().numOfBluelight, e.GetComponent<EnemyTrack>().numOfWeapon, e.GetComponent<EnemyTrack>().numOfGreenlight);
                    analyticsEnemiesDict.Add(e.GetInstanceID().ToString(), analyticsEnemie);
                }
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
                Dictionary<string, AnalyticsEnemy> analyticsEnemiesDict = new Dictionary<string, AnalyticsEnemy>();

                foreach (GameObject e in c.GetComponent<RespawnPoint>().Enemies)
                {
                    if (e.GetComponent<EnemyHP>())
                    {
                        AnalyticsEnemy analyticsEnemie = new AnalyticsEnemy(e.GetInstanceID(), e.name, e.GetComponent<EnemyTrack>().bluelight, e.GetComponent<EnemyTrack>().deadZone, e.GetComponent<EnemyTrack>().weapon, e.GetComponent<EnemyHP>().getHp(), e.GetComponent<EnemyHP>().getHp() > 0 ? "live" : "dead", e.GetComponent<EnemyTrack>().harm, e.GetComponent<EnemyTrack>().numOfBluelight, e.GetComponent<EnemyTrack>().numOfWeapon, e.GetComponent<EnemyTrack>().numOfGreenlight);
                        analyticsEnemiesDict.Add(e.GetInstanceID().ToString(), analyticsEnemie);
                    }
                }
                
                AnalyticsCheckPoint analyticsCheckpoint = new AnalyticsCheckPoint(c.GetComponent<CheckPointTrack>().isPass, c.name,c.GetComponent<CheckPointTrack>().playerHpLost, c.GetComponent<CheckPointTrack>().playerHpLostReasonEnemy, c.GetComponent<CheckPointTrack>().playerHpLostReasonDeadZone, analyticsEnemiesDict);
                analyticsCheckpointsDict.Add(c.GetInstanceID().ToString(), analyticsCheckpoint);
            }
        }

        return analyticsCheckpointsDict;

    }

}
