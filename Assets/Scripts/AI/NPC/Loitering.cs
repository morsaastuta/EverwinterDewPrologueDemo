using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Loitering : MonoBehaviour
{
    [SerializeField] MapProperties mapProperties;

    // Loiter mode
    [SerializeField] float speed;
    [SerializeField] int turnMax;
    int turn;

    // Animation
    public int velocity;
    public int direction;

    // Interaction
    [SerializeField] Talk interaction;

    private void Start()
    {
        turn = turnMax;
    }

    void Update()
    {
        if (mapProperties.pausedGame) velocity = 0;
        else if (interaction is not null)
        {
            if (interaction.interacting) velocity = 0;
            else Loiter();
        }
        else Loiter();
    }

    void Loiter()
    {
        velocity = 1;
        transform.position += speed * transform.forward;
        turn--;
        if (turn <= 0)
        {
            transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            if (transform.localRotation.y > -135)
            {
                if (transform.localRotation.y > -45)
                {
                    if (transform.localRotation.y > 45)
                    {
                        if (transform.localRotation.y > 135) direction = 0;
                        else direction = 3;
                    }
                    else direction = 2;
                }
                else direction = 1;
            }
            else direction = 0;
            turn = turnMax;
        }
    }

    void Stop()
    {
        velocity = 0;
        if (turn > 0)
        {
            turn--;
        }
    }
}
