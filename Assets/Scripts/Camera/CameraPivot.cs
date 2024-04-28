using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public CameraProperties cam;

    public Transform CameraTarget;
    public float pLerp;
    public float rLerp;

    void Update()
    {
        if (cam.canPivot)
        {
            transform.position = Vector3.Lerp(transform.position, CameraTarget.position, pLerp);
            transform.rotation = Quaternion.Lerp(transform.rotation, CameraTarget.rotation, rLerp);
        }
    }
}
