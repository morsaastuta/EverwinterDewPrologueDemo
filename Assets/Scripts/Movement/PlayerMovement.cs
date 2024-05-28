using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerProperties player;

    // Speed
    float walkSpeed = 0.01f;
    float runSpeed = 0.02f;
    float normalFactor = 1.5f;
    float speed;
    public int velocity;

    // Move
    private Vector3 movement;
    private bool movingSides;
    private bool movingFront;
    public int direction;

    // Visuals
    public Animator animator;

    void Start()
    {
        speed = walkSpeed;

        movement = transform.localScale;
        movingSides = false;
        movingFront = false;
        direction = 2;
        velocity = 0;
    }

    void Update()
    {
        if (player.canMove)
        {
            movement = transform.position;

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
                if (!movingFront) movement += speed * transform.right;
                else movement += speed / normalFactor * transform.right;
            }
            if (player.CompareKey(player.westKey))
            {
                direction = 1;
                if (!movingFront) movement += speed * -transform.right;
                else movement += speed/normalFactor * -transform.right;
            }
            if (player.CompareKey(player.northKey))
            {
                direction = 0;
                if (!movingSides) movement += speed * transform.forward;
                else movement += speed / normalFactor * transform.forward;
            }
            if (player.CompareKey(player.southKey))
            {
                direction = 2;
                if (!movingSides) movement += speed * -transform.forward;
                else movement += speed / normalFactor * -transform.forward;
            }

            transform.position = movement;

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
