using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Toggles : MonoBehaviour, IPointerDownHandler
{
    public Toggle toggles;
    public Text subTitle;
    public void OnPointerDown(PointerEventData eventData)
    {
        subTitle.text = gameObject.name;
    }
}
