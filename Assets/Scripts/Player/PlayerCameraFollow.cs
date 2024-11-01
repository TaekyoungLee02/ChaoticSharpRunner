using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float followSpeed = 5f;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            Vector3 targetPosition = transform.position + offset;
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, followSpeed * Time.deltaTime);

            cameraTransform.LookAt(transform);
        }
    }
}