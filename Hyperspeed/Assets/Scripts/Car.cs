using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject wheelFL;
    public GameObject wheelFR;
    public GameObject wheelBL;
    public GameObject wheelBR;

    public float friction;
    public float acceleration;
    public float reverse;
    public float brake;
    public float maxSpeed;
    public float brakingAngle;
    public float normalAngle;
    public float steeringForce;

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
        rb.AddForce(new Vector3(friction - rb.velocity.x, 0, friction - rb.velocity.z));

        float vIn = Input.GetAxis("Vertical");
        bool spacePressed = Input.GetAxis("Jump") > 0 ? true : false;
        if ((vIn == 0) || (rb.velocity.magnitude > maxSpeed))
        {
            colliderBL.motorTorque = 0.0f;
            colliderBR.motorTorque = 0.0f;
        }
        else
        {
            colliderBL.motorTorque = spacePressed ? 0.0f : Mathf.Sign(vIn) == -1 ? reverse * vIn : acceleration * vIn;
            colliderBR.motorTorque = spacePressed ? 0.0f : Mathf.Sign(vIn) == -1 ? reverse * vIn : acceleration * vIn;
        }
        
        if (spacePressed)
        {
            colliderBL.brakeTorque = brake * 0.7f;
            colliderBR.brakeTorque = brake * 0.7f;
            colliderFL.brakeTorque = brake * 0.3f;
            colliderFR.brakeTorque = brake * 0.3f;
        } else
        {
            colliderBL.brakeTorque = 0.0f;
            colliderBR.brakeTorque = 0.0f;
            colliderFL.brakeTorque = 0.0f;
            colliderFR.brakeTorque = 0.0f;
        }

        colliderFL.steerAngle = Input.GetAxis("Horizontal") * (spacePressed ? brakingAngle : normalAngle);
        colliderFL.steerAngle = Input.GetAxis("Horizontal") * (spacePressed ? brakingAngle : normalAngle);
        rb.AddRelativeTorque(new Vector3(0, Input.GetAxis("Horizontal") * (spacePressed ? brakingAngle : normalAngle) * (rb.velocity.magnitude / maxSpeed) * steeringForce, 0));

        //print(vIn + "\t" + rb.velocity.magnitude + "\t" + ((vIn == 0) || (rb.velocity.magnitude > maxSpeed)) + "\t" + colliderBL.motorTorque + "\t" + colliderBL.brakeTorque);
    }
}