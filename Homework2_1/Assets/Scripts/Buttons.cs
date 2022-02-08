using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Buttons : MonoBehaviour, IPointerDownHandler
{
    public Text subTitle;
    public Button DisableButton;
    public Button Button1;
    public Button Button2;

    public void OnPointerDown(PointerEventData eventData)
    {
        //Button1.interactable = false;
       // Debug.Log(this.gameObject.name + " Was Clicked.");
        subTitle.text = gameObject.name + " Clicked";
    }

    public void Test() { }
}
