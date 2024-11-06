using UnityEngine;
using System.Collections;

public class PlayerBlink : MonoBehaviour
{
    private Renderer playerRenderer;
    public float blinkInterval = 0.1f;
    private Color originalColor;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;
    }

    public void StartBlinking(float duration)
    {
        StartCoroutine(Blink(duration));
    }

    private IEnumerator Blink(float duration)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            Color color = playerRenderer.material.color;
            color.a = color.a == 1f ? 0.3f : 1f;
            playerRenderer.material.color = color;
            yield return new WaitForSeconds(blinkInterval);
        }
        playerRenderer.material.color = originalColor;
    }
}
