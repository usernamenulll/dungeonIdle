using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dungeon.Managers;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI darkEnergyText;
    [SerializeField] private TextMeshProUGUI soldierText;
    [SerializeField] private TextMeshProUGUI runeText;

    LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Start()
    {
        ResourceUpdater(levelManager.levelData, 0, 0);
    }
    private void OnEnable()
    {
        levelManager.EarnAction += ResourceUpdater;
        GameManager.Instance.LoadGameAction += LoadLevelListener;
    }
    private void OnDisable()
    {
        levelManager.EarnAction -= ResourceUpdater;
        GameManager.Instance.LoadGameAction -= LoadLevelListener;
    }

    private void ResourceUpdater(LevelData data, ulong goldEearn, ulong darkEarn)
    {
        goldText.text = data.levelGold.ToString();
        darkEnergyText.text = data.levelDarkEnergy.ToString();
        soldierText.text = data.soldierCount.ToString() + " (" + data.deployedSoldier + ") ";
        runeText.text = data.runeCount.ToString() + " (" + data.deployedRune + ") ";
    }
    private void LoadLevelListener(Save save)
    {
        LevelData data = save.levelDatas[levelManager.LevelID];

        goldText.text = data.levelGold.ToString();
        darkEnergyText.text = data.levelDarkEnergy.ToString();
        soldierText.text = data.soldierCount.ToString() + " (" + data.deployedSoldier + ") ";
        runeText.text = data.runeCount.ToString() + " (" + data.deployedRune + ") ";
    }
}
