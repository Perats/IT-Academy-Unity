using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Sprite _cardBackSprite;

    [SerializeField]
    private GameObject _levelCompliteMenu;

    [SerializeField]
    private Sprite[] _cardFaceSprites;

    [SerializeField]
    private SaveManager _saveManager;

    [SerializeField]
    private LevelManager _levelManager;

    [SerializeField]
    private SceneManager _sceneManager;

    [SerializeField]
    private CoinManager _coinManager;

    [SerializeField]
    private Transform _cardField;

    [SerializeField]
    private GameObject _card;


    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> cards = new List<Button>();

    private bool firstCorrect, secondCorrect;
    private int firstCorrectIndex, secondCorrectIndex;

    private int countCorrect;
    private int gameGuess = 0;
    private int countCorrectGuess;
    public int level;
    public static int coins;
    private int _difficulty;

    private string firstGuess, secondGuess;
    void Awake()
    {
        _levelManager = GetComponent<LevelManager>();
    }

    void Start()
    {
        InterstitialAd.S.LoadAd();
        RewardedAds.S.LoadAd();
        _difficulty = _saveManager.PlayerStats.UnlockedLevelsCount;
        CreateField(_difficulty);
        GetCards();
        AddListeners();
        AddCardFaces();
        gameGuess = gamePuzzles.Count / 2;
        coins = _coinManager.coins;
    }

    private void CreateField(int difficulty)
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
        for (int i = 0; i < objects.Length; i++)
        {
            cards.Add(objects[i].GetComponent<Button>());
            cards[i].image.sprite = _cardBackSprite;
        }
    }

    void ShowFace(string name)
    {
        if (!firstCorrect)
        {
            firstCorrect = true;
            firstCorrectIndex = int.Parse(name);
            firstGuess = gamePuzzles[firstCorrectIndex].name;
            cards[firstCorrectIndex].image.sprite = gamePuzzles[firstCorrectIndex];
        }
        else
        {
            secondCorrect = true;
            secondCorrectIndex = int.Parse(name);
            secondGuess = gamePuzzles[secondCorrectIndex].name;
            cards[secondCorrectIndex].image.sprite = gamePuzzles[secondCorrectIndex];
        }
        if (secondCorrect && firstCorrect)
        {
            BlockCards(firstCorrectIndex, secondCorrectIndex);
        }

        StartCoroutine(CheckMatch());
    }

    IEnumerator CheckMatch()
    {

        yield return new WaitForSeconds(2f);
        if (firstGuess == secondGuess)
        {
            cards[firstCorrectIndex].interactable = false;
            cards[secondCorrectIndex].interactable = false;

            cards[firstCorrectIndex].image.color = new Color(0, 0, 0, 0);
            cards[secondCorrectIndex].image.color = new Color(0, 0, 0, 0);
            firstCorrect = secondCorrect = false;
            UnblockCards();
            //firstCorrect = secondCorrect = false;
            CheckGameFinish();
        }
        else
        {
            cards[firstCorrectIndex].image.sprite = _cardBackSprite;
            cards[secondCorrectIndex].image.sprite = _cardBackSprite;
            UnblockCards();
        }

        firstCorrect = secondCorrect = false;

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
        countCorrectGuess++;
        if (countCorrectGuess == gameGuess)
        {
            _levelCompliteMenu.SetActive(true);

            _coinManager.SaveCoins(100);
            _saveManager.PlayerStats.UnlockedLevelsCount++;
            _levelManager.RecalculateUnlockLevels();
            _saveManager.SaveStats();
            // _sceneManager.OpenNextScene();
            //  спрайты в атлас!

        }
    }

    public static void AdsReward()
    {
        coins += 100;

    }

    private void SaveCoins()
    {
        _saveManager.PlayerStats.CoinsAmmount += 100;
    }
}