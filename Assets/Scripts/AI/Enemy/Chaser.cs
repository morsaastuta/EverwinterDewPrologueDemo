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
    [SerializeField] float patrolRange;
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
    }

    void Update()
    {
        if (GetComponentInParent<DataHUB>().world.pausedGame || GetComponentInParent<DataHUB>().player.isInteracting) velocity = 0;

        // Check if enemy was sighted or is being battled
        sighted = Physics.CheckSphere(transform.position, sightRange, enemyLayer);

        if (sighted) Chase();
        else if (!patrolling) Patrol();

        // Check if agent is stopped (finished patrolling)
        if (patrolTarget is not null)
        {
            if (Mathf.Abs(transform.position.x - patrolTarget.transform.position.x) <= 0.01f)
            {
                velocity = 0;
                patrolling = false;
            }
        }
    }

    void Patrol()
    {
        patrolling = true;
        velocity = 1;

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
}
