using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class overallView : MonoBehaviour
{
    private bool camCenter = false;
    [SerializeField] private GameObject m_Camera;
    [SerializeField] private GameObject m_CM;
    [SerializeField] private GameObject target;
    [SerializeField] private float orthoSize;

    [SerializeField] private GameObject m_Player;
    [SerializeField] private float initialOrthoSize;

    // Start is called before the first frame update
    void Start()
    {
        initialOrthoSize = m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            camCenter = true;
        }
        else
        {
            camCenter = false;
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
            m_CM.GetComponent<CinemachineVirtualCamera>().Follow = m_Player.transform;
            m_CM.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = initialOrthoSize;
        }
    }
}
