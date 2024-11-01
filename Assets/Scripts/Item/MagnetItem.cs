using System.Collections;
using UnityEngine;

public class MagnetItem : ItemBase
{
    public override void Use(Player player)
    {
        // �ڼ� �ڵ� �ۼ�

        // �÷��̾� �ݰ����� �����ؼ� ������ ������ �� ������ �÷��̾� ������ �ٰ����� ��.



    }

    private IEnumerator MagnetCoroutine()
    {
        float magnetDuration = 0;

        while (magnetDuration < duration)
        {
            magnetDuration += Time.deltaTime;

            var coins = Physics.OverlapSphere(transform.position, itemValue, LayerMask.NameToLayer("Coin"));

            foreach (var coin in coins)
            {
                coin.GetComponent<CoinItem>().EnableMagnet();
            }

            yield return null;
        }
    }
}
