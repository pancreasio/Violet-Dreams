using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float rotationMultiplier;
    public float heightFromGround;
    public LayerMask groundMask;

    protected Vector3 newDirection;
    protected float newRotationAngle;
    protected bool accelerating;

    bool turning;
    bool grounded;
    Ray rayToGround;
    Vector3 direction;
    float rotationAngle;
    Rigidbody rb;
    Vector3 prevVelocity;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Update()
    {
        rayToGround.origin = transform.position;
        rayToGround.direction = -transform.up;
        grounded = Physics.Raycast(rayToGround,heightFromGround,groundMask);
        Debug.Log(grounded);
        Debug.DrawRay(rayToGround.origin, rayToGround.direction * heightFromGround, Color.red, 2f);
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if(grounded && accelerating)
            rb.AddRelativeForce(Vector3.forward * acceleration);

        //else if (rb.velocity.magnitude > speed)
        //    rb.velocity = rb.velocity.normalized * speed;

        if (newDirection != direction)
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

        prevVelocity = rb.velocity;
    }

    IEnumerator Turn()
    {
        turning = true;
        float timer = 0f;
        float fromAngle = rotationAngle;
        float toAngle = newRotationAngle;
        direction = newDirection;
        rotationAngle = newRotationAngle;
        while (turning && grounded)
        {
            float yAngle = Mathf.LerpAngle(fromAngle, toAngle, timer * rotationMultiplier);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yAngle, transform.eulerAngles.z);

            timer += Time.deltaTime;

            if (timer >= 1f)
            {
                direction = newDirection;
                rotationAngle = newRotationAngle;
                turning = false;
            }

            yield return null;
        }
    }
}

