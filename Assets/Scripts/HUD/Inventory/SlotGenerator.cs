using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

public class SlotGenerator : MonoBehaviour
{
    [SerializeField] GameObject defaultSlot;

    public GameObject Generate()
    {
        return Instantiate(defaultSlot, Vector3.zero, Quaternion.identity);
    }

    public void Generate(int amount)
    {
        for (int i = 0; i < amount; i++) Instantiate(defaultSlot, Vector3.zero, Quaternion.identity);
    }
}