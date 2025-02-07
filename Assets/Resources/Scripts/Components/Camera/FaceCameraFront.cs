using UnityEngine;

public class FaceCameraFront : MonoBehaviour
{
    CameraProperties cam;

    [SerializeField] float verTop;
    [SerializeField] float verBot;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraProperties>();
    }

    void FixedUpdate()
    {
        transform.localRotation = cam.transform.localRotation;
    }
}
