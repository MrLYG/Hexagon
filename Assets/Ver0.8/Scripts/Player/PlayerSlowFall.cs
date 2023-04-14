using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlowFall : MonoBehaviour
{
    [SerializeField] private float maxFallingSpeed;
    bool slowFalling = false;

    private void FixedUpdate()
    {
        if (slowFalling)
        {
            if(GetComponent<ObjectGravity>().getCurrentGD() == GravityDirection.Down)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Mathf.Max(GetComponent<Rigidbody2D>().velocity.y, -maxFallingSpeed));
            }else if (GetComponent<ObjectGravity>().getCurrentGD() == GravityDirection.Up)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Mathf.Min(GetComponent<Rigidbody2D>().velocity.y, maxFallingSpeed));
            }
        }
    }

    public void startSlowFall()
    {
        slowFalling = true;
    }

    public void stopSlowFall()
    {
        slowFalling = false;
    }
}
