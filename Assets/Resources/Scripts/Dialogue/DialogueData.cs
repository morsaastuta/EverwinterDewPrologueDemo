using System.Collections.Generic;
using UnityEngine;

public class DialogueData : MonoBehaviour
{
    [SerializeField] public List<string> characterNames = new();
    [SerializeField] public List<Sprite> characterFaces = new();

    [SerializeField] public List<int> order = new();

    [SerializeField] public List<string> messages = new();
}
