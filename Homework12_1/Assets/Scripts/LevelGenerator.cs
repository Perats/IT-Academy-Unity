using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> walls;
    public NavMeshSurface navMeshSurface;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateLevel());
    }

    IEnumerator GenerateLevel()
    {
        foreach (var wall in walls)
        {
            wall.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        navMeshSurface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
