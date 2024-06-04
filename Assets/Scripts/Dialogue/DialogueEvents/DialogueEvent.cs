using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    [SerializeField] protected DialogueEventController dec;

    public virtual void CheckIndex(int messageIndex)
    {
    }

    protected void CommonIntro()
    {
        foreach (GameObject obj in dec.obstacles) obj.SetActive(false);
        foreach (Actor actor in dec.cast) actor.Load(true);
    }

    protected void CommonOutro()
    {
        foreach (Actor actor in dec.cast) actor.Load(false);
        foreach (GameObject obj in dec.obstacles) obj.SetActive(true);
    }
}
