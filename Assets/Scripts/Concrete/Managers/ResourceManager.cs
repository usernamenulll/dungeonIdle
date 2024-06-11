using System.Collections;
using System.Collections.Generic;
using Dungeon.Data;
using UnityEngine;

namespace Dungeon.Managers
{
    public class ResourceManager : MonoBehaviour
    {
        private LevelManager levelManager;
        private void Awake()
        {
            levelManager = GetComponent<LevelManager>();

        }

        private void OnEnable()
        {
            levelManager.LaunchLevelAction += StartEarning;
        }
        private void OnDisable()
        {
            levelManager.LaunchLevelAction -= StartEarning;
        }

        private void StartEarning()
        {
            StartCoroutine(EarnCoroutine());
        }

        private IEnumerator EarnCoroutine()
        {
            while (true)
            {
                LevelData ld = levelManager.levelData;
                yield return new WaitForSecondsRealtime(ld.earnRoutine);
                int ge = ld.deployedSoldier * GameManager.Instance.metaData.soldier[ld.soldierLevel];
                int de = ld.deployedRune * GameManager.Instance.metaData.rune[ld.runeLevel];
                levelManager.EarnResources((ulong)ge, (ulong)de);
            }
        }
        private void CalculateEarnings()
        {
            LevelData levelData = levelManager.levelData;

            int soldierCount = levelData.soldierCount;
            int runeCount = levelData.runeCount;

            int deployedSoldier = levelData.deployedSoldier;
            int deployedRune = levelData.deployedRune;

            int soldierLevel = levelData.soldierLevel;
            int runeLevel = levelData.runeLevel;

            int goldIncome = levelData.deployedSoldier * soldierLevel;


        }
    }
}
