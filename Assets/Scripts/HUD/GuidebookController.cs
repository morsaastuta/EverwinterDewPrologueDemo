using UnityEngine;

public class GuidebookController : MonoBehaviour
{
    [SerializeField] GameObject lorePane;
    [SerializeField] GameObject overworldPane;
    [SerializeField] GameObject combatPane;

    public void CloseAll()
    {
        lorePane.SetActive(false);
        overworldPane.SetActive(false);
        combatPane.SetActive(false);
    }

    public void Lore()
    {
        CloseAll();
        lorePane.SetActive(true);
    }

    public void Overworld()
    {
        CloseAll();
        overworldPane.SetActive(true);
    }

    public void Combat()
    {
        CloseAll();
        combatPane.SetActive(true);
    }
}
