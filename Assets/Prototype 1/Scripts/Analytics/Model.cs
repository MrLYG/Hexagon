using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Model
{
    public string uid;
    public string level;
    public string collectDate;
    public bool isWin;
    public float runTime;
    public float beHits;
    public Dictionary<string, AnalyticsEnemy> analyticsEnemiesDict;
    public Dictionary<string, AnalyticsCheckPoint> analyticsCheckpointsDict;
    public override string ToString()
    {
        //return UnityEngine.JsonUtility.ToJson(this, true);
        return JsonConvert.SerializeObject(this);
    }

    
}

