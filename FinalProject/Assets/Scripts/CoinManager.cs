using UnityEngine;
using TMPro;

public class CoinManager : Singleton<CoinManager>
{
    [SerializeField]
    TMP_Text textCoins;

    public int coins;

    [SerializeField]
    private SaveManager _saveManager;
    GameManager gm;

    void Start()
    {
        coins = _saveManager.PlayerStats.CoinsAmmount;
        gm = GetComponent<GameManager>();
        textCoins.text = coins.ToString();
    }

    public void SaveCoins(int newCoins)
    {
       
       _saveManager.PlayerStats.CoinsAmmount += newCoins;
        textCoins.text = _saveManager.PlayerStats.CoinsAmmount.ToString();
        _saveManager.SaveStats();
    }

    public void MinusCoins(int coinsMin)
    {
        _saveManager.PlayerStats.CoinsAmmount -= coinsMin;
        textCoins.text = _saveManager.PlayerStats.CoinsAmmount.ToString();
       _saveManager.SaveStats();
    }
}
