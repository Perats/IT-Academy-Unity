using UnityEngine;
using TMPro;

public class CoinManager : Singleton<CoinManager>
{
    [SerializeField]
    private TMP_Text _textCoins;

    public int coins;

    [SerializeField]
    private SaveManager _saveManager;
    private GameManager _gm;

    void Start()
    {
        coins = _saveManager.PlayerStats.CoinsAmmount;
        _gm = GetComponent<GameManager>();
        _textCoins.text = coins.ToString();
    }

    public void SaveCoins(int newCoins)
    {
        _saveManager.PlayerStats.CoinsAmmount += newCoins;
        _textCoins.text = _saveManager.PlayerStats.CoinsAmmount.ToString();
        _saveManager.SaveStats();
    }

    public void MinusCoins(int coinsMin)
    {
        _saveManager.PlayerStats.CoinsAmmount -= coinsMin;
        _textCoins.text = _saveManager.PlayerStats.CoinsAmmount.ToString();
        _saveManager.SaveStats();
    }
}
