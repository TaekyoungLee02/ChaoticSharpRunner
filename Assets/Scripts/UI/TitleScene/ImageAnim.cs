using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnim : MonoBehaviour
{
    private Image image;
    private float time;
    [SerializeField] private float blinkSpeed = 1f;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        Color currentColor = image.color;

        if (time < 0.5f)
        {
            currentColor.a = 1f - time * 2;
        }
        else
        {
            currentColor.a = (time - 0.5f) * 2;
            if (time > 1f)
            {
                time = 0;
            }
        }

        image.color = currentColor;

        time += Time.deltaTime / blinkSpeed;
    }
}