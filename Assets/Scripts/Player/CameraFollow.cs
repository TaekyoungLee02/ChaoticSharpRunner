using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0, 5, -10);

    public float followSpeed = 5f;

    private void Start()
    {
        playerTransform = GameManager.Instance.player.transform;
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 targetPosition = playerTransform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            transform.LookAt(playerTransform);
        }
    }
}