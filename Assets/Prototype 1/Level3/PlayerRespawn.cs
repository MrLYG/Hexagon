using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private GameObject m_GravityManager;
    // Start is called before the first frame update
    private void Start()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("GravityManager"))
        {
            m_GravityManager = gameObject;
        }
    }
    public void Respawn(GameObject location, GravityDirection gravityDirection)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = location.transform.position;
        m_GravityManager.GetComponent<GravityManager>().ForceSwitchGravityDirection(gravityDirection);
    }
}
