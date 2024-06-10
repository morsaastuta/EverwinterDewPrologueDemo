using UnityEngine;

public class AuthorController : MonoBehaviour
{
    [SerializeField] GameObject authorPane;

    public void AuthorInfo(bool state)
    {
        authorPane.SetActive(state);
    }
}
