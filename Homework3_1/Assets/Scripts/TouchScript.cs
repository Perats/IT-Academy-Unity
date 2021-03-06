using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
{
    [SerializeField] public PlainesController plainesList;
    private float width;
    private float height;

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (300 < touch.position.y && touch.position.y < 1100)
            {
                foreach (var activePlaine in plainesList.planesList)
                {
                    if (activePlaine.active)
                    {
                        Vector3 pos = touch.position;
                       // pos.x = (pos.x - width);
                        pos.y = (pos.y - height);
                        var touchRotation = Quaternion.Euler(0.0f, pos.y, 0.0f);
                        activePlaine.transform.rotation = touchRotation;
                    }
                }
               
            }
           
        }
    }
}
