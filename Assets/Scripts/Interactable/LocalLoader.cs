using System.Collections.Generic;
using UnityEngine;

public class LocalLoader : MonoBehaviour
{
    [SerializeField] bool spherical;
    [SerializeField] int size;
    [SerializeField] float range;
    [SerializeField] Transform center2;
    [SerializeField] float range2;
    [SerializeField] Transform center3;
    [SerializeField] float range3;
    List<GameObject> allObjects = new();
    bool shown = true;

    void Start()
    {
        foreach (Transform child in transform) allObjects.Add(child.gameObject);
    }

    void FixedUpdate()
    {
        Check();
    }

    void Check()
    {
        if (!shown)
        {
            switch (size)
            {
                case 1:
                    if (Physics.CheckSphere(transform.position, range, LayerMask.GetMask("Player"))) Show();
                    break;
                case 2:
                    if (Physics.CheckSphere(transform.position, range, LayerMask.GetMask("Player")) || Physics.CheckSphere(center2.position, range2, LayerMask.GetMask("Player"))) Show();
                    break;
                case 3:
                    if (Physics.CheckSphere(transform.position, range, LayerMask.GetMask("Player")) || Physics.CheckSphere(center2.position, range2, LayerMask.GetMask("Player")) || Physics.CheckSphere(center3.position, range3, LayerMask.GetMask("Player"))) Show();
                    break;
            }
        }
        else if (shown)
        {
            switch (size)
            {
                case 1:
                    if (!Physics.CheckSphere(transform.position, range, LayerMask.GetMask("Player"))) Hide();
                    break;
                case 2:
                    if (!Physics.CheckSphere(transform.position, range, LayerMask.GetMask("Player")) && !Physics.CheckSphere(center2.position, range2, LayerMask.GetMask("Player"))) Hide();
                    break;
                case 3:
                    if (!Physics.CheckSphere(transform.position, range, LayerMask.GetMask("Player")) && !Physics.CheckSphere(center2.position, range2, LayerMask.GetMask("Player")) && !Physics.CheckSphere(center3.position, range3, LayerMask.GetMask("Player"))) Hide();
                    break;
            }
        }
    }

    void Show()
    {
        shown = true;
        foreach (GameObject obj in allObjects)
        {
            obj.SetActive(true);
            if (obj.GetComponentInChildren<Encounter>()) obj.GetComponentInChildren<Encounter>().UpdateState();
            if (obj.GetComponentInChildren<Chest>()) obj.GetComponentInChildren<Chest>().UpdateState();
            if (obj.GetComponentInChildren<DialogueEventController>()) obj.GetComponentInChildren<DialogueEventController>().UpdateState();
        }
    }

    void Hide()
    {
        shown = false;
        foreach (GameObject obj in allObjects) obj.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        if (!spherical)
        {
            Gizmos.DrawWireSphere(transform.position, range);
            if (range2 > 0) Gizmos.DrawWireSphere(center2.position, range2);
            if (range3 > 0) Gizmos.DrawWireSphere(center3.position, range3);
        }
        else
        {
            Gizmos.DrawSphere(transform.position, range);
            if (range2 > 0) Gizmos.DrawSphere(center2.position, range2);
            if (range3 > 0) Gizmos.DrawSphere(center3.position, range3);
        }
    }
}