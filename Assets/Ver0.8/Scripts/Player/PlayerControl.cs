using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement")]
    [Space]

    [Tooltip("Walking speed")]
    [SerializeField] private float initWalkSpeed;
    private float walkSpeed;

    [Tooltip("Moving smoothing, high value will cause player to slide")]
    [Range(0, .3f)] [SerializeField] private float smoothMovementTime = 0.05f;

    [Tooltip("Jumping force")]
    [SerializeField] private float jumpForce = 400f;

    [Tooltip("Ground check object reference")]
    [SerializeField] private Transform m_groundCheck;

    [Tooltip("Layers to be consider ground")]
    [SerializeField] private LayerMask groundLayer;

    [Tooltip("rb2d reference")]
    [SerializeField] private Rigidbody2D m_Rigidbody2D;

    [Tooltip("Reference of Gravity Manager")]
    [SerializeField] private GravityManager m_GravityManager;

    private Vector3 velocity = Vector3.zero;

    // Params used for basic movements
    private float dirt;
    private bool jump;
    private bool grounded;

    // Params for gravity related operation
    private bool canSwitchGD = true;
    [Tooltip("Cooldown time for switching gravity direction")]
    [SerializeField] private float GDSwitchCD = 0.5f;
    private float GDSwitchCount = 0f;

    [Tooltip("Layers to be consider light")]
    [SerializeField] private LayerMask lightLayer;

    private ObjectGravity m_ObjectGravity;

    // Params for player orientation (Useful for setting weapon orientation)
    public bool facingRight = true;
    private bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        m_ObjectGravity = GetComponent<ObjectGravity>();
        walkSpeed = initWalkSpeed;

        if (m_GravityManager == null)
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("GravityManager"))
            {
                m_GravityManager = gameObject.GetComponent<GravityManager>();
            }
        }

        if (m_groundCheck == null)
        {
            m_groundCheck = transform.Find("GroundCheck");
        }
    }

    void Update()
    {
        // Gravity Switching CD
        if (!canSwitchGD)
        {
            GDSwitchCount += Time.deltaTime;
        }

        // Stop movement when at overallview
        if (GetComponent<PlayerZoom>() && Input.GetKey(KeyCode.M))
        {
            return;
        }

        // Movement inputs
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
        {
            dirt = -1.0f;
            moving = true;
            if (facingRight && !GetComponent<PlayerPushPull>().pushing)
            {
                facingRight = !facingRight;
                GetComponent<PlayerBattle>().SwitchWeaponSide();
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            dirt = 1.0f;
            moving = true;
            if (!facingRight && !GetComponent<PlayerPushPull>().pushing)
            {
                facingRight = !facingRight;
                GetComponent<PlayerBattle>().SwitchWeaponSide();
            }
        }
        else {
            moving = false;
            dirt = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
        }

        // Flip player sprite
        if (facingRight)
        {
            if(GetComponent<ObjectGravity>().getCurrentGD() == GravityDirection.Down)
                GetComponent<SpriteRenderer>().flipX = true;
            else
                GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            if (GetComponent<ObjectGravity>().getCurrentGD() == GravityDirection.Down)
                GetComponent<SpriteRenderer>().flipX = false;
            else
                GetComponent<SpriteRenderer>().flipX = true;
        }

        // Player Input to change GD : Rotation
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            if (canSwitchGD) 
            {
                bool playerInsideLight = false;
                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("RotationLight"))
                {
                    if (gameObject.GetComponent<RotationLight>().isPlayerInside()) {
                        playerInsideLight = true;
                    }
                }
                if (playerInsideLight)
                {
                    m_GravityManager.setCameraFollowing(true);
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        m_GravityManager.SwitchGravityDirection(GravityDirection.Left, gameObject);
                    }
                    else
                    {
                        m_GravityManager.SwitchGravityDirection(GravityDirection.Right, gameObject);
                    }
                    canSwitchGD = false;
                }
            }
        }

        // Reverse
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (canSwitchGD)
            {
                bool inLight = false;
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f, lightLayer);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject.CompareTag("ReverseLight"))
                    {
                        inLight = true;
                    }
                }
                if (inLight)
                {
                    m_GravityManager.ReverseGravityDirection(gameObject);
                    canSwitchGD = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_groundCheck.position, 0.2f, groundLayer);
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

        GravityDirection currentGD = m_ObjectGravity.getCurrentGD();
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

        Vector3 targetVelocity;
        if (!m_GravityManager.getCameraFollowing())
        {
            currentGD = m_ObjectGravity.getPreviousGD();
        }

        switch (currentGD)
        {
            case GravityDirection.Down:
                targetVelocity = new Vector2(dirt * walkSpeed * Time.deltaTime * 10f, m_Rigidbody2D.velocity.y);
                break;
            case GravityDirection.Up:
                targetVelocity = new Vector2(-dirt * walkSpeed * Time.deltaTime * 10f, m_Rigidbody2D.velocity.y);
                break;
            case GravityDirection.Left:
                targetVelocity = new Vector2(m_Rigidbody2D.velocity.x, -dirt * walkSpeed * Time.deltaTime * 10f);
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
    }

    public void changeWalkSpeed(float ratio) {
        walkSpeed = initWalkSpeed * ratio;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GravityDirection currentGD = m_ObjectGravity.getCurrentGD();

        if (collision.gameObject.CompareTag("Gate"))
        {
            m_GravityManager.setCameraFollowing(collision.gameObject.GetComponent<ModifyGravityGate>().cameraFollowing);
            m_GravityManager.ForceSwitchGravityDirection(collision.gameObject.GetComponent<ModifyGravityGate>().gravityDirection, gameObject);
        }

        if (collision.gameObject.CompareTag("AbnormalGravityZone"))
        {
            m_ObjectGravity.changeGravityScale(collision.gameObject.GetComponent<AbnormalGLight>().getGravityScale());
            m_Rigidbody2D.drag = collision.gameObject.GetComponent<AbnormalGLight>().gravityDrag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AbnormalGravityZone"))
        {
            m_ObjectGravity.resetGravityScale();
            m_Rigidbody2D.drag = 0;
        }
    }
    
    public void RotatePlayerWithGD()
    {
        GravityDirection currentGD = m_ObjectGravity.getCurrentGD();
        switch (currentGD)
        {
            case GravityDirection.Down:
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case GravityDirection.Up:
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case GravityDirection.Left:
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            case GravityDirection.Right:
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }
    public bool isMoving() {
        return moving;
    }
}
