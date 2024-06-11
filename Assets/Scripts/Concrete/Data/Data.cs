
[System.Serializable]
public class Save
{
    public string playerID;

    public string lastLogin;

    //public DungeonData[] dungeonDatas; 
    public LevelData[] levelDatas;

}
[System.Serializable]
public class LevelData
{
    public bool firstload = true;
    public float earnRoutine = 5;
    public ulong levelGold;
    public ulong levelDarkEnergy;

    public int goldIncome;
    public int darkEnergyIncome;
    
    public int soldierCount;
    public int runeCount;

    public int deployedSoldier;
    public int deployedRune;
    
    public int soldierLevel;
    public int runeLevel;

    public DungeonData[] dungeonDatas;

    public CastleData castleData;
}

//we can add some recalculate func to calculate gold and DE income

[System.Serializable]
public class DungeonData
{
    public int dungeonId;
    public int dungeonLevel;
    public int monsterCount;
    public int runeCount;
}
[System.Serializable]
public class CastleData
{
    //will hold some data about main castle skins upgrades vs vs
    //im not sure about holding soldierLevel or runeLevel's here
    //so we will hold some data and catleManager will have references about it bu probably main data will be held at levelManager

    public int castleLevel;
    //int soliderCount;
    //int runeCount;
    //int soldierLevel;
    //int runeLevel;
    //int castleSkinLevel;

}