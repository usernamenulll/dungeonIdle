using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Dungeon.Managers;

public class OffllineEarnCanvas : MonoBehaviour
{
    [SerializeField] GameObject holderPanel;
    [SerializeField] TextMeshProUGUI secText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI darkText;
    [SerializeField] Button CloseButton;

    ulong goldEarn;
    ulong darkEarn;

    // Start is called before the first frame update
    void Start()
    {
        CloseButton.onClick.AddListener(OfflinePopCloseButton);
        //holderPanel.SetActive(false);
    }
    public void SetCanvas(ulong sec, ulong gold, ulong dark)
    {
        SetUIDatas(sec, gold, dark);
        goldEarn = gold;
        darkEarn = dark;
        holderPanel.SetActive(true);
    }

    private void SetUIDatas(ulong _sec, ulong _gold, ulong _dark)
    {
        secText.text = _sec.ToString() + " Seconds";

        //goldText.text = _gold.ToString() + " Gold";
        //darkText.text = _dark.ToString() + " Dark Energy";
        StartCoroutine(AnimateValues(_gold , _dark));
    }
    private void OfflinePopCloseButton()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.EarnResources(goldEarn, darkEarn);
        levelManager.LaunchLevelAction?.Invoke();
        holderPanel.SetActive(false);
    }

    IEnumerator AnimateValues(ulong desiredGold, ulong desiredDark)
    {
        float FPS = 30;
        float animTime = 1.5f;
        ulong totalFrame = (ulong)(FPS * animTime);

        float timePass = 0;

        ulong _gold = 0;
        ulong _dark = 0;


        while (animTime > timePass)
        {
            Debug.Log("working");
            //_gold += Mathf.FloorToInt(desiredGold / totalFrame);
            _gold += desiredGold / totalFrame;
            //_dark += Mathf.FloorToInt(desiredDark / totalFrame);
            _dark += desiredDark / totalFrame;

            goldText.text = _gold.ToString("D");
            darkText.text = _dark.ToString("D");

            timePass += (1f / FPS);
            if (timePass >= animTime)
            {
                _gold = desiredGold;
                _dark = desiredDark;
                goldText.text = _gold.ToString("D");
                darkText.text = _dark.ToString("D");
                break;
            }
            yield return new WaitForSeconds(1f / FPS);
        }
    }
}
