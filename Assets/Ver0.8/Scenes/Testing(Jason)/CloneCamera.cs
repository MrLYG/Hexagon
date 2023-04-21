using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;


public class CloneCamera : MonoBehaviour
{
    private GameObject m_Camera;
    private GameObject m_CM;
    private GameObject m_Player;
    private GameObject cameraAnchor;

    // Params for 'm' key functions
    //private GameObject target;
    //private float targetOrthoSize;
    //private float curOrthoSize;

    private float initialOrthoSize;

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

        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            m_Player = gameObject;
        }
        cameraAnchor = m_Player.GetComponent<PlayerZoom>().getCameraAnchor();
    }

    private void Update()
    {
        cameraAnchor.transform.position = Vector3.Lerp(gameObject.transform.position, m_Player.transform.position + new Vector3(0, 2, 0), 0.5f);
        m_Player.GetComponent<PlayerZoom>().zoomOut(Mathf.Max((transform.position - m_Player.transform.position).magnitude / 8f, 1.2f));
    }
}
