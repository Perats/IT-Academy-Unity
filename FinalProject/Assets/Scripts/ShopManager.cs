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
    GameObject shopItem;

    [SerializeField]
    private Transform _cardField;

    private int coins;

    [SerializeField]
    private GameObject BuyPanel;

    private List<int> skinsId;


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
        coins = _coinManager.coins;
        skinsId = _saveManager.PlayerStats.SkinsAvailable;
        for (int i = 0; i < skins.Count; i++)
        {
            var shopObject = Instantiate(shopItem);
            var image = shopObject.transform.GetChild(0).gameObject.GetComponent<Image>();
            image.sprite = skins[i].cardSkin;
            var lockImage = shopObject.transform.GetChild(1).gameObject.GetComponent<Image>();


            var a = skinsId.Contains(skins[i].id);

            if (a)
            {
                lockImage.gameObject.SetActive(false);
            }

            var button = shopObject.transform.GetChild(2).gameObject.GetComponent<Button>();
            int skinId = skins[i].id;
            button.onClick.AddListener(() => BuySkin(skinId));
            var text = button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
            text.text = skins[i].value.ToString();
            shopObject.transform.SetParent(_cardField, false);
        }
    }

    public void BuySkin(int id)
    {
        bool value = true;
        foreach (var item in skinsId)
        {
            if (item == id)
            {
                value = false;
            }
        }
        if (coins >= skins[id].value && value)
        {
            //coins -= skins[id].value;
            _coinManager.MinusCoins(skins[id].value);
            skinsId.Add(id);
            StartCoroutine(PanelCoroutine());
        }
    }

    IEnumerator PanelCoroutine()
    {
        BuyPanel.SetActive(true);
        yield return new WaitForSeconds(4f);
        BuyPanel.SetActive(false);
    }
}
