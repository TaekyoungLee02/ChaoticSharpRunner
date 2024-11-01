using UnityEngine;

[CreateAssetMenu(menuName = "Character/UnityChan CustomzationData")]
public class CustomizationDataSO : ScriptableObject {
    public Color leftEyeColor;
    public Color rightEyeColor;

    public Material leftEyeMat;
    public Material rightEyeMat;
}
