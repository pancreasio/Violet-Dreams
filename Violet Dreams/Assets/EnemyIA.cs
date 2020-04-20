using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    Rigidbody rb;
    bool navmeshStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        agent.enabled = true;
    }
}
