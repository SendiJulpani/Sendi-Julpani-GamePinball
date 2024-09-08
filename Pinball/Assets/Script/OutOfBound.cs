using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    public Transform ball; // Assign this in the Inspector
    private Vector3 startingPos;
    public GameManagerScript gameManager;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial position of the ball
        startingPos = ball.position;
    }

    // OnCollisionEnter is called when a collision happens
    void OnCollisionEnter(Collision _other)
    {
        if (_other.gameObject.CompareTag("Ball") && !isDead)
        { 
            isDead = true;
            gameManager.gameOver();
            // Reset the velocity of the ball to zero
            if (_other.rigidbody != null)
            {
                _other.rigidbody.velocity = Vector3.zero;
            }
            
            // Move the ball back to the starting position
            _other.transform.position = startingPos;
        }
    }
}
