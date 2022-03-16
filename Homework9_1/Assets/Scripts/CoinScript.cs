using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public Rigidbody2D coinPrefab;
    public int coinValue = 1;

    void Start()
    {
        
    }
    private void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            ScoreManager.Instance.CollectCoin(coinValue);
            Destroy(collision.gameObject);
        }
    }
    public void CreateCoin(Vector3 _spawnCoinPlace)
    {
        Instantiate(coinPrefab, _spawnCoinPlace, Quaternion.identity);
    }
}
