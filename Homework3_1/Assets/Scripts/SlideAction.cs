using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideAction : MonoBehaviour
{
    public Button slide;
    [SerializeField]
    private GameObject[] _arrayOfPlanes;
    static int currentNumber = 0;

    public void ShowNextPlane(bool value)
    {
        _arrayOfPlanes[currentNumber].SetActive(false);

        if (value)
        {
            if (currentNumber == _arrayOfPlanes.Length - 1)
            {
                _arrayOfPlanes[0].SetActive(true);
                _arrayOfPlanes[0].transform.position = new Vector3(0, 0, 0);
                currentNumber = 0;
            }
            else
            {
                currentNumber++;
                _arrayOfPlanes[currentNumber].SetActive(true);
                _arrayOfPlanes[currentNumber].transform.position = new Vector3(0, 0, 0);
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
