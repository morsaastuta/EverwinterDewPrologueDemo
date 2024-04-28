using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public CameraProperties cam;

    public Vector2 turn;
    private float prevTurnY;
    public float verTop = 30f;
    public float verBot = -10f;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main.GetComponent<CameraProperties>();
        }
    }

    void Update()
    {
        if (cam.canRotate)
        {
            turn.y += Input.GetAxis("Mouse Y");
            turn.x += Input.GetAxis("Mouse X") * 5f;
            if (turn.y > -verBot || turn.y < -verTop)
            {
                turn.y = prevTurnY;
            }
            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
            prevTurnY = turn.y;
        }
    }
}
