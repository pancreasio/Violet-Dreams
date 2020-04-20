using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;

public class MissileBehaviour : MonoBehaviour
{
    public delegate Vector3 OnObjectiveSelected();
    public static OnObjectiveSelected SelectObjective;

    public delegate void OnMissileCollided();
    public static OnMissileCollided DeactiveMissile;
    
    public Vector3 target;

   // public Transform van;

    Rigidbody rig;
    
    public Vector3 originalRot;
    
    public float speed;
    public float rotationSpeed;
    public float maxTime = 2f;
    public GameObject ExplosionPrefab;
    public float timeBeforePursue;
    
    float lifeTimer;
    float pursueTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        pursueTimer = timeBeforePursue;
    }
    
    private void OnEnable()
    {
        lerpModifier = 0.0f;
        lifeTimer = 0f;
        transform.rotation = Quaternion.Euler(originalRot);
        target = SelectObjective();
        pursueTimer = timeBeforePursue;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (pursueTimer == timeBeforePursue)
            rig.AddForce(transform.forward *  25f, ForceMode.VelocityChange);
        if(pursueTimer > 0f)
            pursueTimer -= Time.deltaTime;
    
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= maxTime)
        {
            if(tag != "Enemy")
                DeactiveMissile();
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
    
    float lerpModifier = 0.0f;
    
    void FixedUpdate()
    {
        if (pursueTimer <= 0f)
        {
            if (lerpModifier < 1)
            {
                transform.rotation = Quaternion.Lerp(Quaternion.Euler(originalRot),
                    Quaternion.LookRotation((target - transform.position).normalized, Vector3.up), lerpModifier);
                lerpModifier += Time.fixedDeltaTime * rotationSpeed;
                if(lerpModifier>=1)
                {
                    rig.velocity = Vector3.zero;
                    rig.freezeRotation = true;
                    rig.AddForce(transform.forward * speed, ForceMode.VelocityChange);
                }
            }
        }
    }       

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != tag)
        {
            GameObject explosion = Instantiate(ExplosionPrefab, collision.GetContact(0).point, Quaternion.identity, null);
            explosion.GetComponent<ExplosionPhysicsForce>().SetCurrentTag(tag);
            Destroy(explosion, 2);

            if(tag == "Enemy")
            {
                GetComponent<EnemyIA>().itsShootingTime = false;
                transform.rotation = GetComponent<TruckBehaviour>().originalRot;
                transform.localPosition = GetComponent<TruckBehaviour>().originalRelativePos;
            }
            else
            {
                DeactiveMissile();
            }

            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
