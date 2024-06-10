using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSFXPlayer : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] AudioMachine audioMachine;
    [SerializeField] AudioClip clip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable) audioMachine.PlaySFX(clip);
    }
}
