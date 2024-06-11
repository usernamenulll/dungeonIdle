using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dungeon.Data;
using UnityEngine;
namespace Dungeon.Managers
{
    public class LevelManager : MonoBehaviour
    {

        public LevelData levelData;
        public int LevelID;

        public Action LaunchLevelAction;
        //[SerializeField] public LevelData levelData;

        public Action<LevelData, ulong, ulong> EarnAction;

        private void Start()
        {
            GameManager.Instance.LoadGame();

        }

        public void EarnResources(ulong goldEarn, ulong darkEarn)
        {
            levelData.levelGold += goldEarn;
            levelData.levelDarkEnergy += darkEarn;

            EarnAction?.Invoke(levelData, goldEarn, darkEarn);
        }
    }

}