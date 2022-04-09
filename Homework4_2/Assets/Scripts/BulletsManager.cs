using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : Singleton<BulletsManager>
{
    [System.Serializable]
    public struct Pool 
    {
        public string tag;
        public BulletsMovement prefab;
        public int size;
    }
    [SerializeField]
    private List<Pool> _pools;
    private Dictionary<string, Queue<BulletsMovement>> _poolDictionary;
    private BulletsMovement _bullets;

    

    void Awake()
    {
        _poolDictionary = new Dictionary<string, Queue<BulletsMovement>>();
        foreach (Pool pool in _pools)
        {
            Queue<BulletsMovement> objectPool = new Queue<BulletsMovement>();
            for (int i = 0; i < pool.size; i++)
            {
                BulletsMovement _bullets = new BulletsMovement();
                _bullets = Instantiate(pool.prefab);
                _bullets.transform.parent = this.transform;
                _bullets.gameObject.SetActive(false);
                _bullets.OnTriggeredEvent += OnCollisionBulletEvent;
                objectPool.Enqueue(_bullets); 
            }
            _poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public BulletsMovement GetPoolObject(string tag,Vector3 position, Quaternion rotation)//возвращать базовый класс снаряда
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("No " + tag + "Found");
            return null;
        }

        BulletsMovement objectToSpawn = _poolDictionary[tag].Dequeue();
        if (objectToSpawn.gameObject.activeInHierarchy) //Расгиряемость DONE!
        {
            foreach (Pool pool in _pools)
            {
                if (pool.tag == tag)
                {
                    BulletsMovement bull = new BulletsMovement();
                    bull = Instantiate(pool.prefab);
                    bull.gameObject.transform.parent = this.transform;
                    bull.gameObject.SetActive(false);
                    bull.OnTriggeredEvent += OnCollisionBulletEvent;
                    _poolDictionary[tag].Enqueue(bull);
                    return bull;
                }
            }
        }
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.gameObject.SetActive(true);

        _poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    private void OnCollisionBulletEvent(BulletsMovement obj)
    {
        obj.Release();
    }
}