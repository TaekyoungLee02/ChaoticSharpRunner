using UnityEngine;

public class ChangeCoinItem : ItemBase
{
    public override void Use(Player player)
    {
        // ��ֹ��� �������� �ٲ�

        // ���� ��ġ�κ��� Collider �� ���� ���� �̳��� �ִ� ��ֹ��� ��Ȱ��ȭ (Ǯ�� �ٽ� ����������) �ϰ�,
        // ������ Ǯ���� ���� �������� �޾ƿͼ� ����

        var obstacles = Physics.OverlapSphere(transform.position, itemValue, LayerMask.NameToLayer("Obstacle"));

        foreach (var obstacle in obstacles)
        {
            var obstaclePosition = obstacle.transform.position;
            obstacle.GetComponent<Obstacle>().DestroyObstacle();

            var coin = ObjectPool.Instance.SpawnFromPool("CoinItem");
            coin.transform.position = obstaclePosition;
        }
    }
}
