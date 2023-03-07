using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Tooltip("Reference of GravityManager")]
    [SerializeField] private GameObject m_GravityManager;

    [Tooltip("Time to be free of reversing light")]
    [SerializeField] private float InvincibleTime = 3f;
    public bool Invincible = false;

    public void Respawn(GameObject location, GravityDirection gravityDirection)
    {
        if(m_GravityManager == null)
        {
            // Get referance of Gravity Manager
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("GravityManager"))
            {
                m_GravityManager = gameObject;
            }
        }
        // Respawn player by setting assigned position and GD
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = location.transform.position;
        m_GravityManager.GetComponent<GravityManager>().ForceSwitchGravityDirection(gravityDirection, gameObject);

        // Reset player HP
        GetComponent<PlayerHP>().setHP();
        GetComponent<SpriteRenderer>().color = Color.white;

        // Reset weapon status
        GetComponent<PlayerBattle>().ResetWeapon();

        // Set Invincible
        Invincible = true;
        Invoke("resetInvincible", InvincibleTime);

        // Reset green light
        foreach(GameObject gl in GameObject.FindGameObjectsWithTag("GreenLight"))
        {
            gl.GetComponent<ILight>().RemoveLight();
        }
        foreach(GameObject bl in GameObject.FindGameObjectsWithTag("ReverseLight"))
        {
            bl.GetComponent<ILight>().RemoveLight();
        }
        GetComponent<PlayerSlowFall>().stopSlowFall();
        GetComponent<PlayerSpecialBullet>().resetCD();
    }

    private void resetInvincible()
    {
        Invincible = false;
    }
}
