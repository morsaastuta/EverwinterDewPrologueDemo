using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpCall : MonoBehaviour
{
    [SerializeField] GameObject area;

    public void CallForHelp(GameObject pane)
    {
        if (area.transform.childCount >= 0) ClearHelper();

        Instantiate(pane, area.transform);

        area.SetActive(true);
    }

    public void Dismiss()
    {
        area.SetActive(false);
    }

    void ClearHelper()
    {
        foreach (Transform child in area.transform)
        {
            Destroy(child);
        }
    }
}
