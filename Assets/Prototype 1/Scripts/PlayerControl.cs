using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement")]
    [Space]
    [SerializeField] private float walkSpeed;
    [Range(0, .3f)] [SerializeField] private float smoothMovementTime = 0.05f;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;

    [SerializeField] GravityManager m_GravityManager;

    private Vector3 velocity = Vector3.zero;

    private float dirt;
    private bool jump;
    private bool grounded;

    // Inner CD for switching gravity
    private bool canSwitchGD = true;
    [SerializeField] private float GDSwitchCD = 0.5f;
    private float GDSwitchCount = 0f;

    [SerializeField] private LayerMask lightLayer;

    [SerializeField] private GameObject WinningText;

    void Start()
    {
        WinningText.SetActive(false);

        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("GravityManager"))
        {
            m_GravityManager = gameObject.GetComponent<GravityManager>();
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
        if (GetComponent<overallView>().enabled && Input.GetKey(KeyCode.M))
        {
            return;
        }

        // Movement inputs
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
                    m_GravityManager.ReverseGravityDirection();
                    canSwitchGD = false;
                }
            }
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

        Vector3 targetVelocity;
        if (!m_GravityManager.getCameraFollowing())
        {
            currentGD = m_GravityManager.getPreviousGD();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone") || collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<PlayerHP>().getHurt(0.5f, collision.gameObject);
            //Debug.Log("You are dead");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone") || collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<PlayerHP>().getHurt(0.5f, collision.gameObject);
            //Debug.Log("You are dead");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GravityDirection currentGD = m_GravityManager.getCurrentGD();

        if (collision.gameObject.CompareTag("Gate"))
        {
            m_GravityManager.setCameraFollowing(collision.gameObject.GetComponent<ModifyGravityGate>().cameraFollowing);
            m_GravityManager.ForceSwitchGravityDirection(collision.gameObject.GetComponent<ModifyGravityGate>().gravityDirection);
        }

        if (collision.gameObject.CompareTag("AbnormalGravityZone"))
        {
            m_GravityManager.changeGravityScale(collision.gameObject.GetComponent<AbnormalGLight>().getGravityScale());
            m_Rigidbody2D.drag = collision.gameObject.GetComponent<AbnormalGLight>().gravityDrag;
        }

        if (collision.gameObject.CompareTag("Goal")) {
            WinningText.SetActive(true);
            //Time.timeScale = 0;
            Debug.Log("You Win!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AbnormalGravityZone"))
        {
            m_GravityManager.resetGravityScale();
            m_Rigidbody2D.drag = 0;
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

    public void rotateText()
    {
        GravityDirection currentGD = m_GravityManager.getCurrentGD();
        switch (currentGD)
        {
            case GravityDirection.Down:
                transform.Find("Canvas").transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Find("Canvas").transform.localPosition = new Vector3(0, 1.2f, 0);
                break;
            case GravityDirection.Up:
                transform.Find("Canvas").transform.rotation = Quaternion.Euler(0, 0, 180);
                transform.Find("Canvas").transform.localPosition = new Vector3(0, -1.2f, 0);
                break;
            case GravityDirection.Left:
                transform.Find("Canvas").transform.rotation = Quaternion.Euler(0, 0, 270);
                transform.Find("Canvas").transform.localPosition = new Vector3(1.2f, 0, 0);
                break;
            case GravityDirection.Right:
                transform.Find("Canvas").transform.rotation = Quaternion.Euler(0, 0, 90);
                transform.Find("Canvas").transform.localPosition = new Vector3(-1.2f, 0, 0);
                break;
            default:
                transform.Find("Canvas").transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Find("Canvas").transform.localPosition = new Vector3(0, 1.2f, 0);
                break;
        }
    }
}
