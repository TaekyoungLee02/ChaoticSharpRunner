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
            if(true) // ���۾Ƹ��� ���
            {
                gameObject.SetActive(false);
            }
            else
            {
                // �÷��̾�� �浹
            }
        }
    }

}
