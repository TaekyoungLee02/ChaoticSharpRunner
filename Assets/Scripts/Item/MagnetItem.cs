using System.Collections;
using UnityEngine;

public class MagnetItem : ItemBase
{
    public override void Use(Player player)
    {
        // �ڼ� �ڵ� �ۼ�

        // �÷��̾� �ݰ����� �����ؼ� ������ ������ �� ������ �÷��̾� ������ �ٰ����� ��.

        base.Use(player);

        player.GetComponent<PlayerCoroutineReciever>().StartCoroutine(MagnetCoroutine());
    }

    private IEnumerator MagnetCoroutine()
    {
        float magnetDuration = 0;

        while (magnetDuration < duration)
        {
            magnetDuration += Time.deltaTime;

            var coins = Physics.OverlapSphere(transform.position, itemValue, 1 << LayerMask.NameToLayer("Coin"));

            if (coins != null)
            {
                foreach (var coin in coins)
                {
                    coin.GetComponent<CoinItem>().EnableMagnet();
                }
            }

            yield return null;
        }
    }
}
