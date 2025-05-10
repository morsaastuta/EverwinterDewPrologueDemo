using UnityEngine;

public class CircleRunning : MonoBehaviour
{
    WorldProperties mapProperties;

    // Circling vars
    [SerializeField] float speed;
    [SerializeField] float angle;

    // Animation
    public int velocity;
    public int direction;

    // Interaction
    public bool interacting;

    private void Start()
    {
        velocity = 0;
        interacting = false;
        mapProperties = GetComponentInParent<WorldProperties>();
    }

    void Update()
    {
        if (!mapProperties.pausedGame && !interacting)
        {
            Circle();
        }
        else
        {
            velocity = 0;
        }
    }

    void Circle()
    {
        velocity = 1;
        transform.position += speed * transform.forward;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + angle, 0);
    }

    void Stop()
    {
    }
}
