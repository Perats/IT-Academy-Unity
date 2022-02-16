using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public Button buttonColor;
    [SerializeField] PlainesController plainesList;
    private Color currentColor;

    public void ChangePlainColor(int colorValue)
    {

        switch (colorValue)
        {
            case 1:
                currentColor = Color.yellow;
                break;
            case 2:
                currentColor = Color.red;
                break;
            case 3:
                currentColor = Color.blue;
                break;
            case 4:
                currentColor = Color.green;
                break;
            default:
                break;
        }
        foreach (var activePlaine in plainesList.planesList)
        {
            if (activePlaine.activeSelf)
            {
                var getMeshData = activePlaine.GetComponents<MeshRenderer>();
                getMeshData[0].material.color = currentColor;
            }
        }
    }
}
