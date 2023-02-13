using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class overallView : MonoBehaviour
{
    private bool camCenter = false;
    [SerializeField] private GameObject m_Camera;
    [SerializeField] private GameObject m_CM;

    private GameObject target;
    private float orthoSize;

    [SerializeField] private float initialOrthoSize;

    // Start is called before the first frame update
    void Start()
    {
        initialOrthoSize = m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            camCenter = true;

            //gameObject.transform.localScale *= 1.5f;
            //gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            //gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            //gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //transform.Find("Canvas").gameObject.SetActive(true);

            // Make Important Object Bigger
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Item"))
            {
                gameObject.transform.localScale *= 2;
            }
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Goal"))
            {
                gameObject.transform.localScale *= 2;
            }
        }
        else if(Input.GetKeyUp(KeyCode.M))
        {
            camCenter = false;

            //gameObject.transform.localScale /= 1.5f;
            //gameObject.GetComponent<Rigidbody2D>().gravityScale = 4;
            //gameObject.GetComponent<CircleCollider2D>().enabled = true;
            //transform.Find("Canvas").gameObject.SetActive(false);

            // Reset Scale of Important Object
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Item"))
            {
                gameObject.transform.localScale /= 2;
            }
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Goal"))
            {
                gameObject.transform.localScale /= 2;
            }
        } 
    }

    void FixedUpdate()
    {
        if (camCenter)
        {
            m_CM.GetComponent<CinemachineVirtualCamera>().Follow = target.transform;
            m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = orthoSize;
        }
        else
        {
            m_CM.GetComponent<CinemachineVirtualCamera>().Follow = gameObject.transform;
            m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = initialOrthoSize;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zone"))
        {
            target = collision.gameObject.GetComponent<CameraZone>().cameraAnchor;
            orthoSize = collision.gameObject.GetComponent<CameraZone>().orthoSize;
        }
    }
}
