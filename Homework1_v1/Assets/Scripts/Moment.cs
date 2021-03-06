using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moment : MonoBehaviour
{
    // public GameObject prefab;
    public List<GameObject> PrefabList;

    public GameObject PingPong;

    public GameObject Rotator;

    public GameObject Teleport;

    public GameObject Scaler;

    private GameObject instance;

    public float speed = 2.0f;

    void Start()
    {
        PrefabList = new List<GameObject>(4)
        {
            PingPong,
            Rotator,
            Teleport,
            Scaler
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject currentPrefab = PrefabList[Random.Range(0, 4)];
            if (currentPrefab == null)
            {
                Debug.LogError("Prefab is not null");
                return;
            }
            if (instance != null)
            {
                Destroy(instance);
            }
            var rotation = Quaternion.identity;// было 
          
            var position = new Vector3(0.0f, 0.0f, 0.0f);

            instance = Instantiate(currentPrefab, position, rotation);

            //отдельный скрипт который уже висит на префабе и после создания преваба сразу начинает жить 
        }

    }
}
