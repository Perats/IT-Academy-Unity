using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour, IPointerDownHandler
{
    public Button buttonColor;
    [SerializeField] public PlainesController plainesList;
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
        var dd = plainesList;
        var a = GameObject.FindGameObjectsWithTag("Test");
        for (int i = 0; i < a[0].transform.childCount - 1; i++)
        {
            var tt = a[0].transform.GetChild(i);
            var x = tt.GetComponents<MeshRenderer>();
             x[0].material.color = currentColor;
           
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      
        
    }
}
