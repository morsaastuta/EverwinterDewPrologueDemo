using UnityEngine;

public class CameraProperties : MonoBehaviour
{
    [SerializeField] private DataPersistenceManager dpm;

    public bool canZoom;
    public bool canPivot;
    public bool canRotate;

    // Camera info
    public Vector3 cameraPos;
    public Quaternion cameraAngle;

    void Start()
    {
        dpm.LoadGame(0);
    }

    public void SaveStatus()
    {
        cameraPos = transform.position;
        cameraAngle = transform.rotation;
    }

    public void LoadStatus()
    {
        transform.SetPositionAndRotation(cameraPos, cameraAngle);
    }

    public void SetActive(bool state)
    {
        canZoom = state;
        canPivot = state;
        canRotate = state;
    }
}
