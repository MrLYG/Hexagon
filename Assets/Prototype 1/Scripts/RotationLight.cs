using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLight : MonoBehaviour
{
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D m_SpotLight;
    [Range(0, 10f)] [SerializeField] private float rotationSpeed = 7f;
    [SerializeField] private float detectionOffset = 3f;

    private Quaternion targetAngle;
    bool rotating;

    private GameObject m_Player;
    private PlayerControl m_PlayerControl;

    [SerializeField] private LayerMask blockingLayer;

    // Start is called before the first frame update
    void Start()
    {
        rotating = false;
        targetAngle = Quaternion.Euler(0, 0, 0);

        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            m_Player = gameObject;
        }
        m_PlayerControl = m_Player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, rotationSpeed * Time.deltaTime);
            if (transform.rotation.Equals(targetAngle)) rotating = false;
        }

        // Just for debugging
        if (Input.GetKeyDown(KeyCode.T))
        {
            isPlayerInside();
        }
    }

    public void rotateLight(Quaternion TargetAngle) {
        targetAngle = TargetAngle;
        rotating = true;
    }

    public bool isPlayerInside()
    {
        // Check if player is with radian of the light
        Vector2 directToPlayer = m_Player.transform.position - transform.position;
        float distToPlayer = directToPlayer.magnitude;
        directToPlayer.Normalize();
        
        float angle = Mathf.Acos(directToPlayer.x) * Mathf.Rad2Deg;
        if(directToPlayer.y < 0)
        {
            angle *= -1.0f;
        }
        angle += 180;

        float lightAngleRange = m_SpotLight.pointLightInnerAngle / 2;
        float lightAngle = transform.rotation.eulerAngles.z;

        // Special cases
        // Passing the line of angle 0 & 360
        if (angle < 45) angle += 360;
        // Not fully rotated to 0
        if (Mathf.Abs(lightAngle - 360) < 10) lightAngle -= 360;

        float angleLowerBound = lightAngle - lightAngleRange + 90;
        float angleUpperBound = lightAngle + lightAngleRange + 90;

        //Debug.Log(angle + " " + angleLowerBound + " " + angleUpperBound + " " + transform.rotation.eulerAngles.z);

        // Check if player is within the range of the light
        if (angle >= angleLowerBound - detectionOffset && angle <= angleUpperBound + detectionOffset)
        {
            // Raycast to see if there're obstacles blocking the light
            RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, directToPlayer, m_SpotLight.pointLightOuterRadius, blockingLayer);
            
            Debug.DrawRay(transform.position, directToPlayer * m_SpotLight.pointLightOuterRadius, Color.green, 1);

            if (hitPlayer.collider != null && hitPlayer.collider.gameObject.CompareTag("Player"))
            {
                //Debug.Log("Inside");
                return true;
            }
            /*
            // We can check all 4 corner to get more preceise outcome
            directToPlayer = m_Player.transform.position - transform.position + new Vector3(0.35f, 0.35f, 0);
            distToPlayer = directToPlayer.magnitude;
            directToPlayer.Normalize();
            RaycastHit2D hitPlayer2 = Physics2D.Raycast(transform.position, directToPlayer, distToPlayer + 0.5f, blockingLayer);
            Debug.DrawRay(transform.position, directToPlayer * (distToPlayer + 0.5f), Color.red, 1);

            directToPlayer = m_Player.transform.position - transform.position + new Vector3(0.35f, -0.35f, 0);
            distToPlayer = directToPlayer.magnitude;
            directToPlayer.Normalize();
            RaycastHit2D hitPlayer3 = Physics2D.Raycast(transform.position, directToPlayer, distToPlayer + 0.5f, blockingLayer);
            Debug.DrawRay(transform.position, directToPlayer * (distToPlayer + 0.5f), Color.red, 1);

            directToPlayer = m_Player.transform.position - transform.position + new Vector3(-0.35f, 0.35f, 0);
            distToPlayer = directToPlayer.magnitude;
            directToPlayer.Normalize();
            RaycastHit2D hitPlayer4 = Physics2D.Raycast(transform.position, directToPlayer, distToPlayer + 0.5f, blockingLayer);
            Debug.DrawRay(transform.position, directToPlayer * (distToPlayer + 0.5f), Color.red, 1);

            directToPlayer = m_Player.transform.position - transform.position + new Vector3(-0.35f, -0.35f, 0);
            distToPlayer = directToPlayer.magnitude;
            directToPlayer.Normalize();
            RaycastHit2D hitPlayer5 = Physics2D.Raycast(transform.position, directToPlayer, distToPlayer + 0.5f, blockingLayer);
            Debug.DrawRay(transform.position, directToPlayer * (distToPlayer + 0.5f), Color.red, 1);

            if (hitPlayer.collider.gameObject.CompareTag("Player") || hitPlayer2.collider.gameObject.CompareTag("Player")
                || hitPlayer3.collider.gameObject.CompareTag("Player") || hitPlayer4.collider.gameObject.CompareTag("Player") 
                || hitPlayer5.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Inside");
                return true;
            }
            */
        }
        return false;
    }
}
