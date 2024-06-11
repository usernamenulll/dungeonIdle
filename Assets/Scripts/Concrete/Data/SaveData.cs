using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dungeon.Managers;
using Unity.Services.CloudSave;
using Unity.Services.Authentication;
using UnityEngine.SceneManagement;

namespace Dungeon.Data

{
    public class SaveData : MonoBehaviour
    {
        //private Save playerSave = new Save();
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                CreateSave();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                cloudDataLoad();
            }
        }
        private void CreateSave()
        {
            GameManager.Instance.SaveGame();
        }
        public async void cloudDataSave()
        {
            Save playerSave = GameManager.Instance.MasterSave;
            string playerData = JsonUtility.ToJson(playerSave);
            var dt = new Dictionary<string, object>();
            dt.Add("data", playerData);
            if (AuthenticationService.Instance.IsAuthorized)
            {
                await CloudSaveService.Instance.Data.ForceSaveAsync(dt);
                //Debug.Log("save to cloud " + this.gameObject.name);
            }
            else
            {
                Debug.Log("player is not authorized");
            }
        }
        public async void cloudDataLoad()
        {
            if (AuthenticationService.Instance.IsAuthorized)
            {
                Save playerSave = new Save();
                Dictionary<string, string> savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "data" });
                if (savedData.Count > 0)
                {
                    string plyrData = savedData["data"];
                    playerSave = JsonUtility.FromJson<Save>(plyrData);
                    Debug.Log("loading from cloud");
                    //GameManager.Instance.MasterSave = playerSave;
                    //GameManager.Instance.LoadGame(playerSave);
                    GameManager.Instance.SetMasterSave(playerSave);
                    GameManager.Instance.LoadGame();
                }
                else
                {
                    Debug.Log("player has no data first time sign in");
                    //PlayerPrefs.SetString("FirstTime" , "Yes");
                    firstCloudDataSave();
                }
            }
            else
            {
                Debug.Log("player is not authorized");
            }

        }
        private async void firstCloudDataSave()
        {
            GameManager.Instance.MasterSave.playerID = GameManager.Instance.PlayerId;

            if (GameManager.Instance.MasterSave.levelDatas.Length == 0)
            {
                GameManager.Instance.MasterSave.levelDatas = new LevelData[5];
            }
            GameManager.Instance.MasterSave.lastLogin = System.DateTime.Now.ToString();
            Save playerSave = GameManager.Instance.MasterSave;
            string playerData = JsonUtility.ToJson(playerSave);
            var dt = new Dictionary<string, object>();
            dt.Add("data", playerData);
            if (AuthenticationService.Instance.IsAuthorized)
            {
                await CloudSaveService.Instance.Data.ForceSaveAsync(dt);
                Debug.Log("first time save to cloud");
                GameManager.Instance.LoadGame();
            }
            else
            {
                Debug.Log("player is not authorized");
            }
        }
    }
}

/*
public void dataSaver()
        {
            playerSave = CreateSave();
            string playerData = JsonUtility.ToJson(playerSave);
            var dt = new Dictionary<string, object>();
            dt.Add("data", playerData);
            PlayerPrefs.SetString("data", playerData);
            Debug.Log(playerData);
        }
public void dataLoader()
        {
            if (PlayerPrefs.HasKey("data"))
            {
                string plyrData = PlayerPrefs.GetString("data");
                playerSave = JsonUtility.FromJson<Save>(plyrData);

                //GameManager.Instance.LoadGameAction?.Invoke(playerSave);
                GameManager.Instance.LoadGame(playerSave);
                Debug.Log("loading");
            }
            else
            {
                Debug.Log("cant load data because there is no data please save data first");
            }
        }

*/
