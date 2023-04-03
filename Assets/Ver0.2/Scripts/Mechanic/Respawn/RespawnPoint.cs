using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject m_RespawnManager;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private GameObject m_Light;
    public List<GameObject> Enemies;
    public List<GameObject> Objects;
    public List<Vector3> ObjectPositions;
    public GravityDirection gravityDirection;

    private void Start()
    {
        // Get reference of RespawnManager
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("RespawnManager"))
        {
            m_RespawnManager = gameObject;
        }

        ObjectPositions = new List<Vector3>();
        foreach (GameObject obj in Objects)
        {
            ObjectPositions.Add(obj.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player activate the checkpoint by touching it
        if (collision.gameObject.CompareTag("Player"))
        {
            m_RespawnManager.GetComponent<RespawnManager>().SetCheckPoint(gameObject);
            Activate();

            // Record the currently triggered checkpoint
            Analytics.curCP = gameObject;
            Analytics.curCP.GetComponent<CheckPointTrack>().isPass = true;
            
        }
    }

    private void Activate() {
        m_Animator.SetBool("Activated", true);
        m_Light.SetActive(true);
    }

    public void DeActivate()
    {
        m_Animator.SetBool("Activated", false);
        m_Light.SetActive(false);
    }
}
