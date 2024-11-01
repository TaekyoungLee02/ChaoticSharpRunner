using UnityEngine;
using System;

public class ObstacleCollisionProcessor : MonoBehaviour
{
    [SerializeField] private int avoidScore = 10;
    [SerializeField] private int obstacleDamage = 10;

    private PlayerStats playerStats;
    private Transform playerTransform;

    private bool hasPassedPlayer = false; // ��ֹ��� �÷��̾ ���������� Ȯ��
    private bool hasCollidedWithPlayer = false; // �浹 ���� Ȯ��

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
        // ��ֹ��� ���� �������� �ʾҰ�, �浹�� �߻����� ���� ���
        if (!hasPassedPlayer && !hasCollidedWithPlayer && transform.position.z >= playerTransform.position.z)
        {
            // x ���� ��ġ�ϰ� �浹�� ���� ������ �� ���ϱ� �������� ����
            if (Mathf.Approximately(transform.position.x, playerTransform.position.x))
            {
                hasPassedPlayer = true;
                OnAvoidObstacle(); // ���ϱ� ���� �� ���� �ο�
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollidedWithPlayer = true; // �浹 �߻� ���
            hasPassedPlayer = true;       // �浹�� ���� �� �̻� ���ϱ� ���� �ο� ����
            OnHitObstacle();               // �浹 �� ������ ó��
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