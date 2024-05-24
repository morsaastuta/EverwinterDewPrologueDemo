using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Loitering : MonoBehaviour
{
    DataHUB dataHUB;

    // Loiter mode
    [SerializeField] float speed;
    [SerializeField] int turnMax;
    int turn;

    // Animation
    public int velocity;
    public int direction;

    void Start()
    {
        turn = turnMax;
        dataHUB = GetComponentInParent<DataHUB>();
    }

    void Update()
    {
        if (dataHUB.world.pausedGame || dataHUB.player.isInteracting) velocity = 0;
        else Loiter();
    }

    void Loiter()
    {
        velocity = 1;
        transform.position += speed * transform.forward;
        turn--;
        if (turn <= 0)
        {
            transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
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
