using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDragger : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }
}
