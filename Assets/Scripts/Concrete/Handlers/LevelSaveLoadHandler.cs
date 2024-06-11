using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dungeon.Data;
using Dungeon.Managers;
using UnityEngine;

namespace Dungeon.Handlers
{
    public class LevelSaveLoadHandler : MonoBehaviour
    {
        [SerializeField] private LevelData levelData;
        LevelManager levelManager;

        private void Awake()
        {
            levelManager = GetComponent<LevelManager>();
        }
        private void Start()
        {

        }
        private void OnEnable()
        {
            GameManager.Instance.LoadGameAction += LoadGameHandler;
            GameManager.Instance.SaveGameAction += SaveActionHandler;
            levelManager.EarnAction += AutoSaver;
        }
        private void OnDisable()
        {
            GameManager.Instance.LoadGameAction -= LoadGameHandler;
            GameManager.Instance.SaveGameAction -= SaveActionHandler;
            levelManager.EarnAction -= AutoSaver;
        }

        //Save and Get Functions
        //*****************************************

        private void SaveActionHandler(SaveType saveType)
        {
            switch (saveType)
            {
                case SaveType.Safe:
                    StartCoroutine(GetLevelData());
                    break;
                case SaveType.Unsafe:
                    StartCoroutine(NextGenSaveCoroutine());
                    break;
                default:
                    StartCoroutine(GetLevelData());
                    break;
            }
            //StartCoroutine(GetLevelData());
            //GameManager.Instance.MasterSave.levelDatas[LevelID] = levelData;
        }

        private IEnumerator GetLevelData()
        {
            Debug.Log("starting to get datas");
            levelData.dungeonDatas = GetDungeonDatas().ToArray();

            levelData.castleData = GetCastleData();

            yield return GameManager.Instance.MasterSave.levelDatas[levelManager.LevelID] = levelData;

            SaveData saveData = FindObjectOfType<SaveData>();
            saveData.cloudDataSave();
            Debug.Log("have got all datas");
        }

        private IEnumerator NextGenSaveCoroutine()
        {
            //Debug.Log("start save");
            GameManager.Instance.MasterSave.levelDatas[levelManager.LevelID] = levelData;

            SaveData saveData = FindObjectOfType<SaveData>();
            saveData.cloudDataSave();
            yield return new WaitForEndOfFrame();
            //Debug.Log("saved game with some scary code");
        }
        private CastleData GetCastleData()
        {
            CastleData castleData = FindObjectOfType<BaseCastle>().castleData;
            return castleData;

        }

        private List<DungeonData> GetDungeonDatas()
        {
            List<BaseDungeon> dgList = GetDungeons();
            List<DungeonData> _dungeonData = new List<DungeonData>();
            foreach (BaseDungeon dungeon in dgList)
            {
                _dungeonData.Add(dungeon.dungeonInfo);

            }
            return _dungeonData;
        }


        private List<BaseDungeon> GetDungeons()
        {
            List<BaseDungeon> dgList = FindObjectsOfType<BaseDungeon>().ToList();
            dgList = dgList.OrderBy(x => x.dungeonInfo.dungeonId).ToList();
            return dgList;
        }

        //Load Functions + Set Functions
        //*****************************
        private void LoadGameHandler(Save save)
        {

            levelData = save.levelDatas[levelManager.LevelID];
            if (levelData.firstload)
            {
                levelData.dungeonDatas = GetDungeonDatas().ToArray();
                levelData.firstload = false;
                GameManager.Instance.SaveGame();
            }
            //SetLevelData(save.levelDatas[levelManager.LevelID]);
            SetLevelData(levelData);
            SetDungeonDatas(levelData);
            
            //SetCastleData(levelData);
        }

        private void SetDungeonDatas(LevelData levelData)
        {
            if (levelData.dungeonDatas.Length > 0)
            {
                List<DungeonData> dgnData = levelData.dungeonDatas.ToList();
                List<BaseDungeon> dgList = GetDungeons();
                for (int i = 0; i < dgnData.Count; i++)
                {
                    dgList[i].dungeonInfo = dgnData[i];
                }
            }
        }
        private void SetCastleData(LevelData levelData)
        {
            BaseCastle baseCastle = FindObjectOfType<BaseCastle>();
            baseCastle.castleData = levelData.castleData;
        }
        private void SetLevelData(LevelData lvlData)
        {
            levelData = lvlData;
            levelManager.levelData = this.levelData;
        }

        private void AutoSaver(LevelData levelData, ulong goldEarn, ulong darkEarn)
        {
            GameManager.Instance.SaveGame();

            //can also call saveActionHandler from this script
            //SaveActionHandler(SaveType.Unsafe);
        }
    }
}
