using UnityEngine;
using System;

public class ObstacleCollisionProcessor : MonoBehaviour
{
    [SerializeField] private int avoidScore = 10;
    [SerializeField] private int obstacleDamage = 10;

    private PlayerStats playerStats;
    private Transform playerTransform;

    private bool hasPassedPlayer = false; // 장애물이 플레이어를 지나갔는지 확인
    private bool hasCollidedWithPlayer = false; // 충돌 여부 확인

    public event Action OnAvoidObstacleEvent;
    public event Action OnHitObstacleEvent;

    void Start()
    {
        playerStats = GameManager.Instance.player.stats;
        playerTransform = GameManager.Instance.player.transform;
    }

    void OnEnable()
    {
        hasPassedPlayer = false;
        hasCollidedWithPlayer = false;
    }

    void Update()
    {
        // 장애물이 아직 지나가지 않았고, 충돌이 발생하지 않은 경우
        if (!hasPassedPlayer && !hasCollidedWithPlayer && transform.position.z >= playerTransform.position.z)
        {
            // x 값이 일치하고 충돌이 없는 상태일 때 피하기 성공으로 간주
            if (Mathf.Approximately(transform.position.x, playerTransform.position.x))
            {
                hasPassedPlayer = true;
                OnAvoidObstacle(); // 피하기 성공 시 점수 부여
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollidedWithPlayer = true; // 충돌 발생 기록
            hasPassedPlayer = true;       // 충돌로 인해 더 이상 피하기 점수 부여 방지
            OnHitObstacle();               // 충돌 시 데미지 처리
        }
    }

    private void OnAvoidObstacle()
    {
        OnAvoidObstacleEvent?.Invoke();
        ScoreManager.Instance.AddScore(avoidScore);
    }

    private void OnHitObstacle()
    {
        playerStats.TakeDamage(obstacleDamage);
        OnHitObstacleEvent?.Invoke();
    }

    public void TriggerHitObstacle()
    {
        OnHitObstacle();
    }
}