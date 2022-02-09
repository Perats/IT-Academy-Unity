using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour, IPointerDownHandler
{
    public Text subTitle;
    public void OnPointerDown(PointerEventData eventData)
    {
        subTitle.text = gameObject.name.Substring(8);
    }
}
