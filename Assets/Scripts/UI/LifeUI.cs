using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private GameObject[] lifeIcons;

    private void Start()
    {
        var playerStats = GameManager.Instance.player.stats;
        playerStats.OnLifeChanged += UpdateLifeIcons;
    }

    private void UpdateLifeIcons(int currentLives)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].SetActive(i < currentLives);
        }
    }
}