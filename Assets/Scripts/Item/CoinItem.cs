using UnityEngine;

public class CoinItem : ItemBase
{
    private Transform playerTransform;
    private bool magnet;
    private readonly float speed = 1f;

    private void Start()
    {
        playerTransform = GameManager.Instance.player.transform;
    }

    public override void Use(Player player)
    {
        ScoreManager.Instance.AddScore((int)itemValue);
    }

    public void EnableMagnet()
    {
        magnet = true;
    }

    private void FixedUpdate()
    {
        if(magnet)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }
}
