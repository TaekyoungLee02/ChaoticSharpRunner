using UnityEngine;

public class CoinItem : ItemBase
{

    private bool magnet;

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
            //�÷��̾� ������ �ٰ�����
        }
    }
}
