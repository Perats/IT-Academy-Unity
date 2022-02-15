using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Camera _camera;
    public GameObject cube;
    public GameObject sphere;
    public GameObject capsule;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //   var a =  this.cube.GetComponents<MeshRenderer>();
    //  a[0].material.color = Color.red;
    //    }
    //    if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        cube = GameObject.Find("Cube");
    //        var a = cube.GetComponents<MeshRenderer>();
    //        a[0].material.color = Color.white;
    //        Debug.Log("Space key was released.");
    //    }
    //new Vector3 = Transform.rotate();
    //    //if (Input.GetKeyDown(KeyCode.Keypad0))
    //    //{
    //    //    cube = GameObject.Find("Cube");
    //    //    var a = cube.GetComponents<MeshRenderer>();
    //    //    a[0].material.color = Color.red;
    //    //}

    //    //if (Input.GetKeyUp(KeyCode.Keypad0))
    //    //{
    //    //    cube = GameObject.Find("Cube");
    //    //    var a = cube.GetComponents<MeshRenderer>();
    //    //    a[0].material.color = Color.white;
    //    //    Debug.Log("Space key was released.");
    //   // }
    //}
}
