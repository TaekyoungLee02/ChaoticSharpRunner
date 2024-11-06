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

    public override void Use(Player player)
    {
        base.Use(player);

        ScoreManager.Instance.AddScore((int)itemValue);
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
