using System.Collections;
using UnityEngine;

public class MagnetItem : ItemBase
{
    public override void Use(Player player)
    {
        // 자석 코드 작성

        // 플레이어 반경으로 감지해서 코인이 있으면 그 코인이 플레이어 쪽으로 다가가게 함.

        base.Use(player);
        player.GetComponent<PlayerCoroutineReciever>().StopCoroutine();
        player.GetComponent<PlayerCoroutineReciever>().StartCoroutine(MagnetCoroutine(player));
    }

    private IEnumerator MagnetCoroutine(Player player)
    {
        float magnetDuration = 0;

        while (magnetDuration < duration)
        {
            magnetDuration += Time.deltaTime;
            Debug.Log(magnetDuration);

            var coins = Physics.OverlapSphere(player.transform.position, itemValue, 1 << LayerMask.NameToLayer("Coin"));

            if (coins != null)
            {
                foreach (var coin in coins)
                {
                    Debug.Log(coin);

                    coin.GetComponent<CoinItem>().EnableMagnet();
                }
            }

            yield return null;
        }
    }
}
