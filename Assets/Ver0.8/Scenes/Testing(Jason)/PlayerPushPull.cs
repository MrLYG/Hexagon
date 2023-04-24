using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushPull : MonoBehaviour
{
    [SerializeField] private LayerMask objectLayer;
    [SerializeField] private GameObject HighlightObject;
    [SerializeField] private GameObject HighlightEnemy;

    private GameObject highlightObj;
    private GameObject highlightEnm;
    private GameObject curObject;
    private float relativeX;

    public bool pushing = false;
    private float playerPreX;

    private void Start()
    {
        highlightObj = Instantiate(HighlightObject);
        highlightObj.SetActive(false);

        highlightEnm = Instantiate(HighlightEnemy);
        highlightEnm.SetActive(false);
    }

    private void Update()
    {
        Vector2 diretion = Vector2.left;
        if(GetComponent<PlayerControl>().facingRight)
            diretion = Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, diretion, 0.55f, objectLayer);
        //Debug.DrawRay(transform.position, diretion, Color.red, 0.5f);

        if (hit.collider != null)
        {
            if (!hit.collider.gameObject.GetComponent<IEnemy>())
                recordObject(hit.collider.gameObject);
            else
                recordEnemy(hit.collider.gameObject);
        }
        else
        {
            if (curObject != null)
            {
                endPushing();
                resetObject();
            }
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J))
        {
            if(curObject != null)
            {
                startPushing();
            }
        }else if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.J))
        {
            if (curObject != null) {
                endPushing();
            }
        }

        playerPreX = gameObject.transform.position.x;
        if (curObject != null && pushing)
        {
            // If pulling
            if(!(relativeX > 0 && playerPreX - gameObject.transform.position.x > 0) &&
                !(relativeX < 0 && playerPreX - gameObject.transform.position.x < 0))
            curObject.transform.localPosition = new Vector3(relativeX, curObject.transform.localPosition.y, 0);
        }
    }

    private void startPushing() {
        curObject.transform.parent = gameObject.transform;
        relativeX = curObject.transform.localPosition.x;

        if(relativeX > 0)
            relativeX = 1.33f;
        else
            relativeX = -1.33f;

        curObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<PlayerControl>().changeWalkSpeed(0.5f);
        pushing = true;
    }
    private void endPushing() {
        curObject.transform.parent = null;
        relativeX = 0;
        curObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        GetComponent<PlayerControl>().changeWalkSpeed(1);
        pushing = false;
    }

    private void recordObject(GameObject obj)
    {
        curObject = obj;
        highlightObj.SetActive(true);
        highlightObj.transform.parent = obj.transform;
        highlightObj.transform.localPosition = Vector3.zero;
        
    }

    private void recordEnemy(GameObject obj)
    {
        curObject = obj;
        highlightEnm.SetActive(true);
        highlightEnm.transform.parent = obj.transform;
        highlightEnm.transform.localPosition = Vector3.zero;

    }

    private void resetObject()
    {
        curObject = null;
        highlightObj.SetActive(false);
        highlightEnm.SetActive(false);
    }
}
