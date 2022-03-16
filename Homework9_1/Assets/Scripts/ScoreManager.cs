using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI textCoin;
    public TextMeshProUGUI textLife;
    public TextMeshProUGUI textGameOver;
    int score;
    int lifes = 3;

    void Start()
    {
        Instance = this;
    }

    public void CollectCoin(int coinValue)
    {
        score += coinValue;
        textCoin.text = "Coins: " + score.ToString();
    }

    public void LifeCounter()
    {
        lifes--;
        textLife.text = "Life: " + lifes.ToString();
        if (lifes == 0)
        {
            textGameOver.gameObject.SetActive(true);
        }
    }
    public void Finish()
    {
        textGameOver.text = "YOU WIN!";
        textGameOver.gameObject.SetActive(true);
    }
}
