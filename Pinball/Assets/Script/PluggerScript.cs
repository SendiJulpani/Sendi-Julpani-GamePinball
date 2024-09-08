using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PluggerScript : MonoBehaviour
{
    float power;
    float minPower = 0f;
    public float maxPower = 100f;
    public Slider powerSlider; // Ensure this is assigned in the Inspector
    bool ballReady;
    List<Rigidbody> ballList;

    // Start is called before the first frame update
    void Start()
    {
        if (powerSlider != null)
        {
            powerSlider.minValue = 0f;
            powerSlider.maxValue = maxPower;
        }
        else
        {
            Debug.LogError("PowerSlider is not assigned in the Inspector!");
        }
        
        ballList = new List<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (powerSlider == null) return; // Early exit if powerSlider is not assigned

        if (ballReady)
        {
            powerSlider.gameObject.SetActive(true);
        }
        else
        {
            powerSlider.gameObject.SetActive(false);
        }

        powerSlider.value = power;
        
        if(ballList.Count > 0)
        {
            ballReady = true;

            if(Input.GetKey(KeyCode.Space))
            {
                if(power <= maxPower)
                {
                    power += 50 * Time.deltaTime;
                }
            }

            if(Input.GetKeyUp(KeyCode.Space))
            {
                foreach(Rigidbody r in ballList)
                {
                    r.AddForce(power * Vector3.forward);
                }
                power = 0f; // Reset power after applying force
            }
        }
        else
        {
            ballReady = false;
            power = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballList.Add(ballRigidbody);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballList.Remove(ballRigidbody);
            }
            power = 0f;
        }
    }
}
