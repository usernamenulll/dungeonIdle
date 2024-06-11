using System.Collections;
using System.Collections.Generic;
using Dungeon.Abstract;
using Dungeon.Managers;
using UnityEngine;

public class BaseCastle : BaseTouchObject
{
    LevelManager levelManager;
    [SerializeField] public CastleData castleData;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();

        objectType = TouchObjectType.Castle;
    }
    private void OnEnable()
    {
        GameManager.Instance.LoadGameAction += LoadGameListener;
    }
    private void OnDisable()
    {
        GameManager.Instance.LoadGameAction -= LoadGameListener;
    }

    private void LoadGameListener(Save save)
    {
        castleData = save.levelDatas[levelManager.LevelID].castleData;
    }
}
