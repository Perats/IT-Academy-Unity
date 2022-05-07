using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    Transform cardField;

    [SerializeField]
    GameObject card;

    private void Awake()
    {
        for (int i = 0; i < 4; i++)//Add dinamic value for 14!!!!
        {
            GameObject _card = Instantiate(card);
            _card.name = "" + i;
            _card.transform.SetParent(cardField, false);
        }
    }
    void Update()
    {
        
    }
}
