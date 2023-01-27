using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement")]
    [Space]
    [SerializeField] private float walkSpeed;
    [Range(0, .3f)] [SerializeField] private float smoothMovementTime = 0.05f;
    //[SerializeField] private float initialJumpVelocity = 10f;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;

    [SerializeField] GravityManager m_GravityManager;

    private Vector3 velocity = Vector3.zero;

    //[SerializeField] private float speed;
    // Start is called before the first frame update

    private float dirt;
    private bool jump;
    private bool grounded;

    // Inner CD for switching gravity
    private bool canSwitchGD = true;
    [SerializeField] private float GDSwitchCD = 0.5f;
    private float GDSwitchCount = 0f;

    //private float currentJumpVelocity = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) 
        {
            dirt = -1.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dirt = 1.0f;
        }
        else {
            dirt = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        // Player Input to change GD
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            if (canSwitchGD) 
            {
                bool playerInsideLight = false;
                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("SpotLight"))
                {
                    if (gameObject.GetComponent<Gravity_SpotLight>().isPlayerInside()) {
                        playerInsideLight = true;
                    }
                }
                if (playerInsideLight)
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        m_GravityManager.SwitchGravityDirection(GravityDirection.Left);
                    }
                    else
                    {
                        m_GravityManager.SwitchGravityDirection(GravityDirection.Right);
                    }
                    canSwitchGD = false;
                }
            }
        }

        // Gravity Switching CD
        if (!canSwitchGD)
        {
            GDSwitchCount += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!canSwitchGD && GDSwitchCount > GDSwitchCD) {
                    canSwitchGD = true;
                    GDSwitchCount = 0;
                }
            }
        }

        GravityDirection currentGD = m_GravityManager.getCurrentGD();
        if (jump && grounded)
        {
            switch (currentGD) {
                case GravityDirection.Down:
                    m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
                    break;
                case GravityDirection.Up:
                    m_Rigidbody2D.AddForce(new Vector2(0f, -jumpForce));
                    break;
                case GravityDirection.Left:
                    m_Rigidbody2D.AddForce(new Vector2(jumpForce, 0f));
                    break;
                case GravityDirection.Right:
                    m_Rigidbody2D.AddForce(new Vector2(-jumpForce, 0f));
                    break;
                default:
                    m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
                    break;
            }
        }
        jump = false;

        Vector3 targetVelocity = Vector3.zero;
        switch (currentGD)
        {
            case GravityDirection.Down:
                targetVelocity = new Vector2(dirt * walkSpeed * Time.deltaTime * 10f, m_Rigidbody2D.velocity.y);
                break;
            case GravityDirection.Up:
                targetVelocity = new Vector2(-dirt * walkSpeed * Time.deltaTime * 10f, m_Rigidbody2D.velocity.y);
                break;
            case GravityDirection.Left:
                targetVelocity = new Vector2(m_Rigidbody2D.velocity.x , -dirt * walkSpeed * Time.deltaTime * 10f);
                break;
            case GravityDirection.Right:
                targetVelocity = new Vector2(m_Rigidbody2D.velocity.x, dirt * walkSpeed * Time.deltaTime * 10f);
                break;
            default:
                targetVelocity = new Vector2(dirt * walkSpeed * Time.deltaTime * 10f, m_Rigidbody2D.velocity.y);
                break;
        }
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, smoothMovementTime);

        /*
        if (jump && grounded)
        {
            currentJumpVelocity = initialJumpVelocity;
        }
        jump = false;

        switch (currentGD) {
            case GravityDirection.Down:
                transform.position += new Vector3(dist * walkSpeed * Time.deltaTime, currentJumpVelocity * Time.deltaTime, 0);
                break;
            case GravityDirection.Up:
                transform.position += new Vector3(-dist * walkSpeed * Time.deltaTime, -currentJumpVelocity * Time.deltaTime, 0);
                break;
            case GravityDirection.Left:
                transform.position += new Vector3(currentJumpVelocity * Time.deltaTime, -dist * walkSpeed * Time.deltaTime, 0);
                break;
            case GravityDirection.Right:
                transform.position += new Vector3(-currentJumpVelocity * Time.deltaTime, dist * walkSpeed * Time.deltaTime, 0);
                break;
            default:
                transform.position += new Vector3(dist * walkSpeed * Time.deltaTime, currentJumpVelocity * Time.deltaTime, 0);
                break;
        }

        if (currentJumpVelocity > 0) {
            currentJumpVelocity -= gravityScale * Time.deltaTime;
            if (currentJumpVelocity < 0) currentJumpVelocity = 0;
        }
        if (!grounded && currentJumpVelocity > -gravityScale) {
            currentJumpVelocity -= gravityScale * Time.deltaTime;
            if (currentJumpVelocity < -gravityScale) currentJumpVelocity = -gravityScale;
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            //currentJumpVelocity = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GravityDirection currentGD = m_GravityManager.getCurrentGD();
        if (collision.gameObject.CompareTag("Gate")) {
            bool cameraFollowing = m_GravityManager.getCameraFollowing();
            if (cameraFollowing)
            {
                switch (currentGD)
                {
                    case GravityDirection.Down:
                        m_GravityManager.SwitchGravityDirection(GravityDirection.Right);
                        break;
                    case GravityDirection.Up:
                        m_GravityManager.SwitchGravityDirection(GravityDirection.Left);
                        break;
                    case GravityDirection.Left:
                        m_GravityManager.SwitchGravityDirection(GravityDirection.Up);
                        break;
                    case GravityDirection.Right:
                        m_GravityManager.SwitchGravityDirection(GravityDirection.Down);
                        break;
                }
            }
            else {
                m_GravityManager.SwitchGravityDirection(GravityDirection.Right);
            }
        }
    }
    public void switchGroundCheckPosition()
    {
        GravityDirection currentGD = m_GravityManager.getCurrentGD();
        switch (currentGD)
        {
            case GravityDirection.Down:
                groundCheck.position = transform.position + new Vector3(0, -0.4f, 0);
                break;
            case GravityDirection.Up:
                groundCheck.position = transform.position + new Vector3(0, 0.4f, 0);
                break;
            case GravityDirection.Left:
                groundCheck.position = transform.position + new Vector3(-0.4f, 0, 0);
                break;
            case GravityDirection.Right:
                groundCheck.position = transform.position + new Vector3(0.4f, 0, 0);
                break;
            default:
                groundCheck.position = transform.position + new Vector3(0, -0.4f, 0);
                break;
        }
    }

    /*
    private void SwitchGravityDirection(GravityDirection newDirection) {
        currentGD = newDirection;
        currentJumpVelocity = -gravityScale / 2;
        switch (currentGD)
        {
            case GravityDirection.Down:
                groundCheck.position = transform.position + new Vector3(0, -0.4f, 0);
                break;
            case GravityDirection.Up:
                groundCheck.position = transform.position + new Vector3(0, 0.4f, 0);
                break;
            case GravityDirection.Left:
                groundCheck.position = transform.position + new Vector3(-0.4f, 0, 0);
                break;
            case GravityDirection.Right:
                groundCheck.position = transform.position + new Vector3(0.4f, 0, 0);
                break;
            default:
                groundCheck.position = transform.position + new Vector3(0, -0.4f, 0);
                break;
        }
    }
    */
}