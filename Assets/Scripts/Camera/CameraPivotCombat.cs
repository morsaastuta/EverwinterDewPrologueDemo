using UnityEngine;

public class CameraPivotCombat : MonoBehaviour
{
    [SerializeField] CameraProperties cam;
    [SerializeField] Transform CameraTarget;

    void Start()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, CameraTarget.rotation, 1);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, CameraTarget.position, 1);
        if (cam.canPivot)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, CameraTarget.rotation, 1);
        }
    }
}
