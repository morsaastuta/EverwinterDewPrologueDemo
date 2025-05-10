using UnityEngine;
using UnityEngine.Rendering;

public class CSCameraMovement : MonoBehaviour
{
    [SerializeField] PlayerBehaviour playerProperties;
    [SerializeField] CameraProperties cameraProperties;

    // Speed
    float walkSpeed = .02f;
    float runSpeed = .04f;
    float normalFactor = 1.5f;
    float speed;

    // Move
    Vector3 movement;
    bool movingSides;
    bool movingFront;

    void Start()
    {
        speed = walkSpeed;

        movement = transform.localScale;
        movingSides = false;
        movingFront = false;
    }

    void Update()
    {
        movement = transform.position;
        if (playerProperties.CompareKeyOnce(playerProperties.runKey, true))
        {
            speed = runSpeed;
        }
        else if (playerProperties.CompareKeyOnce(playerProperties.runKey, false))
        {
            speed = walkSpeed;
        }

        checkDir();

        if (playerProperties.CompareKey(playerProperties.eastKey))
        {
            if (!movingFront) movement += speed * transform.right;
            else movement += speed / normalFactor * transform.right;
        }
        if (playerProperties.CompareKey(playerProperties.westKey))
        {
            if (!movingFront) movement += speed * -transform.right;
            else movement += speed / normalFactor * -transform.right;
        }
        if (playerProperties.CompareKey(playerProperties.northKey))
        {
            if (!movingSides) movement += speed * transform.forward;
            else movement += speed / normalFactor * transform.forward;
        }
        if (playerProperties.CompareKey(playerProperties.southKey))
        {
            if (!movingSides) movement += speed * -transform.forward;
            else movement += speed / normalFactor * -transform.forward;
        }

        if (cameraProperties.canHold)
        {
            if (playerProperties.CompareKeyOnce(playerProperties.holdKey, true)) cameraProperties.SetPivot(true);
        }
        if (playerProperties.CompareKeyOnce(playerProperties.holdKey, false)) cameraProperties.SetPivot(false);

        movement.y = transform.position.y; // During combat, the camera must not move vertically
        transform.position = movement;
    }

    private void checkDir()
    {
        if (playerProperties.CompareKey(playerProperties.northKey)) movingFront = true;
        if (playerProperties.CompareKey(playerProperties.westKey)) movingSides = true;
        if (playerProperties.CompareKey(playerProperties.southKey)) movingFront = true;
        if (playerProperties.CompareKey(playerProperties.eastKey)) movingSides = true;

        if (!playerProperties.CompareKey(playerProperties.westKey) && !playerProperties.CompareKey(playerProperties.eastKey)) movingSides = false;
        if (!playerProperties.CompareKey(playerProperties.northKey) && !playerProperties.CompareKey(playerProperties.southKey)) movingFront = false;
    }
}
