using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private PlayerProperties player;

    [SerializeField] Rigidbody body;
    [SerializeField] bool grounded = false;
    [SerializeField] float force = 1f;
    [SerializeField] float impulse = 2000f;
    Vector3 jump;
    int safeCheck = 0;
    int safeCheckMax = 100;

    private void Start()
    {
        jump = new(0f, force, 0f);
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
