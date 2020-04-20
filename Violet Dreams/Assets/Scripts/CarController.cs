using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : CarMovement
{
    Vector3 cameraOffset;
    public Camera followCamera;
    public MissileManager missileManager;
    public List<Transform> missileSpawn;

    int currentMissileSpawner = 0;

    public override void Start()
    {
        Vector3 cameraPos = followCamera.transform.position;
        cameraOffset = transform.position - cameraPos;
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (Input.GetMouseButtonDown(0) && !missileManager.isMissileActive)
        {
            missileManager.ActivateMissile(missileSpawn[currentMissileSpawner].transform.position);
            currentMissileSpawner++;
            currentMissileSpawner = (int)Mathf.PingPong(currentMissileSpawner, missileSpawn.Count - 1);
            Debug.Log(currentMissileSpawner);
        }

        if (movementVector != Vector3.zero)
        {
            accelerating = true;
            if (movementVector != newDirection)
            {
                float angle = Vector3.SignedAngle(transform.forward, movementVector, Vector3.up);
                newRotationAngle = transform.eulerAngles.y + angle;
                newDirection = movementVector;
            }
        }
        else
        {
            accelerating = false;
        }

        followCamera.transform.position = transform.position - cameraOffset;
    }

    public override void FixedUpdate()
    {
        if (!grounded && !onAir)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                rb.AddRelativeTorque(Vector3.forward * 50f);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                rb.AddRelativeTorque(-Vector3.forward * 50f);
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
