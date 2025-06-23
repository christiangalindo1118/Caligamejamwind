using UnityEngine;
using System.Collections;

public class ScreenFeedback : MonoBehaviour
{
    [Header("Screen")]
    public Material screenMaterial;
    public Color screenOnColor = Color.white;
    public Color screenOffColor = Color.black;

    public void UpdateScreen(bool isOn, float batteryLife)
    {
        if (screenMaterial == null) return;

        Color targetColor = isOn ? screenOnColor : screenOffColor;

        if (isOn && batteryLife > 0)
        {
            float batteryPercent = batteryLife / 100f;
            targetColor = Color.Lerp(Color.yellow, screenOnColor, batteryPercent);
        }

        screenMaterial.color = targetColor;

        if (screenMaterial.HasProperty("_EmissionColor"))
            screenMaterial.SetColor("_EmissionColor", targetColor * (isOn ? 1f : 0f));
    }

    public IEnumerator FlashRed()
    {
        for (int i = 0; i < 3; i++)
        {
            screenMaterial.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            yield return null;
        }
    }
}