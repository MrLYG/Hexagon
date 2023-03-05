using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectGravity : MonoBehaviour
{
    private float GravityScale;

    [Tooltip("Reference of Gravity Manager")]
    [SerializeField] private GravityManager m_GravityManager;

    [Tooltip("Reference of rg2d")]
    private Rigidbody2D m_rg2d;

    private GravityDirection currentGD = GravityDirection.Down;
    private GravityDirection prevGD = GravityDirection.Down;

    [Tooltip("The base gravity scale for the object")]
    [SerializeField] private float initialGravityScale = 4f;

    // Start is called before the first frame update
    void Start()
    {
        m_rg2d = gameObject.GetComponent<Rigidbody2D>();
        m_rg2d.gravityScale = 0;

        GravityScale = initialGravityScale;

        if (m_GravityManager == null)
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("GravityManager"))
            {
                m_GravityManager = gameObject.GetComponent<GravityManager>();
            }
        }
    }

    private void FixedUpdate()
    {
        m_rg2d.AddForce(getCurrentGravity());
    }

    public void ReverseGravityDirection()
    {
        m_GravityManager.ReverseGravityDirection(gameObject);
    }

    public Vector2 getCurrentGravity()
    {
        switch (currentGD)
        {
            case GravityDirection.Down:
                return new Vector2(0, GravityScale * -9.8f);
            case GravityDirection.Up:
                return new Vector2(0, GravityScale * 9.8f);
            case GravityDirection.Left:
                return new Vector2(GravityScale * -9.8f, 0);
            case GravityDirection.Right:
                return new Vector2(GravityScale * 9.8f, 0);
            default:
                return new Vector2(0, GravityScale * -9.8f);
        }
    }

    // Change GS to given scale
    public void changeGravityScale(float newGS, GameObject target)
    {
        GravityScale = newGS;
    }

    // Change GS to initial scale based on current GD
    public void resetGravityScale()
    {
        GravityScale = initialGravityScale;
        if (gameObject.CompareTag("Player"))
        {
            // Rotate Player according to current GD
            gameObject.GetComponent<PlayerControl>().RotatePlayerWithGD();
        }
    }

    public GravityDirection getCurrentGD()
    {
        return currentGD;
    }
    public GravityDirection getPreviousGD()
    {
        return prevGD;
    }
    public void setPreviousGD(GravityDirection newPrevGD)
    {
        prevGD = newPrevGD;
    }

    public void setCurrentGD(GravityDirection newCurGD)
    {
        currentGD = newCurGD;
    }
}
