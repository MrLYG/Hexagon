using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float startX;
    private float startY;
    private float endX;
    private float endY;
    [SerializeField] Transform endLoc;
    [SerializeField] float endXoffset  = 0;
    [SerializeField] float endYoffset = 0;
    [SerializeField] float speed;
    [SerializeField] float waitTime;

    private float waitCount;
    private bool waiting = false;

    private bool goToEnd = true;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        if (endLoc == null)
        {
            endX = startX + endXoffset;
            endY = startY + endYoffset;
        }
        else
        {
            endX = endLoc.position.x;
            endY = endLoc.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting)
        {
            waitCount += Time.deltaTime;
            if (waitCount > waitTime)
            {
                waiting = false;
            }
        }
        else
        {
            Vector3 targetLoc;
            if (goToEnd)
            {
                targetLoc = new Vector3(endX, endY, 0);
            }
            else
            {
                targetLoc = new Vector3(startX, startY, 0);
            }
            //transform.position = Vector3.Lerp(transform.position, targetLoc, speed * Time.deltaTime);
            transform.position += (targetLoc - transform.position).normalized * speed * Time.deltaTime;
            if ((transform.position - targetLoc).magnitude < 0.1f)
            {
                goToEnd = !goToEnd;
                waiting = true;
                waitCount = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
