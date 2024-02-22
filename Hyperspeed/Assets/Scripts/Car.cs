using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Car : MonoBehaviour
{
    public GameObject wheelFL;
    public GameObject wheelFR;
    public GameObject wheelBL;
    public GameObject wheelBR;

    public float acceleration;
    public float reverse;
    public float brake;
    public float maxSpeed;
    public float steeringBrakingMult;
    public float maxAngle;
    public float friction;

    Rigidbody rb;

    WheelCollider colliderFL;
    WheelCollider colliderFR;
    WheelCollider colliderBL;
    WheelCollider colliderBR;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        float vIn = Input.GetAxis("Vertical");
        bool spacePressed = Input.GetAxis("Jump") > 0 ? true : false;
        if (vIn == 0 || rb.velocity.magnitude > maxSpeed)
        {
            colliderBL.motorTorque = 0;
            colliderBR.motorTorque = 0;
        }
        else
        {
            colliderBL.motorTorque = spacePressed ? 0 : vIn < 0 ? reverse * vIn : acceleration * vIn;
            colliderBR.motorTorque = spacePressed ? 0 : vIn < 0 ? reverse * vIn : acceleration * vIn;
        }

        if (spacePressed)
        {
            colliderBL.brakeTorque = brake * 0.7f;
            colliderBR.brakeTorque = brake * 0.7f;
            colliderFL.brakeTorque = brake * 0.3f;
            colliderFR.brakeTorque = brake * 0.3f;
        } else
        {
            colliderBL.brakeTorque = 0;
            colliderBR.brakeTorque = 0;
            colliderFL.brakeTorque = 0;
            colliderFR.brakeTorque = 0;
        }

        colliderFL.steerAngle = maxAngle * Input.GetAxis("Horizontal") * (spacePressed ? steeringBrakingMult : 1);
        colliderFR.steerAngle = maxAngle * Input.GetAxis("Horizontal") * (spacePressed ? steeringBrakingMult : 1);

        print(rb.velocity.magnitude);
    }
}