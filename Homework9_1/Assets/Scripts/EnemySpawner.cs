using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemy;
    public Rigidbody2D Bullet;
    private Vector2 _spawnPlace;

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        _spawnPlace = new Vector2(1.58f, transform.position.y + 0.2f);
        Instantiate(Enemy[0], _spawnPlace, Quaternion.identity);
    }
}
