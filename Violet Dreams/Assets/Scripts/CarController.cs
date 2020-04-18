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

    void Update()
    {
        Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (movementVector != Vector3.zero && movementVector!= newDirection)
        {
            newDirection = movementVector;
            float angle = Vector3.SignedAngle(transform.forward, movementVector, Vector3.up);
            newRotationAngle = transform.eulerAngles.y + angle;
            Debug.Log(angle);
        }

        Camera.main.transform.position = transform.position - cameraOffset;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
