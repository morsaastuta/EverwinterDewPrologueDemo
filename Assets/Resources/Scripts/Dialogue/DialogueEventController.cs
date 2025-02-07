using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventController : MonoBehaviour
{
    [OdinSerialize] public bool seen = false;

    DataHUB dataHUB;
    DialogueController controller;
    DialogueData data;
    DialogueEvent dialogueEvent;
    [SerializeField] int range;

    [SerializeField] public List<GameObject> obstacles = new();
    [SerializeField] Transform castParent;
    public List<Actor> cast = new();
    [SerializeField] Transform positionsParent;
    public List<Transform> positions = new();
    [SerializeField] Transform anglesParent;
    public List<Transform> angles = new();

    Vector3 prevCameraPos;
    Quaternion prevCameraAngle;
    public List<Actor> movingActor = new();

    void Start()
    {
        dataHUB = GetComponentInParent<DataHUB>();
        controller = GetComponentInParent<DialogueController>();
        data = GetComponent<DialogueData>();
        dialogueEvent = GetComponent<DialogueEvent>();

        foreach (Transform t in castParent) cast.Add(t.GetComponentInChildren<Actor>());
        foreach (Transform t in positionsParent) positions.Add(t);
        foreach (Transform t in anglesParent) angles.Add(t);

        foreach (Actor actor in cast) actor.Load(false);
    }

    void Update()
    {
        if (!dataHUB.player.isInteracting)
        {
            if (Physics.CheckSphere(transform.position, range, LayerMask.GetMask("Player"))) EnterEvent();
        }

        if (movingActor.Count > 0)
        {
            List<Actor> actorsToRemove = new();
            foreach (Actor actor in movingActor) if (actor.velocity == 0) actorsToRemove.Add(actor);
            foreach (Actor actor in actorsToRemove) movingActor.Remove(actor);
        }
    }

    void EnterEvent()
    {
        prevCameraPos = Camera.main.transform.position;
        prevCameraAngle = Camera.main.transform.rotation;
        controller.dec = this;
        controller.isEvent = true;

        controller.StartInteraction(data);
    }

    public void CheckIndex(int index)
    {
        dialogueEvent.CheckIndex(index);
    }

    public void SwitchPOV(int angle)
    {
        Camera.main.transform.position = angles[angle].position;
        Camera.main.transform.rotation = angles[angle].rotation;
    }

    public void MoveActor(int actor, int position, float speed)
    {
        cast[actor].SetTarget(positions[position], speed);
        movingActor.Add(cast[actor]);
    }

    public void ExitEvent()
    {
        Camera.main.transform.position = prevCameraPos;
        Camera.main.transform.rotation = prevCameraAngle;
        controller.dec = null;
        controller.isEvent = false;

        controller.EndInteraction();

        Seen(true);
    }

    public void Seen(bool s)
    {
        seen = s;
        UpdateState();
    }

    public void UpdateState()
    {
        if (seen) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
