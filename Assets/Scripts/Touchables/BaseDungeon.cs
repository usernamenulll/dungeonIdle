using System;
using System.Collections;
using System.Collections.Generic;
using Dungeon.Abstract;
using UnityEngine;

[Serializable]
public class BaseDungeon : BaseTouchObject
{
    public DungeonData dungeonInfo;

    private void Awake()
    {
        dungeonInfo.dungeonId = transform.GetSiblingIndex();
    }
}
