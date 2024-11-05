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
        stats.InitializeStats();
        ability.InitializeAbility();
        ResetPosition();        
    }

    public void ResetPlayer()
    {
        controller.InitializeMovement();
        stats.ResetStats();
        ability.InitializeAbility();
        ResetPosition();
    }

    private void ResetPosition()
    {
        transform.position = Vector3.zero;
    }
}