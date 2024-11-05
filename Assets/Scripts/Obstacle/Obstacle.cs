using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject scoreCollider;
    public int damage;

    private void OnEnable()
    {
        scoreCollider.SetActive(true);
    }

    public void DestroyObstacle()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();

            if(player.stats.isInvincible) return;

            if(player.stats.isSuperArmor) // 슈퍼아머일 경우
            {
                DestroyObstacle();
            }
            else
            {
                player.stats.TakeDamage(damage);

                scoreCollider.SetActive(false);
            }
        }
    }

}
