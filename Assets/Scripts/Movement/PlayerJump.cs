using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private PlayerProperties player;

    [SerializeField] private Rigidbody body;
    [SerializeField] private bool grounded = false;
    [SerializeField] private float force = 1f;
    [SerializeField] private float impulse = 2000f;
    private Vector3 jump;
    int safeCheck = 0;
    int safeCheckMax = 100;

    private void Start()
    {
        jump = new Vector3(0f, force, 0f);
    }

    void Update()
    {
        if (player.canJump)
        {
            if (grounded && player.CompareKey(player.jumpKey) && safeCheck >= safeCheckMax)
            {
                body.AddForce(jump * impulse, ForceMode.Impulse);
                grounded = false;
                safeCheck = 0;
            }
            else safeCheck++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            grounded = true;
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }
    }
}
