using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x+1f, _player.transform.position.y+3f, -10f);
    }
}
