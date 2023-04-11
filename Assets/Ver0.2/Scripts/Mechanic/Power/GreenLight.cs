using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLight : ILight
{
    [SerializeField] private float drag;
    [SerializeField] private float slowdownRatio;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>()){
            collision.gameObject.GetComponent<Rigidbody2D>().drag = drag;
            AffectedObjects.Add(collision.gameObject);
        }
        if (collision.gameObject.GetComponent<IEnemy>()){
            collision.gameObject.GetComponent<IEnemy>().chageSpeed(slowdownRatio);
            AffectedObjects.Add(collision.gameObject);
        }
        if (collision.gameObject.GetComponent<IObject>())
        {
            collision.gameObject.GetComponent<IObject>().chageSpeed(slowdownRatio);
            AffectedObjects.Add(collision.gameObject);
        }
        if (collision.gameObject.GetComponent<EnemyTrack>())
        {
            collision.gameObject.GetComponent<EnemyTrack>().numOfGreenlight += 1;
        }


    }
    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>())
        {
            collision.gameObject.GetComponent<Rigidbody2D>().drag = 0;
            AffectedObjects.Remove(collision.gameObject);
        }
        if (collision.gameObject.GetComponent<IEnemy>())
        {
            collision.gameObject.GetComponent<IEnemy>().resetSpeed();
            AffectedObjects.Remove(collision.gameObject);
        }
        if (collision.gameObject.GetComponent<IObject>())
        {
            collision.gameObject.GetComponent<IObject>().resetSpeed();
            AffectedObjects.Remove(collision.gameObject);
        }
    }

    public override void RemoveLight()
    {
        foreach (GameObject obj in AffectedObjects) {
            if (obj.GetComponent<PlayerControl>())
            {
                obj.GetComponent<Rigidbody2D>().drag = 0;
            }
            if (obj.GetComponent<IEnemy>())
            {
                obj.GetComponent<IEnemy>().resetSpeed();
            }
            if (obj.GetComponent<IObject>())
            {
                obj.GetComponent<IObject>().resetSpeed();
            }
        }
        base.RemoveLight();
    }
}
