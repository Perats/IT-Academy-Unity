using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Button button;
    public Camera secondCamera;
    public void ChangeCameraView(int possition)
    {
        switch (possition)
        {
            case 0:
                secondCamera.transform.position = new Vector3(8f,0f,0f);
                secondCamera.transform.rotation = Quaternion.Euler(180f, 90f, 0f);
                return;
            case 1:
                secondCamera.transform.position = new Vector3(0f, 3f, 0f);
                secondCamera.transform.rotation = Quaternion.Euler(5f, 0f, 0f);
                return;
            case 2:
                secondCamera.transform.position = new Vector3(0f, 3f, 0f);
                secondCamera.transform.rotation = Quaternion.Euler(80f, 0f, 0f);
                return;
            case 3:
                secondCamera.transform.position = new Vector3(-1f, -4f, -2f);
                secondCamera.transform.rotation = Quaternion.Euler(-80f, 0f, 0f);
                return;
            default:
                break;
        }
    }
}
