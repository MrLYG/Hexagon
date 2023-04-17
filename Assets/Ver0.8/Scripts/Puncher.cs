using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : IEnemy
{
    public float downDistance = 5.0f; // Distance to move down
    public float upDuration = 2.0f; // Duration to move up

    private Vector3 initialPosition; // Initial position of the object

    public override void Start()
    {
        base.Start();
        // Store the initial position of the object
        initialPosition = transform.localPosition;

        // Start the coroutine to move the object down initially
        StartCoroutine(MoveObjectDown());
    }

    IEnumerator MoveObjectDown()
    {
        // Calculate the target position to move the object down

        Vector3 targetPosition = initialPosition - new Vector3(0, downDistance, 0);

        // Move the object down
        while (transform.localPosition.y > targetPosition.y)
        {
            transform.localPosition -= Vector3.up * curSpeed * Time.deltaTime;
            yield return null;
        }
        transform.localPosition = targetPosition;

        // Start the coroutine to move the object up slowly
        StartCoroutine(MoveObjectUp());
    }

    IEnumerator MoveObjectUp()
    {
        // Calculate the target position to move the object up
        Vector3 targetPosition = initialPosition;

        while (transform.localPosition.y < targetPosition.y - 0.1f)
        {
            transform.localPosition += Vector3.up * curSpeed * Time.deltaTime * 0.5f;
            yield return null;
        }

        // Set the object position to the target position to avoid precision errors
        transform.localPosition = targetPosition;

        yield return new WaitForSeconds(0.5f);

        // Restart the coroutine to move the object down initially
        StartCoroutine(MoveObjectDown());
    }
}
