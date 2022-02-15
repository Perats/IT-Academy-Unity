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
           
            foreach (var activePlaine in plainesList.planesList)
            {
                if (activePlaine.active)
                {
                    Vector3 pos = touch.position;
                    pos.x = (pos.x - width) ;
                    pos.y = (pos.y - height);
                    var touchRotation = Quaternion.Euler(pos.x, pos.y, pos.z);
                    activePlaine.transform.rotation = touchRotation;
                }
            }
        }
    }
}
