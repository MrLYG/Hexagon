using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class PlayerZoom : MonoBehaviour
{
    private bool camCenter = false;
    private GameObject m_Camera;
    private GameObject m_CM;

    private GameObject target;
    private float orthoSize;

    [SerializeField] private float initialOrthoSize;

    //[SerializeField] private float movingZoomSpeed;
    //[SerializeField] private float movingZoomRatio;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Camera == null || m_CM == null)
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("MainCamera"))
            {
                if (gameObject.GetComponent<CinemachineVirtualCamera>() != null)
                {
                    m_CM = gameObject;
                }
                else
                {
                    m_Camera = gameObject;
                }
            }
        }
        initialOrthoSize = m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            camCenter = true;

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
        else if (Input.GetKeyUp(KeyCode.M))
        {
            camCenter = false;

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

        /*
        if (GetComponent<Rigidbody2D>().velocity.magnitude > 0)
        {
            m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize =
                Mathf.Lerp(m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize, initialOrthoSize * movingZoomRatio, movingZoomSpeed * Time.deltaTime);
        }
        else
        {
            m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize =
                Mathf.Lerp(m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize, initialOrthoSize, movingZoomSpeed * Time.deltaTime);
        }
        */
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
