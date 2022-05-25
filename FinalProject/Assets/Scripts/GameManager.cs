using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShopManager;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Sprite _cardBackSprite;

    [SerializeField]
    private List<Skin> _skins = new List<Skin>();

    [SerializeField]
    private GameObject _levelCompliteMenu;

    [SerializeField]
    private Sprite[] _cardFaceSprites;

    [SerializeField]
    private SaveManager _saveManager;

    [SerializeField]
    private LevelManager _levelManager;

    [SerializeField]
    private TMP_Text _textLevelCompliteCoins;

    [SerializeField]
    private CoinManager _coinManager;

    [SerializeField]
    private Transform _cardField;

    [SerializeField]
    private GameObject _card;


    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> cards = new List<Button>();

    private bool _firstGuess, _secondGuess;
    private int _firstGuessIndex, _secondGuessIndex;

    private int _activeSkinId;
    private int _gameGuess = 0;
    private int _countCorrectGuess;
    private static int _coins;
    public int difficulty;
    private string firstGuess, secondGuess;
    private string _rewardCoins = "100";

    private Skin _skinData;
    void Awake()
    {
        _levelManager = GetComponent<LevelManager>();
    }

    void Start()
    {
        InterstitialAd.Instance.LoadAd();
        RewardedAds.Instance.LoadAd();
        //_difficulty = _saveManager.PlayerStats.UnlockedLevelsCount;
        CreateField();
        GetCards();
        AddListeners();
        AddCardFaces();
        _gameGuess = gamePuzzles.Count / 2;
        _coins = _coinManager.coins;
        _activeSkinId = _saveManager.PlayerStats.ActiveSkinId;
    }

    private void CreateField()
    {
        var currentDifficulty = difficulty % 2 == 0 ? difficulty : difficulty - 1;
        if (currentDifficulty < 4)
        {
            currentDifficulty = 4;
        }
        for (int i = 0; i < currentDifficulty; i++)
        {
            GameObject _cardObject = Instantiate(_card);
            _cardObject.name = "" + i;
            _cardObject.transform.SetParent(_cardField, false);
        }
    }

    void AddListeners()
    {
        foreach (var card in cards)
        {
            card.onClick.AddListener(() => ShowFace(card.name));
        }
    }

    void AddCardFaces()
    {
        int counter = cards.Count;
        int index = 0;
        for (int i = 0; i < counter; i++)
        {
            if (index == counter / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(_cardFaceSprites[index]);
            index++;
        }
        Randomize(gamePuzzles);

    }

    private void Randomize(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int rnd = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[rnd];
            list[rnd] = temp;
        }
    }

    void GetCards()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Card");
        _skinData = _skins.First(i => i.id == _activeSkinId);
        for (int i = 0; i < objects.Length; i++)
        {
            cards.Add(objects[i].GetComponent<Button>());
            cards[i].image.sprite = _skinData.cardSkin;
        }
    }

    void ShowFace(string name)
    {
        if (!_firstGuess)
        {
            _firstGuess = true;
            _firstGuessIndex = int.Parse(name);
            firstGuess = gamePuzzles[_firstGuessIndex].name;
            cards[_firstGuessIndex].image.sprite = gamePuzzles[_firstGuessIndex];
        }
        else
        {
            _secondGuess = true;
            _secondGuessIndex = int.Parse(name);
            secondGuess = gamePuzzles[_secondGuessIndex].name;
            cards[_secondGuessIndex].image.sprite = gamePuzzles[_secondGuessIndex];
        }
        if (_secondGuess && _firstGuess)
        {
            BlockCards(_firstGuessIndex, _secondGuessIndex);
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {

        yield return new WaitForSeconds(2f);
        if (firstGuess == secondGuess)
        {
            cards[_firstGuessIndex].interactable = false;
            cards[_secondGuessIndex].interactable = false;

            cards[_firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            cards[_secondGuessIndex].image.color = new Color(0, 0, 0, 0);
            _firstGuess = _secondGuess = false;
            UnblockCards();
            CheckGameFinish();
        }
        else
        {
            cards[_firstGuessIndex].image.sprite = _skinData.cardSkin;
            cards[_secondGuessIndex].image.sprite = _skinData.cardSkin;
            UnblockCards();
        }
        _firstGuess = _secondGuess = false;
    }

    private void BlockCards(int firstCorrectIndex, int secondCorrectIndex)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (i != firstCorrectIndex && i != secondCorrectIndex)
            {
                cards[i].interactable = false;
            }

        }
    }
    private void UnblockCards()
    {
        foreach (var item in cards)
        {
            item.interactable = true;
        }
    }

    private void CheckGameFinish()
    {
        _countCorrectGuess++;
        if (_countCorrectGuess == _gameGuess)
        {
            _levelCompliteMenu.SetActive(true);
            _textLevelCompliteCoins.text = _rewardCoins;
            _coinManager.SaveCoins(100);
            _saveManager.PlayerStats.UnlockedLevelsCount++;
            _levelManager.RecalculateUnlockLevels();
            _saveManager.SaveStats();
        }
    }

    public void AdsReward()
    {
        _saveManager.PlayerStats.CoinsAmmount += 100;
    }
}