using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlatformSpawner[] spawners;
    private int spawnerIndex;
    private PlatformSpawner currentSpawner;
    private void Awake()
    {
        spawners = FindObjectsOfType<PlatformSpawner>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (MovingPlatform.CurrentPlatform != null)
            {
                MovingPlatform.CurrentPlatform.Stop();
            }

            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];
            currentSpawner.SpawnPlatform();
        }
    }
}
