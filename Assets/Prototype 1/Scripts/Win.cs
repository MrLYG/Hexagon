using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class Win : MonoBehaviour
{
    public string level; 
    public GameObject Timerref;
    private readonly string basePath = "https://hexagon-11bf5-default-rtdb.firebaseio.com";
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Win Win");
            generateJson();
        }
    }
    private Model generateJson()
    {
        Model m = new Model();
        m.uid = "22";
        m.level = level;
        m.isWin = true;
        m.runTime = Timerref.GetComponent<Timer>().runningTime;
        Debug.Log(m.ToString());
        RestClient.Post(basePath + "/posts" + ".json", m);
        return null;
    }
}
