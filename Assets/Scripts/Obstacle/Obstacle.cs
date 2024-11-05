using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject scoreCollider;
    public int damage;

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

            if(player.stats.isSuperArmor) // ���۾Ƹ��� ���
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
