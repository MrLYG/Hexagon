using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D.velocity = new Vector2(4, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
