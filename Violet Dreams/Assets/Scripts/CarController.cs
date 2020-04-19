using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : CarMovement
{
    Vector3 cameraOffset;

    public override void Start()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        cameraOffset = transform.position - cameraPos;
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (movementVector != Vector3.zero)
        {
            accelerating = true;
            if (movementVector != newDirection)
            {
                float angle = Vector3.SignedAngle(transform.forward, movementVector, Vector3.up);
                newRotationAngle = transform.eulerAngles.y + angle;
            }
        }
        else
        {
            accelerating = false;
        }

        Camera.main.transform.position = transform.position - cameraOffset;
    }

    public override void FixedUpdate()
    {
        if (!grounded && !onAir)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                rb.AddRelativeTorque(transform.forward * 75f);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                rb.AddRelativeTorque(-transform.forward * 75f);
            }
        }

        base.FixedUpdate();
    }

    private void OnCollisionExit(Collision collision)
    {
        onAir = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        onAir = false;
    }
}
