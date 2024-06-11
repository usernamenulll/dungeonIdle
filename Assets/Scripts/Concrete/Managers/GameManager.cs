using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Dungeon.Data;
using Dungeon.Abstract;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string PlayerId { get; set; }
    public string PlayerAccessToken { get; set; }

    public MetaData metaData;

    [Header("Time")]
    public DateTime ActiveTime = DateTime.Now;

    [Header("Save")]
    public Action<SaveType> SaveGameAction;
    [SerializeField] private SaveType saveType;
    public Action<Save> LoadGameAction;
    [SerializeField] private Save masterSave;
    public Save MasterSave
    {
        get { return masterSave; }
        private set { masterSave = value; }
    }

    // hit action
    public Action<GameObject> HitAction;

    private void Awake()
    {
        SingletonThis();
    }
    private void Start()
    {

    }
    public void SaveGame()
    {
        MasterSave.playerID = PlayerId;
        if (ActiveTime > DateTime.Parse(MasterSave.lastLogin))
        {
            MasterSave.lastLogin = ActiveTime.ToString();
        }
        /*
        if (MasterSave.levelDatas.Length == 0)
        {
            MasterSave.levelDatas = new LevelData[5];
        }
        */
        SaveGameAction?.Invoke(saveType);
    }
    public void NextGenSaveGame()
    {
        MasterSave.playerID = PlayerId;
        if (MasterSave.levelDatas.Length == 0)
        {
            MasterSave.levelDatas = new LevelData[5];
        }

    }
    public void LoadGame()
    {
        Debug.Log("game manager load game method");
        if (MasterSave.levelDatas.Length > 0)
        {
            LoadGameAction?.Invoke(MasterSave);
        }
    }
    public void SetMasterSave(Save save)
    {
        MasterSave = save;
    }

    public void HitHandler(GameObject hitObject)
    {
        BaseTouchObject baseTouchObject = hitObject.GetComponent<BaseTouchObject>();
        switch (baseTouchObject.objectType)
        {
            case TouchObjectType.Dungeon:
                Debug.Log("this is a dungeon");
                break;
            case TouchObjectType.Castle:
                Debug.Log("this is a castle");
                break;
            default:
                Debug.Log("wrong type");
                break;
        }
    }

    private void SingletonThis()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

}
