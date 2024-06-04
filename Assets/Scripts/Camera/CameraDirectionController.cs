using UnityEngine;

public class CameraDirectionController : MonoBehaviour
{
    CameraProperties cam;

    Vector2 turn;
    float prevTurnY;
    [SerializeField] float verTop;
    [SerializeField] float verBot;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraProperties>();
    }

    void FixedUpdate()
    {
        if (cam.canPivot)
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
