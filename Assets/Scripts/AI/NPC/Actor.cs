using UnityEngine;
using UnityEngine.AI;

public class Actor : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject body;
    [SerializeField] GameObject projection;
    Transform target;
    public int velocity;
    public int direction;
    float velocityDecrementNotice;

    void Awake()
    {
        velocity = 0;
        agent.updateRotation = false;
    }

    void Update()
    {
        if (target is not null)
        {
            float distanceX = Mathf.Abs(transform.position.x - target.position.x);
            float distanceZ = Mathf.Abs(transform.position.z - target.position.z);
            if ((distanceX <= 0.01f && distanceZ <= 0.01f) || (distanceX <= 0.05f && distanceZ <= 0.05f && velocityDecrementNotice > agent.velocity.magnitude))
            {
                velocity = 0;
                agent.isStopped = true;
                target = null;
            }
            else if (velocity != 1) velocity = 1;

            velocityDecrementNotice = agent.velocity.magnitude;
        }
    }

    void LateUpdate()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon) transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }

    public void Load(bool l)
    {
        if (!l)
        {
            body.SetActive(false);
            projection.SetActive(false);
        }
        else
        {
            body.SetActive(true);
            projection.SetActive(true);
        }
    }

    public void SetTarget(Transform t, float speed)
    {
        target = t;
        agent.speed = speed;
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }
}