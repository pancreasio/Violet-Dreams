using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float rotationMultiplier;
    public float minSpeedToRotate;
    public float minRotationSpeed;
    public float maxRotationSpeed;
    public float heightFromGround;
    public float frontRayDistance;
    public LayerMask groundMask;

    protected Vector3 newDirection;
    protected float newRotationAngle;
    protected bool accelerating;
    protected Rigidbody rb;
    protected bool grounded;
    protected bool onAir;

    bool turning;
    Ray rayToGround;
    Ray rayToForward;
    Vector3 direction;
    float rotationAngle;
    int accelerationMultiplier = 1;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Update()
    {
        rayToGround.origin = transform.position;
        rayToGround.direction = -transform.up;
        rayToForward.origin = transform.position;
        rayToForward.direction = transform.forward;
        grounded = Physics.Raycast(rayToGround,heightFromGround,groundMask);
        if (Physics.Raycast(rayToForward, frontRayDistance, groundMask))
            accelerationMultiplier = -30;
        else
            accelerationMultiplier = 1;
        Debug.DrawRay(rayToGround.origin, rayToGround.direction * heightFromGround, Color.red, 2f);
        Debug.DrawRay(rayToForward.origin, rayToForward.direction * frontRayDistance, Color.yellow);

        Vector3 newRotation = transform.localRotation.eulerAngles;
        newRotation.z = 0f;
        transform.localRotation = Quaternion.Euler(newRotation);
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if(grounded && accelerating)
            rb.AddRelativeForce(Vector3.forward * acceleration * accelerationMultiplier);

        //else if (rb.velocity.magnitude > speed)
        //    rb.velocity = rb.velocity.normalized * speed;

        if (rotationAngle != newRotationAngle)
        {
            if (turning)
            {
                StopCoroutine("Turn");
                direction = newDirection;
                turning = false;
            }
            rotationAngle = transform.eulerAngles.y;

            StartCoroutine("Turn");
        }
    }

    IEnumerator Turn()
    {
        turning = true;
        float fromAngle = rotationAngle;
        float toAngle = newRotationAngle;
        direction = newDirection;
        rotationAngle = newRotationAngle;
        while (turning && grounded)
        {
            float velocityMagnitude = rb.velocity.magnitude;

            Quaternion destRot = Quaternion.Euler(transform.rotation.x, toAngle, transform.rotation.z);
            float velocityCoefficient = velocityMagnitude * rotationMultiplier;
            
            if(velocityMagnitude > minSpeedToRotate)
            {
                velocityCoefficient = Mathf.Clamp(velocityMagnitude * rotationMultiplier, minRotationSpeed, maxRotationSpeed);
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, destRot, velocityCoefficient * Time.deltaTime);

            if(fromAngle != transform.eulerAngles.y && velocityMagnitude == 0)
            {
                rotationAngle = transform.eulerAngles.y;
                newRotationAngle = rotationAngle;
                turning = false;
            }

            yield return null;
        }
    }
}

