using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullSpring : MonoBehaviour
{
    public string inputButtonName = "Pull";
    public float distance = 50f;
    public float speed = 1f;
    public GameObject ball;
    public float power = 2000f;

    private bool ready = false;
    private bool fire = false;
    private float moveCount = 0f;

    void OnCollisionEnter(Collision _other)
    {
        if (_other.gameObject.CompareTag("Ball"))
        {
            ready = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the input button is pressed, move the piece backward
        if (Input.GetButton(inputButtonName))
        {
            if (moveCount < distance)
            {
                transform.Translate(0, 0, -speed * Time.deltaTime);
                moveCount += speed * Time.deltaTime;
                fire = true;
            }
        }
        else if (moveCount > 0)
        {
            // Shoot the ball
            if (fire && ready)
            {
                ball.transform.TransformDirection(Vector3.forward * 10);
                ball.GetComponent<Rigidbody>().AddForce(0, 0, moveCount * power);
                fire = false;
                ready = false;
            }

            // Move back to the starting position
            transform.Translate(0, 0, 20 * Time.deltaTime);
            moveCount -= 20 * Time.deltaTime;
        }

        // Ensure we don't go past the starting position
        if (moveCount <= 0)
        {
            fire = false;
            moveCount = 0f;
        }
    }
}
