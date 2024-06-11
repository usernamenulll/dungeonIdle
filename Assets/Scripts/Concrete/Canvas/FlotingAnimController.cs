using System.Collections;
using System.Collections.Generic;
using Dungeon.Managers;
using UnityEngine;
using TMPro;

public class FlotingAnimController : MonoBehaviour
{
    LevelManager levelManager;
    Animator floatingAnimator;

    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI darkText;



    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        floatingAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        levelManager.EarnAction += EarnListener;
    }
    private void OnDisable()
    {
        levelManager.EarnAction -= EarnListener;
    }


    private void EarnListener(LevelData ld, ulong gi, ulong di)
    {
        goldText.text = "+" + gi.ToString();
        darkText.text = "+" + di.ToString();

        floatingAnimator.SetTrigger("Earn");
    }

}
