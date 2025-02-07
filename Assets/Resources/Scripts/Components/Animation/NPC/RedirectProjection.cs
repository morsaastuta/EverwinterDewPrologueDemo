using UnityEngine;

public class RedirectProjection : MonoBehaviour
{
    [SerializeField] Transform projectionDirector;
    int direction; // Back 0 - Right 1 - Front 2 - Left 3

    void FixedUpdate()
    {
        float proDir = projectionDirector.rotation.eulerAngles.y + 45;
        float camDir = Camera.main.transform.rotation.eulerAngles.y + 45;

        if (proDir > 360) proDir -= 360;
        if (camDir > 360) camDir -= 360;

        if (Mathf.Abs(camDir - proDir) <= 45) direction = 0;
        else if ((camDir - proDir <= 135 && camDir - proDir > 45) || (proDir - camDir <= 315 && proDir - camDir > 225)) direction = 1;
        else if ((proDir - camDir <= 135 && proDir - camDir > 45) || (camDir - proDir <= 315 && camDir - proDir > 225)) direction = 3;
        else direction = 2;

        if (GetComponent<Animator>().runtimeAnimatorController is not null)
        {
            if (GetComponent<Animator>().GetInteger("direction") != direction) GetComponent<Animator>().SetInteger("direction", direction);
        }
    }
}
