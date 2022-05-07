using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private SaveManager _saveManager;

    [SerializeField]
    private List<Button> _levels;

    private int _unlockLevelsCount = 1;

    private void Start()
    {
        _unlockLevelsCount = _saveManager.PlayerStats.UnlockedLevelsCount;
        RecalculateUnlockLevels();
    }

    public void RecalculateUnlockLevels() 
    {
        for (int i = _unlockLevelsCount; i < _levels.Count; i++)
        {
            _levels[i].interactable = false;
            GameObject lockImage = _levels[i].transform.GetChild(0).gameObject;
            lockImage.SetActive(true);
        }
    }
}
