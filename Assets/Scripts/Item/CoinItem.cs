using UnityEngine;

public class CoinItem : ItemBase
{
    private Transform playerTransform;
    private bool magnet;
    private readonly float speed = 3f;

    private void Start()
    {
        playerTransform = GameManager.Instance.player.transform;
    }

    private void OnEnable()
    {
        magnet = false;
    }

    public override void Use(Player player)
    {
        base.Use(player);

        ScoreManager.Instance.AddScore((int)itemValue);
        magnet = false;
    }

    public void EnableMagnet()
    {
        magnet = true;
        transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        if(magnet)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }
}
