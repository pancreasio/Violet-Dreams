using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : CarMovement
{
    void Update()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movementVector != Vector2.zero && movementVector!= newDirection)
        {
            newDirection = movementVector;
            float angle = Vector2.SignedAngle(transform.up, movementVector);
            newRotationAngle = transform.eulerAngles.z + angle;
            Debug.Log(angle);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
