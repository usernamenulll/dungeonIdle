using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dungeon.Data;
using UnityEngine;

namespace Dungeon.Managers.Menu
{
    public class MenuManager : MonoBehaviour
    {
        //public Action<string> menuLogAction;

        //public Func<Task> SignInAction;
        private SaveData SaveData;

        [SerializeField] AuthenticationManager authenticationManager;
        void Start()
        {
            //SignInAction += authenticationManager.SignInAnonymouslyAsync;


            GameManager.Instance.SaveGameAction += MenuSaveListener;
        }

        private void OnDisable()
        {
            //SignInAction -= authenticationManager.SignInAnonymouslyAsync;
            
            GameManager.Instance.SaveGameAction -= MenuSaveListener;
        }

        private void MenuSaveListener(SaveType saveType)
        {
            SaveData saveData = FindObjectOfType<SaveData>();
            saveData.cloudDataSave();
        }

        public void LogPlayerIn()
        {
            SaveData saveData = FindObjectOfType<SaveData>();
            saveData.cloudDataLoad();
        }

    }
}
