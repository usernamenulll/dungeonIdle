using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dungeon.Managers.Menu;
using Dungeon.Data;
using UnityEngine.SceneManagement;

namespace Dungeon.Canvas
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI playerDataLog;
        [SerializeField] MenuManager menuManager;
        [SerializeField] GameObject blockerButton;
        SaveData saveData;

        private void Awake()
        {
            saveData = FindObjectOfType<SaveData>();
        }
        private void OnEnable()
        {
            GameManager.Instance.LoadGameAction += LoadDataHandler;
        }
        private void OnDisable()
        {
            GameManager.Instance.LoadGameAction -= LoadDataHandler;
        }
        public void PlayButton()
        {
            SceneManager.LoadScene(1);
        }

        public void LoadDataHandler(Save save)
        {
            playerDataLog.text = $"player ID : {save.playerID}" + $"\nlevel count : {save.levelDatas.Length}";
            blockerButton.SetActive(!blockerButton.activeSelf);
        }



    }
}
