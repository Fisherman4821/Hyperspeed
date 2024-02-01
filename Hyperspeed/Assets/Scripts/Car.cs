using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public bool driveable = false;
    public Transform frontLeftWheelWrapper;
    public Transform frontRightWheelWrapper;
    public Transform rearLeftWheelWrapper;
    public Transform rearRightWheelWrapper;
    public Transform frontLeftWheelMesh;
    public Transform frontRightWheelMesh;
    public Transform rearLeftWheelMesh;
    public Transform rearRightWheelMesh;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    public float maxTorque = 20f;
    public float brakeTorque = 100f;
    public float maxWheelTurnAngle = 30f;
    public Vector3 centerOfMass = new Vector3(0f, 0f, 0f);
    private float torquePower = 0f;
    private float steerAngle = 0f;
    private float wheelMeshWrapperFLx;
    private float wheelMeshWrapperFLy;
    private float wheelMeshWrapperFLz;
    private float wheelMeshWrapperFRx;
    private float wheelMeshWrapperFRy;
    private float wheelMeshWrapperFRz;
    private float wheelMeshWrapperRLx;
    private float wheelMeshWrapperRLy;
    private float wheelMeshWrapperRLz;
    private float wheelMeshWrapperRRx;
    private float wheelMeshWrapperRRy;
    private float wheelMeshWrapperRRz;


    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
    }

    void Update()
    {
        if (!driveable)
        {
            return;
        }

        frontLeftWheelWrapper.localEulerAngles = new Vector3(0, steerAngle, 0);
        frontRightWheelWrapper.localEulerAngles = new Vector3(0, steerAngle, 0);

        frontLeftWheelMesh.Rotate(0, wheelFL.rpm / 60 * 360 * Time.deltaTime, 0);
        frontRightWheelMesh.Rotate(0, wheelFR.rpm / 60 * 360 * Time.deltaTime, 0);
        rearLeftWheelMesh.Rotate(0, wheelRL.rpm / 60 * 360 * Time.deltaTime, 0);
        rearRightWheelMesh.Rotate(0, wheelRR.rpm / 60 * 360 * Time.deltaTime, 0);

        GetComponent<AudioSource>().pitch = (torquePower / maxTorque) + 0.5f;
    }

    void FixedUpdate()
    {
        if (!driveable)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            torquePower = 0f;
            wheelRL.brakeTorque = brakeTorque;
            wheelRR.brakeTorque = brakeTorque;
        }
        else
        {
            torquePower = maxTorque * Mathf.Clamp(Input.GetAxis("Vertical"), -1, 1);
            wheelRL.brakeTorque = 0f;
            wheelRR.brakeTorque = 0f;
        }

        wheelRR.motorTorque = torquePower;
        wheelRL.motorTorque = torquePower;

        Debug.Log("torquePower: " + torquePower);
        Debug.Log("brakeTorque RL: " + wheelRL.brakeTorque);
        Debug.Log("brakeTorque RR: " + wheelRR.brakeTorque);
        Debug.Log("steerAngle: " + steerAngle);

        steerAngle = maxWheelTurnAngle * Input.GetAxis("Horizontal");
        wheelFL.steerAngle = steerAngle;
        wheelFR.steerAngle = steerAngle;
    }
}