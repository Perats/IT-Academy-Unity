using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Buttons : MonoBehaviour, IPointerDownHandler
{
    public Text subTitle;
    public Button Button;
    public void OnPointerDown(PointerEventData eventData)
    {
        subTitle.text = gameObject.name + " Clicked";
    }
}
