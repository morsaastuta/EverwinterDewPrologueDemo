using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    [SerializeField] CameraProperties cam;

    [SerializeField] Transform CameraTarget;

    void Update()
    {
        if (cam.canPivot)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, CameraTarget.rotation, 1);
            transform.position = Vector3.Lerp(transform.position, CameraTarget.position, 1);
        }
    }
}
