using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : CarMovement
{
    Vector3 cameraOffset;
    public Camera followCamera;
    public MissileManager missileManager;
    public List<Transform> missileSpawn;
    public float lastPositionTime;
    public GameObject plantHud;
    public GameObject emptyPlantHud;
    public List<GameObject> plantFrames;
    public float maxPlantTime;

    float lastPositionTimer;
    float plantTimer;
    Vector3 lastPosition;
    Quaternion lastRotation;
    public Animator plantAnimator;
    int currentMissileSpawner = 0;

    private bool seedsAvailable;
    public override void Start()
    {
        lastPositionTimer = 0f;
        lastPosition = transform.position;
        lastRotation = transform.rotation;
        Vector3 cameraPos = followCamera.transform.position;
        cameraOffset = transform.position - cameraPos;
        base.Start();
        seedsAvailable = true;
        plantTimer = maxPlantTime;
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
        //Debug.Log(seedsAvailable);
        followCamera.transform.position = transform.position - cameraOffset;

        lastPositionTimer += Time.deltaTime;
        if (lastPositionTimer > lastPositionTime && grounded)
        {
            Debug.Log("Actt" + Time.deltaTime);
            lastPosition = transform.position;
            lastRotation = transform.rotation;
            lastPositionTimer = 0f;
        }

        plantTimer -= Time.deltaTime;

        if (plantTimer <= 0f)
        {
            seedsAvailable = false;
            DeactivateAnimFrames();
            emptyPlantHud.SetActive(true);

        }

        if (seedsAvailable)
        {
            Debug.Log(plantTimer);
            if (plantTimer <= 0.3f * maxPlantTime)
            {
                DeactivateAnimFrames();
                plantFrames[0].SetActive(true);

            }
            else
            {
                if (plantTimer <= 0.3f * maxPlantTime)
                {
                    DeactivateAnimFrames();
                    plantFrames[1].SetActive(true);
                }
                else
                {
                    DeactivateAnimFrames();
                    plantFrames[2].SetActive(true);
                }
            }
        }
    }

    private void DeactivateAnimFrames()
    {
        foreach (GameObject frame in plantFrames)
        {
            frame.SetActive(false);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SeedSpot")
        {
            if (!other.GetComponent<SeedSpot>().seeded)
            {
                other.GetComponent<SeedSpot>().Seed();
                seedsAvailable = false;
                plantHud.SetActive(false);
                emptyPlantHud.SetActive(true);
                DeactivateAnimFrames();
            }
        }

        if (other.tag == "RechargeStation")
        {
            seedsAvailable = true;
            plantTimer = maxPlantTime;
            plantHud.SetActive(true);
            emptyPlantHud.SetActive(false);
        }

        if (other.tag == "Water")
        {
            transform.position = lastPosition;
            transform.rotation = lastRotation;
            rb.velocity = Vector3.zero;
        }
    }
}
