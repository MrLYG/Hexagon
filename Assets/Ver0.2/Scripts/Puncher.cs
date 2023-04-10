using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour
{
    public float downSpeed = 5.0f; // Downward speed
    public float upSpeed = 1.0f; // Upward speed
    public float downDistance = 5.0f; // Distance to move down
    public float upDuration = 2.0f; // Duration to move up

    private Vector3 initialPosition; // Initial position of the object

    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;

        // Start the coroutine to move the object down initially
        StartCoroutine(MoveObjectDown());
    }

    IEnumerator MoveObjectDown()
    {
        // Calculate the target position to move the object down
        Vector3 targetPosition = initialPosition - new Vector3(0, downDistance, 0);

        // Calculate the time taken for the object to move down
        float timeTaken = (initialPosition.y - targetPosition.y) / downSpeed;

        // Move the object down
        float elapsedTime = 0;
        while (elapsedTime < timeTaken)
        {
            transform.position -= Vector3.up * downSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Start the coroutine to move the object up slowly
        StartCoroutine(MoveObjectUp());
    }

    IEnumerator MoveObjectUp()
    {
        // Calculate the target position to move the object up
        Vector3 targetPosition = initialPosition;

        // Move the object up slowly
        float elapsedTime = 0;
        while (elapsedTime < upDuration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / upDuration * upSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the object position to the target position to avoid precision errors
        transform.position = targetPosition;

        // Restart the coroutine to move the object down initially
        StartCoroutine(MoveObjectDown());
    }
}
