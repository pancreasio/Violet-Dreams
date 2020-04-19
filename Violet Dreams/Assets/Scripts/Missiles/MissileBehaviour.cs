using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    public delegate GameObject OnObjectiveSelected();
    public static OnObjectiveSelected SelectObjective;

    public delegate void OnMissileCollided();
    public static OnMissileCollided DeactiveMissile;

    Transform target;

    public Transform van;

    Rigidbody rig;

    public Vector3 originalRot;

    public float speed;
    public float rotationSpeed;

    float pursueTimer;
    public float timeBeforePursue;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        pursueTimer = timeBeforePursue;
    }

    private void OnEnable()
    {
        lerpModifier = 0.0f;
        transform.position = van.position;
        transform.rotation = Quaternion.Euler(originalRot);
        target = SelectObjective().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(pursueTimer > 0f)
            pursueTimer -= Time.deltaTime;
    }

    float lerpModifier = 0.0f;

    void FixedUpdate()
    {
        if (pursueTimer <= 0f)
        {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(originalRot),
                Quaternion.LookRotation((target.transform.position - transform.position).normalized,Vector3.up), lerpModifier);
            lerpModifier += Time.fixedDeltaTime/rotationSpeed;
        }
        rig.velocity = transform.forward * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        DeactiveMissile();
        pursueTimer = timeBeforePursue;
        rig.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
