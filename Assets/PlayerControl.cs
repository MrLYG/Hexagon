using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement")]
    [Space]
    [SerializeField] private float walkSpeed;
    [Range(0, .3f)] [SerializeField] private float smoothMovementTime = 0.05f;
    [SerializeField] private float gravityScale = 1f;
    //[SerializeField] private float initialJumpVelocity = 10f;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;

    [Header("Camera")]
    [Space]
    [SerializeField] private GameObject CAM;
    [SerializeField] private bool cameraFollowing = false;
    [Range(0, 10f)] [SerializeField] private float cameraRotationSpeed = 10f;

    private Vector3 velocity = Vector3.zero;

    //[SerializeField] private float speed;
    // Start is called before the first frame update

    enum GravityDirection { Down, Left, Up, Right};
    private GravityDirection currentGD = GravityDirection.Down;
    private GravityDirection prevGD = GravityDirection.Down;

    private float dirt;
    private bool jump;
    private bool grounded;

    //private bool rotating;
    private Quaternion targetRotation;

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

        if (Input.GetKey(KeyCode.LeftShift)) {
            //Time.timeScale = 0.5f;
            if (Input.GetKeyDown(KeyCode.W)) {
                SwitchGravityDirection(GravityDirection.Up);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                SwitchGravityDirection(GravityDirection.Left);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                SwitchGravityDirection(GravityDirection.Down);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                SwitchGravityDirection(GravityDirection.Right);
            }
        }
        else
        {
            //Time.timeScale = 1f;
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
            }
        }

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

        // Rotate Camera
        if (cameraFollowing) {
            //CAM.transform.rotation = targetRotation;
            CAM.transform.rotation = Quaternion.Slerp(CAM.transform.rotation, targetRotation, cameraRotationSpeed * Time.deltaTime);
        }

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
        if (collision.gameObject.CompareTag("Gate")) {
            if (cameraFollowing)
            {
                switch (currentGD)
                {
                    case GravityDirection.Down:
                        SwitchGravityDirection(GravityDirection.Right);
                        break;
                    case GravityDirection.Up:
                        SwitchGravityDirection(GravityDirection.Left);
                        break;
                    case GravityDirection.Left:
                        SwitchGravityDirection(GravityDirection.Up);
                        break;
                    case GravityDirection.Right:
                        SwitchGravityDirection(GravityDirection.Down);
                        break;
                }
            }
            else {
                SwitchGravityDirection(GravityDirection.Right);
            }
        }
    }
    private void SwitchGravityDirection(GravityDirection newDirection) {
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
        //currentJumpVelocity = -gravityScale / 2;
        switch (currentGD)
        {
            case GravityDirection.Down:
                groundCheck.position = transform.position + new Vector3(0, -0.4f, 0);
                Physics2D.gravity = new Vector2(0, -gravityScale * 9.8f);
                break;
            case GravityDirection.Up:
                groundCheck.position = transform.position + new Vector3(0, 0.4f, 0);
                Physics2D.gravity = new Vector2(0, gravityScale * 9.8f);
                break;
            case GravityDirection.Left:
                groundCheck.position = transform.position + new Vector3(-0.4f, 0, 0);
                Physics2D.gravity = new Vector2(-gravityScale * 9.8f, 0);
                break;
            case GravityDirection.Right:
                groundCheck.position = transform.position + new Vector3(0.4f, 0, 0);
                Physics2D.gravity = new Vector2(gravityScale * 9.8f, 0);
                break;
            default:
                groundCheck.position = transform.position + new Vector3(0, -0.4f, 0);
                Physics2D.gravity = new Vector2(0, -gravityScale * 9.8f);
                break;
        }

        if (cameraFollowing) {
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
