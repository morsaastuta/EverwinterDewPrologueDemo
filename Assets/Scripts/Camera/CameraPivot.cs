using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    [SerializeField] CameraProperties cam;

    [SerializeField] Transform CameraTarget;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, CameraTarget.position, 1);
        if (cam.canPivot) transform.rotation = Quaternion.Lerp(transform.rotation, CameraTarget.rotation, 1);
    }
}
