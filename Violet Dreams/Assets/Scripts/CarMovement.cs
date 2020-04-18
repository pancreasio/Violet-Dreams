using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float deacceleration;
    
    protected Vector2 newDirection;
    protected float newRotationAngle;

    bool turning;
    Vector2 direction;
    float rotationAngle;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
            rb.AddRelativeForce(Vector2.up * acceleration);
            if (rb.velocity.magnitude > speed)
                rb.velocity = rb.velocity.normalized * speed;
        if (newDirection != direction)
        {
            if (turning)
            {
                StopCoroutine("Turn");
                direction = newDirection;
                turning = false;
            }
            rotationAngle = transform.eulerAngles.z;

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
        while (turning)
        {
            float zAngle = Mathf.LerpAngle(fromAngle, toAngle, timer);
            transform.eulerAngles = new Vector3(0f, 0f, zAngle);

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
