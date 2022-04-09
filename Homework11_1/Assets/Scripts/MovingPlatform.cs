using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingPlatform : MonoBehaviour
{
    public static MovingPlatform CurrentPlatform { get; private set; }
    public static MovingPlatform LastPlatform { get; private set; }
    public MoveDirection MoveDirection { get; set; }

    [SerializeField]
    private float speed = 1f;

    Mesh mesh;

    private void Awake()
    { 
        if (LastPlatform == null)
        {
            LastPlatform = GameObject.Find("StartPlatform").GetComponent<MovingPlatform>();
        }
        CurrentPlatform = this;
        GetComponent<Renderer>().material.color = GetRandomColor();
        transform.localScale = new Vector3( LastPlatform.transform.localScale.x, transform.localScale.y, LastPlatform.transform.localScale.z);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0,1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    internal void Stop()
    {
        speed = 0;
        float deltapossition = GetDelta();

        float max = MoveDirection == MoveDirection.Z ? LastPlatform.transform.localScale.z : LastPlatform.transform.localScale.x;
        if (Mathf.Abs(deltapossition) >= max)
        {
            LastPlatform = null;
            CurrentPlatform = null;
            SceneManager.LoadScene(0);
        }
        float direction = deltapossition > 0 ? 1f : -1f;
        if (MoveDirection == MoveDirection.Z)
        {
            SplitPlatformZ(deltapossition, direction);
        }
        else
        {
            SplitPlatformX(deltapossition, direction);
        }
        LastPlatform = this;
    }

    private float GetDelta()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            return transform.position.z - LastPlatform.transform.position.z;
        }
        else
        {
            return transform.position.x - LastPlatform.transform.position.x;
        }
       
    }

    private void SplitPlatformZ(float deltapossition, float direction)
    {
        float newZSize = LastPlatform.transform.localScale.z - Mathf.Abs(deltapossition);
        float fallPlatformSize = transform.localScale.z - newZSize;
        float newZPossition = LastPlatform.transform.position.z + (deltapossition / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPossition);

        float platformEdge = transform.position.z + (newZSize / 2f * direction);
        float follingPlatformZPossition = platformEdge + fallPlatformSize / 2f * direction;
        SpawnDropPlatform(follingPlatformZPossition, fallPlatformSize);
    }

    private void SplitPlatformX(float deltapossition, float direction)
    {
        float newXSize = LastPlatform.transform.localScale.x - Mathf.Abs(deltapossition);
        float fallPlatformSize = transform.localScale.x - newXSize;
        float newXPossition = LastPlatform.transform.position.x + (deltapossition / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPossition, transform.position.y, transform.position.z);

        float platformEdge = transform.position.x + (newXSize / 2f * direction);
        float follingPlatformXPossition = platformEdge + fallPlatformSize / 2f * direction;
        SpawnDropPlatform(follingPlatformXPossition, fallPlatformSize);
    }

    private void SpawnDropPlatform(float follingPlatformZPossition, float fallPlatformSize)
    {
        //var platform = Instantiate(CreatePlatform());
        var platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (MoveDirection == MoveDirection.Z)
        {
            platform.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallPlatformSize);
            platform.transform.position = new Vector3(transform.position.x, transform.position.y, follingPlatformZPossition);
        }
        else
        {
            platform.transform.localScale = new Vector3(fallPlatformSize,transform.localScale.y, transform.localScale.z );
            platform.transform.position = new Vector3(follingPlatformZPossition,transform.position.y, transform.position.z);
        }

        platform.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        platform.gameObject.AddComponent<Rigidbody>();
        Destroy(platform.gameObject, 1f);
    }

    private void Update()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
           
        }
        else
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }
    }

    public MovingPlatform CreatePlatform()
    {
        var a = this;
        mesh = new Mesh();
        mesh.vertices = GenerateVectices();
        mesh.triangles = GenerateTriangles();
        mesh.RecalculateNormals();

        a.GetComponent<MeshFilter>().mesh = mesh;

        return a;
    }

    Vector3[] GenerateVectices()
    {
        return new Vector3[]
        {
            new Vector3(0.0f,0.0f,0.0f), //0
             new Vector3(0.0f,0.0f,1.0f), //1
              new Vector3(0.0f,1.0f,0.0f), //2  
               new Vector3(0.0f,1.0f,1.0f), //3
                new Vector3(1.0f,0.0f,0.0f), //4
                 new Vector3(1.0f,0.0f,1.0f), //5
                  new Vector3(1.0f,1.0f,0.0f), //6
                   new Vector3(1.0f,1.0f,1.0f), //7       
        };
    }
    int[] GenerateTriangles()
    {
        return new int[]
        {
            0,2,4,2,6,4,
            4,6,5,6,7,5,
            4,5,0,5,1,0,
            1,3,0,3,2,0,
            5,7,1,7,3,1,
            2,3,6,3,7,6,
        };
    }
}
