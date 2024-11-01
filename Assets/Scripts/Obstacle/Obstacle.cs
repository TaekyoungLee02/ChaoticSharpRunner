using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject scoreCollider;

    public void DestroyObstacle()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(true) // 슈퍼아머일 경우
            {
                gameObject.SetActive(false);
            }
            else
            {
                // 플레이어와 충돌
            }
        }
    }

}
