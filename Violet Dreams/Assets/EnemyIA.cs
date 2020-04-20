using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    Rigidbody rb;
    bool nearTargetStop = false;

    bool nearTarget;

    protected bool itsShootingTime = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        nearTarget = Vector3.Distance(target.position, transform.position) < agent.stoppingDistance;

        if (agent.enabled)
        {
            agent.SetDestination(target.position);

            bool blocked = false;
            NavMeshHit nmHit;
            if (agent.FindClosestEdge(out nmHit))
            {
                if (nmHit.distance < agent.radius)
                    blocked = true;
            }

            if (blocked)
            {
                rb.velocity = agent.velocity;
                agent.enabled = false;
            }



            if (nearTarget && agent.enabled)
            {
                agent.enabled = false;
                agent.velocity = Vector3.zero;
                transform.rotation = Quaternion.identity;
                rb.velocity = Vector3.zero;
                nearTargetStop = true;
                itsShootingTime = true;
            }
        }
        else
        {
            bool grounded = Physics.Raycast(transform.position, Vector3.down, 5);
            if (grounded && rb.velocity.magnitude < 1 && !nearTargetStop)
            {
                agent.enabled = true;
            }

            if (!nearTarget && nearTargetStop)
            {
                agent.enabled = true;
                nearTargetStop = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!nearTargetStop && agent)
            agent.enabled = true;
    }
}
