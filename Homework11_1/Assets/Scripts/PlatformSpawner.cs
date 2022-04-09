using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    //[SerializeField]
    //private MovingPlatform platformPrefab;
    [SerializeField]
    private MovingPlatform platformpref;

    [SerializeField]
    private MoveDirection moveDirection;

    private Mesh mesh;

    public void SpawnPlatform() 
    {
      //var platform = Instantiate(platformpref);
        var platform = Instantiate(CreatePlatform(platformpref));

        if (MovingPlatform.LastPlatform != null && MovingPlatform.LastPlatform.gameObject != GameObject.Find("StartPlatform"))
        {
            float x = moveDirection == MoveDirection.X ? transform.position.x  : MovingPlatform.LastPlatform.transform.position.x;
            float z = moveDirection == MoveDirection.Z ? transform.position.z  : MovingPlatform.LastPlatform.transform.position.z;
            platform.transform.position = new Vector3(x ,
                MovingPlatform.LastPlatform.transform.position.y + platformpref.transform.localScale.y, z );
        }
        else
        {
            platform.transform.position = transform.position; //add x for correct[===
        }
        platform.MoveDirection = moveDirection;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, platformpref.transform.localScale);
    }

    private MovingPlatform CreatePlatform(MovingPlatform platform) 
    {
        mesh = new Mesh();
        mesh.vertices = GenerateVectices();
        mesh.triangles = GenerateTriangles();
        mesh.RecalculateNormals();

        platformpref.GetComponent<MeshFilter>().mesh = mesh;
        platform.gameObject.transform.position = new Vector3(platform.gameObject.transform.position.x + 2f, platform.gameObject.transform.position.y, platform.gameObject.transform.position.z);
        return platformpref;
    }

    Vector3[] GenerateVectices()
    {
        return new Vector3[]
        {
            new Vector3(-0.5f,0.0f,0.0f), //0
             new Vector3(-0.5f,0.0f,1.0f), //1
              new Vector3(-0.5f,1.0f,0.0f), //2  
               new Vector3(-0.5f,1.0f,1.0f), //3
                new Vector3(0.5f,0.0f,0.0f), //4
                 new Vector3(0.5f,0.0f,1.0f), //5
                  new Vector3(0.5f,1.0f,0.0f), //6
                   new Vector3(0.5f,1.0f,1.0f), //7       
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
