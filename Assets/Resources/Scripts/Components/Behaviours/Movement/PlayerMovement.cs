using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerBehaviour player;

    // Speed
    float walkSpeed = 0.01f;
    float runSpeed = 0.02f;
    float normalFactor = 2f;
    float speed;
    public int velocity;

    // Move
    Vector3 movement;
    bool movingSides;
    bool movingFront;
    public int direction;

    // Visuals
    [SerializeField] Animator animator;
    [SerializeField] Transform body;
    [SerializeField] Transform projection;

    void Start()
    {
        speed = walkSpeed;

        movement = body.localScale;
        movingSides = false;
        movingFront = false;
        direction = 2;
        velocity = 0;

        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        if (player.canMove)
        {
            movement = body.position;

            if (player.canRun)
            {
                if (player.CompareKeyOnce(player.runKey, true))
                {
                    speed = runSpeed;
                }
                else if (player.CompareKeyOnce(player.runKey, false))
                {
                    speed = walkSpeed;
                }
            }

            checkDir();

            if (player.CompareKey(player.eastKey))
            {
                direction = 3;
                if (!movingFront) movement += speed * projection.right;
                else movement += speed / normalFactor * projection.right;
            }
            if (player.CompareKey(player.westKey))
            {
                direction = 1;
                if (!movingFront) movement += speed * -projection.right;
                else movement += speed/normalFactor * -projection.right;
            }
            if (player.CompareKey(player.northKey))
            {
                direction = 0;
                if (!movingSides) movement += speed * projection.forward;
                else movement += speed / normalFactor * projection.forward;
            }
            if (player.CompareKey(player.southKey))
            {
                direction = 2;
                if (!movingSides) movement += speed * -projection.forward;
                else movement += speed / normalFactor * -projection.forward;
            }

            body.position = movement;

            if (animator.GetInteger("velocity") != velocity) animator.SetInteger("velocity", velocity);
            if (animator.GetInteger("direction") != direction) animator.SetInteger("direction", direction);
        }
    }

    private void checkDir()
    {
        if (player.CompareKey(player.northKey)) movingFront = true;
        if (player.CompareKey(player.westKey)) movingSides = true;
        if (player.CompareKey(player.southKey)) movingFront = true;
        if (player.CompareKey(player.eastKey)) movingSides = true;

        if (!player.CompareKey(player.westKey) && !player.CompareKey(player.eastKey)) movingSides = false;
        if (!player.CompareKey(player.northKey) && !player.CompareKey(player.southKey)) movingFront = false;

        if (!movingSides && !movingFront) velocity = 0;
        else
        {
            if (player.CompareKey(player.runKey)) velocity = 1;
            else velocity = 1; // actually 2 but the animator does not contain run animations
        }
    }
}
