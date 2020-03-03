using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI CurrentLevelText, NextLevelText;
    public Image[] LevelBridges;

    public GameObject RestartPanel;
    public GameObject StartPanel;
    public GameObject LevelPanel;
    public GameObject LevelCompletedTextGO;

    public void LevelComplete(){
        LevelPanel.SetActive(false);
        LevelCompletedTextGO.SetActive(true);
    }

    public void HideStartPanel(){
        StartPanel.SetActive(false);
    }

    public void ShowRestartPanel(){
        RestartPanel.SetActive(true);
    }

    public void SetLevelTexts(int p_level){
        CurrentLevelText.text = p_level.ToString();
        NextLevelText.text = (p_level + 1).ToString();
    }

    public void SetBridgeFill(int p_bridgeIndex, float p_percentage){
        LevelBridges[p_bridgeIndex].fillAmount = p_percentage;
    }

    public void ResetBridges(){
        SetBridgeFill(0, 0f);
        SetBridgeFill(1, 0f);
    }
}
