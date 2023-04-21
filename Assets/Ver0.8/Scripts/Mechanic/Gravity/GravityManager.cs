using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public enum GravityDirection { Down, Left, Up, Right };
public class GravityManager : MonoBehaviour
{
    private bool prevCameraFollowing = false;

    private Quaternion targetRotation;

    [Header("Camera")]
    [Space]
    private GameObject CAM;
    [SerializeField] private bool cameraFollowing = false;
    [Range(0, 10f)] [SerializeField] private float cameraRotationSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("MainCamera"))
        {
            if (gameObject.GetComponent<CinemachineVirtualCamera>() != null)
            {
                CAM = gameObject;
            }
        }
        // Rotate Camera
        if (cameraFollowing)
        {
            //CAM.transform.rotation = targetRotation;
            CAM.transform.rotation = Quaternion.Slerp(CAM.transform.rotation, targetRotation, cameraRotationSpeed * Time.deltaTime);
        }
    }

    private GravityDirection findNextGravityDirection(GravityDirection newDirection, GameObject target)
    {
        GravityDirection nextGD = target.GetComponent<ObjectGravity>().getCurrentGD();

        // Find next GD based on current camera following type
        if (cameraFollowing && !prevCameraFollowing)
        {
            // Check rotation angle of CAM to see which GD is the camera at
            float camZ = CAM.transform.rotation.eulerAngles.z;
            if (Mathf.Abs(Mathf.Abs(camZ) - 0) < 10 || Mathf.Abs(Mathf.Abs(camZ) - 0) > 350)
            {
                nextGD = GravityDirection.Down;
            }
            else if (Mathf.Abs(Mathf.Abs(camZ) - 90) < 10 || Mathf.Abs(Mathf.Abs(camZ) - 90) > 350)
            {
                nextGD = GravityDirection.Right;
            }
            else if (Mathf.Abs(Mathf.Abs(camZ) - 180) < 10 || Mathf.Abs(Mathf.Abs(camZ) - 180) > 350)
            {
                nextGD = GravityDirection.Up;
            }
            else if (Mathf.Abs(Mathf.Abs(camZ) - 270) < 10 || Mathf.Abs(Mathf.Abs(camZ) - 270) > 350)
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

    public void SwitchGravityDirection(GravityDirection newDirection, GameObject target)
    {
        ForceSwitchGravityDirection(findNextGravityDirection(newDirection, target), target);
    }

    public void ReverseGravityDirection(GameObject target)
    {
        if (target.CompareTag("Player") || target.CompareTag("Clone"))
        {
            if (target.GetComponent<PlayerBattle>())
            {
                target.GetComponent<PlayerBattle>().ReverseWeapon();
                target.GetComponent<PlayerBattle>().SwitchWeaponSide();
            }
            if (getCameraFollowing())
                target.GetComponent<ObjectGravity>().setPreviousGD(target.GetComponent<ObjectGravity>().getCurrentGD());
            setCameraFollowing(false);
        }
        ForceSwitchGravityDirection(findNextGravityDirection(GravityDirection.Up, target), target);
    }

    public void ForceSwitchGravityDirection(GravityDirection newDirection, GameObject target)
    {
        target.GetComponent<ObjectGravity>().setCurrentGD(newDirection);

        // Switch GD
        target.GetComponent<ObjectGravity>().resetGravityScale();

        /*
        if (target.CompareTag("Player") || target.CompareTag("Clone"))
        {
            // Rotate Camera if needed
            if (cameraFollowing)
            {
                switch (target.GetComponent<ObjectGravity>().getCurrentGD())
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
        }
        */

        /*
        if (target.CompareTag("Player") || target.GetComponent("Clone"))
        {
            // Rotate all spot light to current GD
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("RotationLight"))
            {
                switch (target.GetComponent<ObjectGravity>().getCurrentGD())
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
        }
        */
        
        prevCameraFollowing = cameraFollowing;
    }

    public bool getCameraFollowing()
    {
        return cameraFollowing;
    }
    public void setCameraFollowing(bool newCF)
    {
        cameraFollowing = newCF;
    }
}
