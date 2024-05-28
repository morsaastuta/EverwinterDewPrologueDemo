using UnityEngine;

public class Loitering : MonoBehaviour
{
    DataHUB dataHUB;

    // Loiter mode
    [SerializeField] float speed;
    [SerializeField] int turnMax;
    int turn;

    // Animation
    public int velocity;

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
