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

    [SerializeField] private float initialGravityScale = 1f;

    [Header("Camera")]
    [Space]
    [SerializeField] private GameObject CAM;
    [SerializeField] private bool cameraFollowing = false;
    [Range(0, 10f)] [SerializeField] private float cameraRotationSpeed = 10f;

    private bool prevCameraFollowing = false;

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

    private GravityDirection findNextGravityDirection(GravityDirection newDirection) {
        GravityDirection nextGD = currentGD;

        // Find next GD based on current camera following type
        if (cameraFollowing && !prevCameraFollowing)
        {
            // Check rotation angle of CAM to see which GD is the camera at
            float camZ = CAM.transform.rotation.eulerAngles.z;
            if (Mathf.Abs(camZ - 0) < 10 || Mathf.Abs(camZ - 0) > 350)
            {
                nextGD = GravityDirection.Down;
            }
            else if (Mathf.Abs(camZ - 90) < 10 || Mathf.Abs(camZ - 90) > 350)
            {
                nextGD = GravityDirection.Right;
            }
            else if (Mathf.Abs(camZ - 180) < 10 || Mathf.Abs(camZ - 180) > 350)
            {
                nextGD = GravityDirection.Up;
            }
            else if (Mathf.Abs(camZ - 360) < 10 || Mathf.Abs(camZ - 360) > 350)
            {
                nextGD = GravityDirection.Left;
            }
        }
        switch (newDirection)
        {
            case GravityDirection.Down:
                break;
            case GravityDirection.Up:
                switch (nextGD)
                {
                    case GravityDirection.Down:
                        nextGD = GravityDirection.Up;
                        break;
                    case GravityDirection.Up:
                        nextGD = GravityDirection.Down;
                        break;
                    case GravityDirection.Left:
                        nextGD = GravityDirection.Right;
                        break;
                    case GravityDirection.Right:
                        nextGD = GravityDirection.Left;
                        break;
                }
                break;
            case GravityDirection.Left:
                if (nextGD == GravityDirection.Right)
                    nextGD = GravityDirection.Down;
                else
                    nextGD++;
                break;
            case GravityDirection.Right:
                if (nextGD == GravityDirection.Down)
                    nextGD = GravityDirection.Right;
                else
                    nextGD--;
                break;
        }
        return nextGD;
    }
    public void SwitchGravityDirection(GravityDirection newDirection)
    {
        ForceSwitchGravityDirection(findNextGravityDirection(newDirection));
    }

    public void ReverseGravityDirection() {
        if(getCameraFollowing())
            prevGD = currentGD;
        setCameraFollowing(false);
        ForceSwitchGravityDirection(findNextGravityDirection(GravityDirection.Up));
    }

    public void ForceSwitchGravityDirection(GravityDirection newDirection) {
        currentGD = newDirection;

        // Switch GD
        resetGravityScale();

        // Swith Player's ground check to according GD
        m_PlayerControl.switchGroundCheckPosition();
        m_PlayerControl.rotateText();

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
            }
        }

        // Rotate all spot light to current GD
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("RotationLight"))
        {
            switch (currentGD)
            {
                case GravityDirection.Down:
                    gameObject.GetComponent<RotationLight>().rotateLight(Quaternion.Euler(0, 0, 0));
                    break;
                case GravityDirection.Up:
                    gameObject.GetComponent<RotationLight>().rotateLight(Quaternion.Euler(0, 0, 180));
                    break;
                case GravityDirection.Left:
                    gameObject.GetComponent<RotationLight>().rotateLight(Quaternion.Euler(0, 0, -90));
                    break;
                case GravityDirection.Right:
                    gameObject.GetComponent<RotationLight>().rotateLight(Quaternion.Euler(0, 0, 90));
                    break;
            }
        }

        prevCameraFollowing = cameraFollowing;
    }

    // Change GS to given scale
    public void changeGravityScale(float newGS)
    {
        switch (currentGD)
        {
            case GravityDirection.Down:
                Physics2D.gravity = new Vector2(0, newGS * - initialGravityScale * 9.8f);
                break;
            case GravityDirection.Up:
                Physics2D.gravity = new Vector2(0, newGS * initialGravityScale * 9.8f);
                break;
            case GravityDirection.Left:
                Physics2D.gravity = new Vector2(newGS  * - initialGravityScale * 9.8f, 0);
                break;
            case GravityDirection.Right:
                Physics2D.gravity = new Vector2(newGS * initialGravityScale * 9.8f, 0);
                break;
        }
    }

    // Change GS to initial scale based on current GD
    public void resetGravityScale()
    {
        float drag = m_Player.GetComponent<Rigidbody2D>().drag;
        m_Player.GetComponent<Rigidbody2D>().drag = 0;
        switch (currentGD)
        {
            case GravityDirection.Down:
                Physics2D.gravity = new Vector2(0, -initialGravityScale * 9.8f);
                break;
            case GravityDirection.Up:
                Physics2D.gravity = new Vector2(0, initialGravityScale * 9.8f);
                break;
            case GravityDirection.Left:
                Physics2D.gravity = new Vector2(-initialGravityScale * 9.8f, 0);
                break;
            case GravityDirection.Right:
                Physics2D.gravity = new Vector2(initialGravityScale * 9.8f, 0);
                break;
        }
        m_Player.GetComponent<Rigidbody2D>().drag = drag;
    }

    public GravityDirection getCurrentGD()
    {
        return currentGD;
    }
    public GravityDirection getPreviousGD()
    {
        return prevGD;
    }
    public bool getCameraFollowing() {
        return cameraFollowing;
    }
    public void setCameraFollowing(bool newCF) {
        cameraFollowing = newCF;
    }
}
