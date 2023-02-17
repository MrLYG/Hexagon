using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectGravity : MonoBehaviour
{
    private float GravityScale;

    private Rigidbody2D m_rg2d;

    private GravityDirection currentGD = GravityDirection.Down;
    private GravityDirection prevGD = GravityDirection.Down;

    [SerializeField] private float initialGravityScale = 4f;

    // Start is called before the first frame update
    void Start()
    {
        m_rg2d = gameObject.GetComponent<Rigidbody2D>();
        GravityScale = initialGravityScale;
    }

    private void FixedUpdate()
    {
        m_rg2d.AddForce(getCurrentGravity());
        /*
        switch (currentGD)
        {
            case GravityDirection.Down:
                m_rg2d.AddForce(new Vector2(0, GravityScale * -9.8f));
                break;
            case GravityDirection.Up:
                m_rg2d.AddForce(new Vector2(0, GravityScale * 9.8f));
                break;
            case GravityDirection.Left:
                m_rg2d.AddForce(new Vector2(GravityScale * -9.8f, 0));
                break;
            case GravityDirection.Right:
                m_rg2d.AddForce(new Vector2(GravityScale * 9.8f, 0));
                break;
                
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
        */
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
        /*
        switch (currentGD)
        {
            case GravityDirection.Down:
                Physics2D.gravity = new Vector2(0, newGS * -initialGravityScale * 9.8f);
                break;
            case GravityDirection.Up:
                Physics2D.gravity = new Vector2(0, newGS * initialGravityScale * 9.8f);
                break;
            case GravityDirection.Left:
                Physics2D.gravity = new Vector2(newGS * -initialGravityScale * 9.8f, 0);
                break;
            case GravityDirection.Right:
                Physics2D.gravity = new Vector2(newGS * initialGravityScale * 9.8f, 0);
                break;
         }
         */
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
