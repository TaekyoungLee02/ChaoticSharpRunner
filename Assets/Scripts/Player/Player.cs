using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller { get; private set; }
    public PlayerStats stats { get; private set; }
    public PlayerAbility ability { get; private set; }

    void Awake()
    {
        controller = GetComponent<PlayerController>();
        stats = GetComponent<PlayerStats>();
        ability = GetComponent<PlayerAbility>();

        GameManager.Instance.player = this;
    }

    public void InitializePlayer()
    {
        controller.InitializeMovement();
        stats.InitializeHealth();
        ability.InitializeAbility();
        ResetPosition();        
    }

    public void ResetPlayer()
    {
        controller.InitializeMovement();
        stats.ResetHealth();
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