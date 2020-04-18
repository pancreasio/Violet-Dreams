using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed;
    public float acceleration;

    protected Vector3 newDirection;
    protected float newRotationAngle;

    bool turning;
    Vector3 direction;
    float rotationAngle;
    Rigidbody rb;
    Vector3 prevVelocity;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
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
        while (turning)
        {
            float yAngle = Mathf.LerpAngle(fromAngle, toAngle, timer);
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

