using TMPro;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    [SerializeField] GameObject popupPrefab;

    public void FullLaunch(int amount, string type, Vector3 position, Transform parent)
    {
        SetMesh(Launch(position, parent), amount, type);
    }

    public GameObject Launch(Vector3 position, Transform parent)
    {
        return Instantiate(popupPrefab, position, parent.rotation, parent);
    }

    public void SetMesh(GameObject newPopup, int amount, string type)
    {
        string detailedAmount = Mathf.Abs(amount).ToString();
        newPopup.GetComponentInChildren<TextMeshPro>().fontMaterial.EnableKeyword("GLOW_ON");

        switch (type)
        {
            case "HP":
                if (amount > 0)
                {
                    newPopup.GetComponentInChildren<TextMeshPro>().fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color32(0, 200, 0, 100));
                    newPopup.GetComponentInParent<CellController>().Affect(true);
                }
                else if (amount < 0)
                {
                    newPopup.GetComponentInChildren<TextMeshPro>().fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color32(200, 0, 0, 100));
                    newPopup.GetComponentInParent<CellController>().Affect(false);
                }
                else
                {
                    newPopup.GetComponentInChildren<TextMeshPro>().fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color32(100, 100, 100, 100));
                    detailedAmount = "null";
                }
                break;
            case "MP":
                if (amount > 0)
                {
                    newPopup.GetComponentInChildren<TextMeshPro>().fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color32(0, 0, 200, 100));
                    newPopup.GetComponentInParent<CellController>().Affect(true);
                }
                else if (amount < 0)
                {
                    newPopup.GetComponentInChildren<TextMeshPro>().fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color32(200, 0, 200, 100));
                }
                else
                {
                    newPopup.GetComponentInChildren<TextMeshPro>().fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color32(100, 100, 100, 100));
                    detailedAmount = "null";
                }
                break;
            case "AP":
                if (amount > 0)
                {
                    newPopup.GetComponentInChildren<TextMeshPro>().fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color32(200, 200, 0, 100));
                    newPopup.GetComponentInParent<CellController>().Affect(true);
                }
                else if (amount < 0)
                {
                    newPopup.GetComponentInChildren<TextMeshPro>().fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color32(0, 200, 200, 100));
                }
                else
                {
                    newPopup.GetComponentInChildren<TextMeshPro>().fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color32(100, 100, 100, 100));
                    detailedAmount = "null";
                }
                break;
        }

        newPopup.GetComponentInChildren<TextMeshPro>().SetText(detailedAmount);
    }
}
