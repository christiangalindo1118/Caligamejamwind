using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonFadeOut : MonoBehaviour, IPointerClickHandler
{
    public float fadeDuration = 1.0f;

    private Graphic[] graphics; // Puede incluir Image, Text, TMP_Text...

    void Awake()
    {
        // Captura todos los elementos gráficos del botón
        graphics = GetComponentsInChildren<Graphic>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Ejecuta el fade sin bloquear la acción del botón
        StartCoroutine(FadeOutGraphics());
    }

    IEnumerator FadeOutGraphics()
    {
        float elapsed = 0f;

        // Guarda los colores originales
        Color[] originalColors = new Color[graphics.Length];
        for (int i = 0; i < graphics.Length; i++)
            originalColors[i] = graphics[i].color;

        // Interpolación
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].color = new Color(
                    originalColors[i].r,
                    originalColors[i].g,
                    originalColors[i].b,
                    alpha
                );
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Asegura que queden invisibles
        foreach (var g in graphics)
            g.color = new Color(g.color.r, g.color.g, g.color.b, 0f);
    }
}