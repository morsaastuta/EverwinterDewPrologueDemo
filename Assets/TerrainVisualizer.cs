using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class TerrainVisualizer : MonoBehaviour
{
    [SerializeField] List<Transform> viewpoints;
    [SerializeField] List<int> moveTimes;
    [SerializeField] List<int> restTimes;

    int index;
    int timer = 200;
    bool moving = false;

    void FixedUpdate()
    {
        if (!moving)
        {
            if (timer > 0)
            {
                print(timer);
                timer--;
                if (timer <= 0)
                {
                    if (index >= viewpoints.Count) Application.Quit();
                    else NextViewpoint();
                }
            }
        }
        else if (transform.position == viewpoints[index].position)
        {
            index++;
            moving = false;

        }
    }

    void NextViewpoint()
    {
        transform.DOMove(viewpoints[index].position, moveTimes[index]);
        timer = restTimes[index];
        moving = true;
    }
}
