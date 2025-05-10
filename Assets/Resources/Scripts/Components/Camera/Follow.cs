using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] GameObject item;

    void FixedUpdate()
    {
        transform.position = item.transform.position;
    }
}
