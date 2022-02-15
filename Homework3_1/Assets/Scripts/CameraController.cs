using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Button button;
    [SerializeField] public PlainesController plainesList;
    public Camera cameraTest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCameraView(int possition)
    {
        Vector3 left = new Vector3(-11.88f, -1.1f, 274.8f);
        Vector3 Up = new Vector3(0f, 90f, 0f);
        Vector3 Down = new Vector3(180f, -90f, 0f);
        Vector3 Face = new Vector3(285.5f, -152.40f, 421.8f);
       var a = Quaternion.Euler(-11.88f, -1.1f, 274.8f);
        //   cameraTest.transform.position = left;
        cameraTest.transform.rotation = a;
        // var cam0 = cameras[0].GetComponent<Camera>();
    }
}
