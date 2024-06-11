using System;
using System.Collections;
using System.Collections.Generic;
using Dungeon.Managers;
using UnityEngine;

namespace Dungeon.Handlers
{
    public class OfflineEarnHandler : MonoBehaviour
    {
        LevelManager levelManager;
        [SerializeField] OffllineEarnCanvas offlineEarnCanvas;


        private void Awake()
        {
            levelManager = GetComponent<LevelManager>();
        }
        private void OnEnable()
        {
            GameManager.Instance.LoadGameAction += LaodGameListener;
        }
        private void OnDisable()
        {
            GameManager.Instance.LoadGameAction -= LaodGameListener;
        }

        private void LaodGameListener(Save save)
        {
            LevelData data = save.levelDatas[levelManager.LevelID];
            if (data.deployedSoldier > 0 || data.deployedRune > 0)
            {
                DateTime lastLog = DateTime.Parse(save.lastLogin);
                DateTime thisTime = GameManager.Instance.ActiveTime;

                TimeSpan diff = thisTime - lastLog;

                ulong secDiff = ((ulong)diff.TotalSeconds);
                Debug.Log(lastLog + "-" + thisTime + " \n" + diff.Seconds);

                ulong goldEarn = (ulong)(GameManager.Instance.metaData.soldier[data.soldierLevel] * data.deployedSoldier) * (secDiff / (ulong)data.earnRoutine);
                ulong darkEarn = (ulong)(GameManager.Instance.metaData.rune[data.runeLevel] * data.deployedRune) * (secDiff / (ulong)data.earnRoutine);
                Debug.Log(goldEarn + " - " + darkEarn);

                if (offlineEarnCanvas)
                {
                    offlineEarnCanvas.SetCanvas(secDiff, goldEarn, darkEarn);
                }
                else
                {
                    Debug.Log("please add canvas");
                }
            }
            else
            {
                Debug.Log("this is a clean game");
                levelManager.LaunchLevelAction?.Invoke();
            }
        }
    }
}

