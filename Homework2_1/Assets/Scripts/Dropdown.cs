using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour
{
    public Text subTitle;
    public TMPro.TMP_Dropdown dropDown;
    public void GetChangedValue()
    {
        subTitle.text = dropDown.options[dropDown.value].text;
    }
}
