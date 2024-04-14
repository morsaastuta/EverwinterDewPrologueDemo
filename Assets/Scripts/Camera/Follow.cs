using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject item;

    void Update()
    {
        transform.position = item.transform.position;
    }
}
