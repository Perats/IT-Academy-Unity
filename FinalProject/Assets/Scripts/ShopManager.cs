using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField]
    List<Skin> skins = new List<Skin>();

    [SerializeField]
    private CoinManager _coinManager;

    [SerializeField]
    private SaveManager _saveManager;

    [SerializeField]
    private GameObject _shopItem;

    [SerializeField]
    private Transform _cardField;

    private int _coins;

    [SerializeField]
    private GameObject _buyPanel;

    private List<int> _skinsId;


    [System.Serializable]
    public struct Skin
    {
        public Sprite cardSkin;
        public int value;
        public bool isLocked;
        public int id;
    }

    void Start()
    {
        _coins = _coinManager.coins;
        _skinsId = _saveManager.PlayerStats.SkinsAvailable;
        for (int i = 0; i < skins.Count; i++)
        {
            var shopObject = Instantiate(_shopItem);
            var image = shopObject.transform.GetChild(0).gameObject.GetComponent<Image>();
            image.sprite = skins[i].cardSkin;
            var clickButton = image.transform.GetChild(0).gameObject.GetComponent<Button>();
            var lockImage = shopObject.transform.GetChild(1).gameObject.GetComponent<Image>();
            var a = _skinsId.Contains(skins[i].id);
            if (a)
            {
                lockImage.gameObject.SetActive(false);
            }
            var button = shopObject.transform.GetChild(2).gameObject.GetComponent<Button>();
            int skinId = skins[i].id;
            button.onClick.AddListener(() => BuySkin(skinId));
            clickButton.onClick.AddListener(() => SetActiveBack(skinId));
            var text = button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
            text.text = skins[i].value.ToString();
            shopObject.transform.SetParent(_cardField, false);
        }
    }

    public void BuySkin(int id)
    {
        bool value = true;
        foreach (var item in _skinsId)
        {
            if (item == id)
            {
                value = false;
            }
        }
        if (_coins >= skins[id].value && value)
        {
            _coinManager.MinusCoins(skins[id].value);
            _skinsId.Add(id);
            StartCoroutine(PanelCoroutine());
        }
    }

    IEnumerator PanelCoroutine()
    {
        _buyPanel.SetActive(true);
        yield return new WaitForSeconds(4f);
        _buyPanel.SetActive(false);
    }

    public void SetActiveBack(int id)
    {
        _saveManager.PlayerStats.ActiveSkinId = id;
        _saveManager.SaveStats();
    }
}
