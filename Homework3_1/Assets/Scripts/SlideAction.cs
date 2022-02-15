using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideAction : MonoBehaviour
{
    public Button slide;
    [SerializeField]
    private GameObject[] _arrayOfPlanes;
    int currentNumber = 0;

    public void ShowNextPlane(bool value)
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _arrayOfPlanes[currentNumber].SetActive(false);
                if (value)
                {
                    if (currentNumber == _arrayOfPlanes.Length - 1)
                    {
                        _arrayOfPlanes[0].SetActive(true);
                        currentNumber = 0;
                    }
                    else
                    {
                        currentNumber++;
                        _arrayOfPlanes[currentNumber].SetActive(true);
                    }
                }
                else
                {

                    if (currentNumber == 0)
                    {
                        _arrayOfPlanes[_arrayOfPlanes.Length - 1].SetActive(true);
                        currentNumber = _arrayOfPlanes.Length - 1;
                    }
                    else
                    {
                        currentNumber--;
                        _arrayOfPlanes[currentNumber].SetActive(true);
                    }

                }

            }
        }
    }
}
