using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsCheckPoint
{

    public bool isPass;
    public string name;
    public float playerHpLost;
    public float playerHpLostReasonEnemy;
    public float playerHpLostReasonDeadZone;
    public Dictionary<string, AnalyticsEnemy> analyticsEnemiesDict;

    public AnalyticsCheckPoint(bool isPass, string name, float playerHpLost, float playerHpLostReasonEnemy, float playerHpLostReasonDeadZone, Dictionary<string, AnalyticsEnemy> analyticsEnemiesDict)
    {
        this.isPass = isPass;
        this.name = name;
        this.playerHpLost = playerHpLost;
        this.playerHpLostReasonEnemy = playerHpLostReasonEnemy;
        this.playerHpLostReasonDeadZone = playerHpLostReasonDeadZone;
        this.analyticsEnemiesDict = analyticsEnemiesDict;
    }


    /*public AnalyticsCheckPoint(bool isPass, string name, float playerHpLost, float playerHpLostReasonEnemy, float playerHpLostReasonDeadZone)
    {
        this.isPass = isPass;
        this.name = name;
        this.playerHpLost = playerHpLost;
        this.playerHpLostReasonEnemy = playerHpLostReasonEnemy;
        this.playerHpLostReasonDeadZone = playerHpLostReasonDeadZone;
    }*/


    /**"ispass": true,
"name": "c2",
"palyerHpLost": 20**/
}
