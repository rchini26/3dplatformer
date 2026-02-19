using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Core.Singleton;
using Items;

public class SaveManager : Singleton<SaveManager>
{
    private SaveSetup _saveSetup;
    private string _path;
    
    public int lastLevel;
    public Action<SaveSetup> FileLoaded;

    public SaveSetup SaveSetup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        _path = Application.streamingAssetsPath + "/save.json";
    }

    void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = PlayerPrefs.GetString("PlayerName");
    }
    
    void Start()
    {
        Load();
    }
    
    private void Save()
    {
        string json = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(json);
        SaveFile(json);
    }

    public void SaveItems()
    {
        _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(ItemType.Coin).soInt.value;
        _saveSetup.lifePack = Items.ItemManager.Instance.GetItemByType(ItemType.LifePack).soInt.value;
        Save();
    }
    
    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        Save();
    }

    public void SaveName(string name)
    {
        _saveSetup.playerName = name;
        Save();
    }
    
    private void SaveFile(string json)
    {
        string fileLoaded = "";
        if (File.Exists(_path)) fileLoaded = File.ReadAllText(_path);
        
        File.WriteAllText(_path, json);
    }

    private void Load()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }
        FileLoaded?.Invoke(_saveSetup);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public int coins;
    public int lifePack;
    
    public string playerName;
}
