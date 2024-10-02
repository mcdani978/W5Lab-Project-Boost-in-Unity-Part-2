using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float thrustForce = 10f;
    public float rotationSpeed = 100f;
    public float moveSpeed = 5f;

    public float fuel = 100f; 
    public float fuelConsumptionRate = 10f; 
    public TextMeshProUGUI fuelText; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateFuelText(); 
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        ProcessHorizontalMovement();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) && fuel > 0) 
        {
            rb.AddRelativeForce(Vector3.up * thrustForce);
            ConsumeFuel(); 
        }
    }

    void ProcessRotation()
    {
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
    }

    void ProcessHorizontalMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.left * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    void ConsumeFuel() // Method to reduce fuel
    {
        fuel -= fuelConsumptionRate * Time.deltaTime; 
        fuel = Mathf.Clamp(fuel, 0, 100); 
        UpdateFuelText(); 
    }

    void UpdateFuelText() 
    {
        fuelText.text = "Fuel: " + Mathf.RoundToInt(fuel).ToString(); 
    }
}
