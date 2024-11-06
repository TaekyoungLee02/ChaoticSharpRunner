using System.Collections;
using UnityEngine;

public class MagnetItem : ItemBase
{
    public override void Use(Player player)
    {
        // �ڼ� �ڵ� �ۼ�

        // �÷��̾� �ݰ����� �����ؼ� ������ ������ �� ������ �÷��̾� ������ �ٰ����� ��.

        AudioManager.Instance.PlaySoundFXClip(AudioClipName.Sfx_09, transform.position, 0.5f);


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
