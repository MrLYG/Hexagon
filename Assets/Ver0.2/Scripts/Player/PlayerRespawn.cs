using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private GameObject m_GravityManager;

    public void Respawn(GameObject location, GravityDirection gravityDirection)
    {
        if(m_GravityManager == null)
        {
            // Get referance of Gravity Manager
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("GravityManager"))
            {
                m_GravityManager = gameObject;
            }
        }
        // Respawn player by setting assigned position and GD
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = location.transform.position;
        m_GravityManager.GetComponent<GravityManager>().ForceSwitchGravityDirection(gravityDirection, gameObject);
    }
}
