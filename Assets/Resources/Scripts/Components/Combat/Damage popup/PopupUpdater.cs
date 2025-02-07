using TMPro;
using UnityEngine;

public class PopupUpdater : MonoBehaviour
{
    float beforeDisappear = .5f;
    Color textColor;

    void Start()
    {
        textColor = GetComponentInChildren<TextMeshPro>().color;
    }

    void Update()
    {
        transform.position += new Vector3(0, 0.2f) * Time.deltaTime;

        if (beforeDisappear <= 0)
        {
            textColor.a -= Time.deltaTime;
            GetComponentInChildren<TextMeshPro>().color = textColor;

            if (textColor.a <= 0) Destroy(gameObject);
        }
        else beforeDisappear -= Time.deltaTime;
    }
}
