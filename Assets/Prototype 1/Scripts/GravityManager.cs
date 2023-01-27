using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GravityDirection { Down, Left, Up, Right };
public class GravityManager : MonoBehaviour
{
    private GameObject m_Player;
    private PlayerControl m_PlayerControl;
    
    private GravityDirection currentGD = GravityDirection.Down;
    private GravityDirection prevGD = GravityDirection.Down;

    [SerializeField] private float gravityScale = 1f;

    [Header("Camera")]
    [Space]
    [SerializeField] private GameObject CAM;
    [SerializeField] private bool cameraFollowing = false;
    [Range(0, 10f)] [SerializeField] private float cameraRotationSpeed = 10f;

    private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player")) {
            m_Player = gameObject;
        }
        m_PlayerControl = m_Player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate Camera
        if (cameraFollowing)
        {
            //CAM.transform.rotation = targetRotation;
            CAM.transform.rotation = Quaternion.Slerp(CAM.transform.rotation, targetRotation, cameraRotationSpeed * Time.deltaTime);
        }
    }

    public void SwitchGravityDirection(GravityDirection newDirection)
    {
        // Find next GD based on current camera following type
        prevGD = currentGD;
        if (!cameraFollowing)
        {
            currentGD = newDirection;
        }
        else
        {
            switch (newDirection)
            {
                case GravityDirection.Down:
                    break;
                case GravityDirection.Up:
                    switch (currentGD)
                    {
                        case GravityDirection.Down:
                            currentGD = GravityDirection.Up;
                            break;
                        case GravityDirection.Up:
                            currentGD = GravityDirection.Down;
                            break;
                        case GravityDirection.Left:
                            currentGD = GravityDirection.Right;
                            break;
                        case GravityDirection.Right:
                            currentGD = GravityDirection.Left;
                            break;
                    }
                    break;
                case GravityDirection.Left:
                    if (currentGD == GravityDirection.Right)
                        currentGD = GravityDirection.Down;
                    else
                        currentGD++;
                    break;
                case GravityDirection.Right:
                    if (currentGD == GravityDirection.Down)
                        currentGD = GravityDirection.Right;
                    else
                        currentGD--;
                    break;
                default:
                    break;
            }
        }

        // Switch GD
        switch (currentGD)
        {
            case GravityDirection.Down:
                Physics2D.gravity = new Vector2(0, -gravityScale * 9.8f);
                break;
            case GravityDirection.Up:
                Physics2D.gravity = new Vector2(0, gravityScale * 9.8f);
                break;
            case GravityDirection.Left:
                Physics2D.gravity = new Vector2(-gravityScale * 9.8f, 0);
                break;
            case GravityDirection.Right:
                Physics2D.gravity = new Vector2(gravityScale * 9.8f, 0);
                break;
            default:
                Physics2D.gravity = new Vector2(0, -gravityScale * 9.8f);
                break;
        }

        // Swith Player's ground check to according GD
        m_PlayerControl.switchGroundCheckPosition();

        // Rotate Camera if needed
        if (cameraFollowing)
        {
            switch (currentGD)
            {
                case GravityDirection.Down:
                    targetRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case GravityDirection.Up:
                    targetRotation = Quaternion.Euler(0, 0, 180);
                    break;
                case GravityDirection.Left:
                    targetRotation = Quaternion.Euler(0, 0, -90);
                    break;
                case GravityDirection.Right:
                    targetRotation = Quaternion.Euler(0, 0, 90);
                    break;
                default:
                    targetRotation = Quaternion.Euler(0, 0, 0);
                    break;
            }
        }

        // Rotate all spot light to current GD
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("SpotLight"))
        {
            switch (currentGD)
            {
                case GravityDirection.Down:
                    gameObject.GetComponent<Gravity_SpotLight>().rotateLight(Quaternion.Euler(0, 0, 0));
                    break;
                case GravityDirection.Up:
                    gameObject.GetComponent<Gravity_SpotLight>().rotateLight(Quaternion.Euler(0, 0, 180));
                    break;
                case GravityDirection.Left:
                    gameObject.GetComponent<Gravity_SpotLight>().rotateLight(Quaternion.Euler(0, 0, -90));
                    break;
                case GravityDirection.Right:
                    gameObject.GetComponent<Gravity_SpotLight>().rotateLight(Quaternion.Euler(0, 0, 90));
                    break;
                default:
                    gameObject.GetComponent<Gravity_SpotLight>().rotateLight(Quaternion.Euler(0, 0, 0));
                    break;
            }
        }
    }


    public GravityDirection getCurrentGD()
    {
        return currentGD;
    }
    public bool getCameraFollowing() {
        return cameraFollowing;
    }
}
