using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideAction : MonoBehaviour
{
    private Button _slideLeft;
    private Button _slideRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            print("ther is a touch");

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                print("Touch has Began");
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                print("Touch has Ended");
            }
        }
    }
}
