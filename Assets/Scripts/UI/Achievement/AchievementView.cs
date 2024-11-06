using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementView : MonoBehaviour
{
    [SerializeField] private GameObject achievementSlotPrefab;  // ¾÷Àû ½½·Ô ÇÁ¸®ÆÕ
    [SerializeField] private RectTransform viewContent;
    [SerializeField] private float prefabHeight;

    private void Start()
    {
        CreateAchievementSlots(AchievementManager.Instance.Achievements);
    }

    public void CreateAchievementSlots(List<AchievementSO> achievements)
    {
        for (int i = 0; i < achievements.Count; i++)
        {
            var achieveSlot = Instantiate(achievementSlotPrefab, transform).GetComponent<AchievementSlot>();
            achieveSlot.Init(achievements[i]);
            achieveSlot.transform.SetParent(viewContent.transform);
        }

        viewContent.sizeDelta = new Vector2(0, (prefabHeight + 20) * achievements.Count);
    }

    public void BackButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
