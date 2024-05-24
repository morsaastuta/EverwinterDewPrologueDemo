using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    // Enemy detection
    public GameObject enemy;
    private Vector3 target;
    public NavMeshAgent agent;
    public LayerMask groundLayer, enemyLayer;

    // Range declaration
    public float patrolRange;
    public float sightRange;
    public float jumpRange;
    public float combatRange;

    // Jump utils
    public Rigidbody body;
    private bool grounded = false;
    private Vector3 jump = new Vector3(0f, .7f, 0f);
    public float impulse = 100f;

    // Patrol mode
    public float speed;
    private bool sighted;
    public int turnMax;
    private int turn;

    // Combat mode
    private bool combatMode;

    private void Start()
    {
        turn = turnMax;
    }

    void Update()
    {

        target = enemy.transform.position;

        // Jump
        if (grounded & target.y > transform.position.y && Physics.CheckSphere(transform.position, jumpRange, enemyLayer))
        {
            agent.updatePosition = false;
            agent.updateRotation = false;
            body.AddForce((jump + transform.forward / 7) * impulse, ForceMode.Impulse);
        }

        // Check if enemy was sighted or is being battled
        sighted = Physics.CheckSphere(transform.position, sightRange, enemyLayer);
        combatMode = Physics.CheckSphere(transform.position, combatRange, enemyLayer);
        if (!sighted)
        {
            Patrol();
        }
        else
        {
            if (!combatMode)
            {
                Chase();
            }
            else
            {
                Combat();
            }
        }
    }

    void Patrol()
    {
        transform.position += speed * transform.forward;
        turn--;
        if (turn == 0)
        {
            transform.localRotation = Quaternion.Euler(0, Random.Range(-patrolRange, patrolRange), 0);
            turn = turnMax;
        }
    }

    void Chase()
    {
        agent.SetDestination(target);
    }

    void Combat()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, combatRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, jumpRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            grounded = true;
        }
    }
}
