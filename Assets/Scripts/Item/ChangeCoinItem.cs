using UnityEngine;

public class ChangeCoinItem : ItemBase
{
    public override void Use(Player player)
    {
        // ��ֹ��� �������� �ٲ�

        // ���� ��ġ�κ��� Collider �� ���� ���� �̳��� �ִ� ��ֹ��� ��Ȱ��ȭ (Ǯ�� �ٽ� ����������) �ϰ�,
        // ������ Ǯ���� ���� �������� �޾ƿͼ� ����

        base.Use(player);

        var obstacles = Physics.OverlapSphere(transform.position, itemValue, 1 << LayerMask.NameToLayer("Obstacle"));

        foreach (var obstacle in obstacles)
        {
            Debug.Log(obstacle.name);

            var obstaclePosition = obstacle.transform.position;
            obstacle.GetComponent<Obstacle>().DestroyObstacle();

            var coin = ObjectPool.Instance.SpawnFromPool("CoinItem");
            coin.transform.position = obstaclePosition;
        }
    }
}
