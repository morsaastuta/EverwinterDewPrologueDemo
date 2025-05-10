using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] CameraProperties cam;

    [SerializeField] Transform CameraTarget;

    void FixedUpdate()
    {
        if (cam.canPivot)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, CameraTarget.rotation, 1);
            transform.position = Vector3.Lerp(transform.position, CameraTarget.position, 1);
        }
    }
}
