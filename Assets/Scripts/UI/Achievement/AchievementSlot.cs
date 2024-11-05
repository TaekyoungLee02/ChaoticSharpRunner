using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTxt;
    [SerializeField] private TextMeshProUGUI descTxt;
    [SerializeField] private GameObject checkMark;

    public void Init(AchievementSO data)
    {
        titleTxt.text = data.achievementName;
        descTxt.text = data.achievementDescription;
        checkMark.SetActive(data.completed);
    }
}
