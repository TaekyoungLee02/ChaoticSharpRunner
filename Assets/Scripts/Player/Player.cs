using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller { get; private set; }
    public PlayerHealth health { get; private set; }
    public PlayerAbility ability { get; private set; }

    void Awake()
    {
        controller = GetComponent<PlayerController>();
        health = GetComponent<PlayerHealth>();
        ability = GetComponent<PlayerAbility>();

        GameManager.Instance.player = this;
    }

    public void InitializePlayer()
    {
        controller.InitializeMovement();
        health.InitializeHealth();
        ability.InitializeAbility();
        ResetPosition();        
    }

    public void ResetPlayer()
    {
        controller.InitializeMovement();
        health.ResetHealth();
        ability.InitializeAbility();
        ResetPosition();
    }

    private void ResetPosition()
    {
        transform.position = Vector3.zero;
    }

    //public void ApplyItemEffect(Item item)
    //{
    //    item.ApplyEffect(this);  // 아이템이 자신을 플레이어에 적용하도록 전달
    //}

    void OnEnable()
    {
        GameManager.Instance.OnGameReset += ResetPlayer;
        GameManager.Instance.OnGameOver += InitializePlayer;
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameReset -= ResetPlayer;
        GameManager.Instance.OnGameOver -= InitializePlayer;
    }
}