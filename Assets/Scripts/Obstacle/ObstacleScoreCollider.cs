using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScoreCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            ObstacleManager.Instance.ObstaclePassed();
            ScoreManager.Instance.AddScore(1);
        }
    }
}
