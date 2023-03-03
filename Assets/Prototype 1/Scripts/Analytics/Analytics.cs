using System.Collections.Generic;
using UnityEngine;

public class Analytics : MonoBehaviour
{

    public float runningTime = 0.0f;
    public float beHits = 0.0f;
    

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



   
}
