using UnityEngine;

public class ChangeCoinItem : ItemBase
{
    public override void Use(Player player)
    {
        // 장애물이 코인으로 바뀜

        // 먹은 위치로부터 Collider 로 일정 범위 이내에 있는 장애물을 비활성화 (풀로 다시 돌려보내지) 하고,
        // 아이템 풀에서 코인 아이템을 받아와서 생성

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
