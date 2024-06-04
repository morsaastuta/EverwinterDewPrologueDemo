using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    // Enemy detection
    Vector3 target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, enemyLayer;

    // Range declaration
    [SerializeField] float sightRange;
    [SerializeField] float jumpRange;

    // Patrol mode
    [SerializeField] float speed;
    bool sighted;
    bool patrolling;
    public int velocity;
    public int direction;
    [SerializeField] List<GameObject> patrolPoints;
    GameObject patrolTarget;

    void Start()
    {
        velocity = 0;
        agent.updateRotation = false;
        agent.speed = speed;
    }

    void Update()
    {
        if (GetComponentInParent<DataHUB>().world.pausedGame || GetComponentInParent<DataHUB>().player.isInteracting)
        {
            velocity = 0;
            agent.speed = 0;
        }
        else
        {
            if (velocity == 0)
            {
                velocity = 1;
                agent.speed = speed;
            }

            // Check if enemy was sighted or is being battled
            sighted = Physics.CheckSphere(transform.position, sightRange, enemyLayer);

            if (sighted) Chase();
            else if (!patrolling) Patrol();

            // Check if agent is stopped (finished patrolling)
            if (patrolTarget is not null)
            {
                float distanceX = Mathf.Abs(transform.position.x - patrolTarget.transform.position.x);
                float distanceZ = Mathf.Abs(transform.position.z - patrolTarget.transform.position.z);
                if (distanceX <= 0.01f && distanceZ <= 0.01f)
                {
                    velocity = 0;
                    patrolling = false;
                }
            }
        }
    }

    void LateUpdate()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon) transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }

    void Patrol()
    {
        patrolling = true;

        int point = Random.Range(0, patrolPoints.Count);
        patrolTarget = patrolPoints[point];
        agent.SetDestination(patrolTarget.transform.position);
    }

    void Chase()
    {
        patrolling = false;
        velocity = 1;
        target = GetComponentInParent<DataHUB>().player.gameObject.transform.position;
        agent.SetDestination(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

