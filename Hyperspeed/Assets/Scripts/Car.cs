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
        float vIn = Input.GetAxis("Vertical");
        colliderBL.motorTorque = acceleration * vIn;
        colliderBR.motorTorque = acceleration * vIn;

        colliderFL.steerAngle = maxAngle * Input.GetAxis("Horizontal");
        colliderFR.steerAngle = maxAngle * Input.GetAxis("Horizontal");
    }
}