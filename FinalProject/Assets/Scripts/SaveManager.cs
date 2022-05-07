using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private Stats _playerStats;
    public Stats PlayerStats { get { return _playerStats; }
        set { _playerStats = value; }
    }
    private void Awake()
    {
        GetStats();
    }
    public void GetStats()
    {
        _playerStats = JsonUtility.FromJson<Stats>(File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "PlayerStats.json")));
    }
    public void SaveStats()
    {
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "PlayerStats.json"), JsonUtility.ToJson(_playerStats));
    }
    //private void Awake() 
    //{
    //    cm = GetComponent<CoinManager>();
    //    gm = GetComponent<GameManager>();
    //    string directory = Application.persistentDataPath + "/saves";
    //    if (!Directory.Exists(directory))
    //    {
    //        Directory.CreateDirectory(directory);
    //    }
    //    filePath = directory  + "/Gamesave.save";
    //}

    //public void SaveData()
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream stream = new FileStream(filePath, FileMode.Create);
    //    SaveData save = new SaveData();
    //    save.level = gm.level;
    //    save.coins = cm.coins;
    //    bf.Serialize(stream,save);
    //    stream.Close();
    //}

    //public SaveData LoadData()
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream stream = new FileStream(filePath, FileMode.Open);
    //    SaveData save = (SaveData)bf.Deserialize(stream);
    //    stream.Close();
    //    gm.level = save.level;
    //    cm.coins = save.coins;
    //    return save;
    //}
}

[System.Serializable]
public class Stats
{
    [Range(1, 20)]
    [SerializeField]  private int _unlockedLevelsCount;
    public int UnlockedLevelsCount
    {   get  { return _unlockedLevelsCount; }
        set  { _unlockedLevelsCount = value; }
    }

    [SerializeField] private int _coinsAmmount;
    public int CoinsAmmount
    {
        get { return _coinsAmmount; }
        set { _coinsAmmount = value; }
    }

    [SerializeField] private List<int> _skinsAvailable;
    public List<int> SkinsAvailable
    {
        get { return _skinsAvailable; }
        set { _skinsAvailable = value; }
    }

  
}
