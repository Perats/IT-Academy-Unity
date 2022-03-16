using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] gameObjects;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateObject();
        }
    }

    private void CreateObject() 
    {
       Instantiate(gameObjects[Random.Range(0, 3)], new Vector3(1f,0f,-50f), Quaternion.identity);
    }
}
