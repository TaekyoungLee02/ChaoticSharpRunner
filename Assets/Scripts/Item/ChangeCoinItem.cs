using UnityEngine;

public class ChangeCoinItem : ItemBase
{
    public override void Use(Player player)
    {
        // ��ֹ��� �������� �ٲ�

        // ���� ��ġ�κ��� Collider �� ���� ���� �̳��� �ִ� ��ֹ��� ��Ȱ��ȭ (Ǯ�� �ٽ� ����������) �ϰ�,
        // ������ Ǯ���� ���� �������� �޾ƿͼ� ����

        base.Use(player);

        AudioManager.Instance.PlaySoundFXClip(AudioClipName.Sfx_08, transform.position, 0.5f);
        AudioManager.Instance.PlaySoundFXClip(AudioClipName.UnityChan_Soret, transform.position, 0.5f);

        var obstacles = Physics.OverlapSphere(transform.position, itemValue, 1 << LayerMask.NameToLayer("Obstacle"));

        foreach (var obstacle in obstacles)
        {
            Debug.Log(obstacle.name);

            var obstacleTransform = obstacle.transform;
            var coin = ObjectPool.Instance.SpawnFromPool("Coin");

            coin.transform.position = new(obstacleTransform.position.x, obstacleTransform.position.y + 1.5f, obstacleTransform.position.z);
            coin.transform.SetParent(obstacleTransform.parent);

            obstacle.GetComponent<Obstacle>().DestroyObstacle();

        }
    }
}
