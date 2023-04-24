using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class PlayerZoom : MonoBehaviour
{
    // References
    //private bool camCenter = false;
    private GameObject m_Camera;
    private GameObject m_CM;
    [SerializeField] private GameObject cameraAnchor;

    // Params for 'm' key functions
    private GameObject target;
    private float targetOrthoSize;
    private float curOrthoSize;

    private float initialOrthoSize;

    // Params for move & zoom function
    [Header("Move & Zoom")]
    [Space]

    [Tooltip("True on/off the function of move & zoom")]
    [SerializeField] private bool movingZoom;

    [Tooltip("Speed to zoom in while moving")]
    [SerializeField] private float movingZoomSpeed;

    [Tooltip("Scale ratio to zoom in while moving")]
    [SerializeField] private float movingZoomRatio;

    public bool initMoving;
    private bool startMoving;
    private Vector3 initalPosAnchor;
    private Vector3 initialLocalPosAnchor;

    void Start()
    {
        // Get reference of camera if we don't already have
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

        // Get initial zoom scale and set up camera to follow player
        initialOrthoSize = m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
        curOrthoSize = initialOrthoSize;
        m_CM.GetComponent<CinemachineVirtualCamera>().Follow = cameraAnchor.transform;
        m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = curOrthoSize;

        if(!PlayerPrefs.HasKey("RespawnX"))
            StartCoroutine("InitMove");
    }

    // Update is called once per frame
    void Update()
    {
        if (initMoving)
        {
            if (startMoving)
            {
                cameraAnchor.transform.position = Vector3.MoveTowards(cameraAnchor.transform.position, initalPosAnchor, 15 * Time.deltaTime);
                if ((cameraAnchor.transform.position - initalPosAnchor).magnitude < 1f)
                {
                    initMoving = false;
                    startMoving = false;
                    cameraAnchor.transform.localPosition = initialLocalPosAnchor;
                }
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            //camCenter = true;

            // Make Important Object Bigger
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Item"))
            {
                gameObject.transform.localScale *= 2;
            }
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Goal"))
            {
                gameObject.transform.localScale *= 2;
            }

            curOrthoSize = targetOrthoSize;
            //m_CM.GetComponent<CinemachineVirtualCamera>().Follow = target.transform;
            m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = curOrthoSize;
        }
        else if (Input.GetKeyUp(KeyCode.M))
        {
            //camCenter = false;

            // Reset Scale of Important Object
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Item"))
            {
                gameObject.transform.localScale /= 2;
            }
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Goal"))
            {
                gameObject.transform.localScale /= 2;
            }

            curOrthoSize = initialOrthoSize;
            //m_CM.GetComponent<CinemachineVirtualCamera>().Follow = gameObject.transform;
            m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = curOrthoSize;
        }

        // Moving will have the camera zoom closer on player
        if (movingZoom)
        {
            if (GetComponent<PlayerControl>().isMoving())
            {
                m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize =
                    Mathf.Lerp(m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize, curOrthoSize * movingZoomRatio, movingZoomSpeed * Time.deltaTime);
            }
            else
            {
                m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize =
                    Mathf.Lerp(m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize, curOrthoSize, movingZoomSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zone"))
        {
            target = collision.gameObject.GetComponent<CameraZone>().cameraAnchor;
            targetOrthoSize = collision.gameObject.GetComponent<CameraZone>().orthoSize;
        }
    }

    public GameObject getCameraAnchor()
    {
        return cameraAnchor;
    }

    public void zoomOut(float ratio)
    {
        curOrthoSize = initialOrthoSize * ratio;
        m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = curOrthoSize;
    }

    IEnumerator InitMove()
    {
        if (GameObject.FindGameObjectWithTag("Door"))
        {
            initMoving = true;
            startMoving = false;
            initalPosAnchor = cameraAnchor.transform.position;
            initialLocalPosAnchor = cameraAnchor.transform.localPosition;
            cameraAnchor.transform.position = GameObject.FindGameObjectWithTag("Door").transform.position;
            yield return new WaitForSeconds(1.0f);
            startMoving = true;
        }
    }
}
