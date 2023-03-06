using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnalyticsEnemy
{
    public int instanceId;
    public string name;
    public bool bluelight;
    public bool deadZone;
    public bool weapon; 
    public float hp; 
    public string status;
    public float harm;
    public float numOfBluelight;
    public float numOfWeapon;
    public float numOfGreenlight;

    public AnalyticsEnemy(int instanceId, string name, bool bluelight, bool deadZone, bool weapon, float hp, string status, float harm, float numOfBluelight, float numOfWeapon, float numOfGreenlight)
    {
        this.instanceId = instanceId;
        this.name = name;
        this.bluelight = bluelight;
        this.deadZone = deadZone;
        this.weapon = weapon;
        this.hp = hp;
        this.status = status;
        this.harm = harm;
        this.numOfBluelight = numOfBluelight;
        this.numOfWeapon = numOfWeapon;
        this.numOfGreenlight = numOfGreenlight;
    }







    /**
     "bluelight": false,
        "deadZone": false,
        "enemyname": "e1",
        "harm": 1,
        "status": "live",
        "weapon": true
     */
    public override string ToString()
    {
        return UnityEngine.JsonUtility.ToJson(this, true);
    }
}