using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartBlink : MonoBehaviour
{
    public float blinkSpeed = 4.0f;
    public Image mainImage;
    public Image borderImage;
    public TextMeshProUGUI text;

    private bool isFadingOut = true;

    void Update()
    {
        float alpha = Mathf.Lerp(mainImage.color.a, isFadingOut ? 0 : 1, blinkSpeed * Time.deltaTime);

        Color mainImageColor = mainImage.color;
        Color borderImageColor = borderImage.color;
        Color textColor = text.color;

        mainImageColor.a = alpha;
        borderImageColor.a = alpha;
        textColor.a = alpha;

        mainImage.color = mainImageColor;
        borderImage.color = borderImageColor;
        text.color = textColor;

        if (alpha <= 0.1f) isFadingOut = false;
        if (alpha >= 0.9f) isFadingOut = true;
    }
}