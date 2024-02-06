using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject wheelFL;
    public GameObject wheelFR;
    public GameObject wheelBL;
    public GameObject wheelBR;

    public float acceleration;
    public float maxSpeed;
    public float maxAngle;

    WheelCollider colliderFL;
    WheelCollider colliderFR;
    WheelCollider colliderBL;
    WheelCollider colliderBR;


    void Start()
    {
        colliderFL = wheelFL.GetComponent<WheelCollider>();
        colliderFR = wheelFR.GetComponent<WheelCollider>();
        colliderBL = wheelBL.GetComponent<WheelCollider>();
        colliderBR = wheelBR.GetComponent<WheelCollider>();
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        colliderBL.motorTorque = acceleration * Input.GetAxis("Vertical");
        colliderBR.motorTorque = acceleration * Input.GetAxis("Vertical");

        colliderFL.steerAngle = maxAngle * Input.GetAxis("Horizontal");
        colliderFR.steerAngle = maxAngle * Input.GetAxis("Horizontal");
    }
}