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
    protected Rigidbody rb;
    protected bool grounded;
    protected bool onAir;

    bool turning;
    Ray rayToGround;
    Vector3 direction;
    float rotationAngle;

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
        Debug.DrawRay(rayToGround.origin, rayToGround.direction * heightFromGround, Color.red, 2f);
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if(grounded && accelerating)
            rb.AddRelativeForce(Vector3.forward * acceleration);

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
        float timer = 0f;
        float fromAngle = rotationAngle;
        float toAngle = newRotationAngle;
        direction = newDirection;
        rotationAngle = newRotationAngle;
        Debug.Log(toAngle);
        while (turning && grounded)
        {
            float yAngle = Mathf.LerpAngle(fromAngle, toAngle, timer);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yAngle, transform.eulerAngles.z);

            timer += Time.deltaTime * rotationMultiplier * (rb.velocity.magnitude * 0.1f);

            if (timer >= 1f)
            {
                if (toAngle < 0)
                    toAngle += 360;

                if (transform.eulerAngles.y != toAngle)
                    direction = Vector3.zero;
                else
                   direction = newDirection;
                rotationAngle = transform.eulerAngles.y;
                newRotationAngle = rotationAngle;
                turning = false;
            }

            yield return null;
        }
    }
}

