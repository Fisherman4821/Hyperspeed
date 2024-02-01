using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public struct curveMul {    
        public AnimationCurve curve;
        public float multiplier;
    }
    public float frictionMultiplier;
    public curveMul acceleration;
    public float takeOff;
    public curveMul steering;
    public float maxSpeed;
    public float maxAngle;

    Rigidbody rb;
    Transform tf;
    bool isGrounded;
    float y;
    float x;
    float groundFriction;
    float[] speedDeltas = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};


    void OnCollisionStay(Collision collision)
    {
        Ground groundObject;
        collision.gameObject.TryGetComponent<Ground>(out groundObject);
        if (groundObject != null)
        {
            groundFriction = groundObject.friction;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        isGrounded = true;
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            _ = y + 2;
            isGrounded = false;

        }
    }

    void hover()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == false)
        {
            _ = y + 2;
 
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ground")
        {
            isGrounded = true;
            y = 0;
        }
    }
    
    void getSpeedDelta()
    {
        speedDeltas[9] = speedDeltas[8];
        speedDeltas[8] = speedDeltas[7];
        speedDeltas[7] = speedDeltas[6];
        speedDeltas[6] = speedDeltas[5];
        speedDeltas[5] = speedDeltas[4];
        speedDeltas[4] = speedDeltas[3];
        speedDeltas[3] = speedDeltas[2];
        speedDeltas[2] = speedDeltas[1];
        speedDeltas[1] = speedDeltas[0];
        speedDeltas[0] = (rb.velocity.x + rb.velocity.z) / maxSpeed;
    }

    float CalcSteering(float speed, float angle, float friction)
    {
        return steering.curve.Evaluate(1 - angle) * speed * friction;
    }

    float CalcAcceleration(float input, float speed, float friction)
    {
        float speedDeltasAvr = 0;
        for (int i = 0; i < 10; i++)
        {
            speedDeltasAvr += speedDeltas[i];
        }

        speedDeltasAvr = speedDeltasAvr / 10;

        if (speedDeltasAvr < 0.1 && speedDeltasAvr > -0.1)
        {
            return acceleration.curve.Evaluate(speed) * (acceleration.multiplier + takeOff) * input * friction;
        }
        return acceleration.curve.Evaluate(speed) * acceleration.multiplier * input * friction;
    }

    void FixedUpdate()
    {
        //Apply Acceleration
        rb.AddRelativeForce(new Vector3(0, 0, CalcAcceleration(Input.GetAxis("Vertical"), (rb.velocity.x + rb.velocity.z) / maxSpeed, groundFriction)), ForceMode.Acceleration);
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed));
        if (rb.velocity.x > 0 && rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x - (groundFriction * frictionMultiplier), rb.velocity.y, rb.velocity.z - (groundFriction * frictionMultiplier));
        } else if (rb.velocity.x < 0 && rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x + (groundFriction * frictionMultiplier), rb.velocity.y, rb.velocity.z - (groundFriction * frictionMultiplier));
        } else if (rb.velocity.x > 0 && rb.velocity.z < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x - (groundFriction * frictionMultiplier), rb.velocity.y, rb.velocity.z + (groundFriction * frictionMultiplier));
        } else if (rb.velocity.x > 0 && rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x + (groundFriction * frictionMultiplier), rb.velocity.y, rb.velocity.z + (groundFriction * frictionMultiplier));
        }

        print(rb.velocity);

        //rb.AddTorque(new Vector3(0, CalcSteering((rb.velocity.x + rb.velocity.z) / maxSpeed, Input.GetAxis("Horizontal"), groundFriction), 0), ForceMode.Force);
        tf.Rotate(new Vector3(0, CalcSteering((rb.velocity.x + rb.velocity.z) / maxSpeed, Input.GetAxis("Horizontal"), groundFriction), 0));

        /*
        if (Input.GetAxis("Jump") > 0 && isGrounded)
        {
            print("worked");
            isGrounded = false;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + 10.0f, rb.velocity.z);
        }
        */
    }
}
