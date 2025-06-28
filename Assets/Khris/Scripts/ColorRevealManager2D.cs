using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Script principal que maneja el efecto grayscale en objetos 2D
public class GrayscaleEffect2D : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Image uiImage;
    private Color originalColor;
    private bool isGrayscale = false;
    
    [Header("Configuración")]
    public bool startInGrayscale = true;
    public float transitionDuration = 1f;
    public float grayscaleIntensity = 0.8f;
    
    void Start()
    {
        // Obtener componentes
        spriteRenderer = GetComponent<SpriteRenderer>();
        uiImage = GetComponent<Image>();
        
        // Guardar color original
        originalColor = GetCurrentColor();
        
        if (startInGrayscale)
        {
            MakeGrayscale();
        }
    }
    
    public void MakeGrayscale()
    {
        if (!isGrayscale)
        {
            StartCoroutine(TransitionToGrayscale());
        }
    }
    
    public void RevealColor()
    {
        if (isGrayscale)
        {
            StartCoroutine(TransitionToColor());
        }
    }
    
    public void SetGrayscaleImmediate()
    {
        isGrayscale = true;
        Color grayColor = ConvertToGrayscale(originalColor);
        SetColor(grayColor);
    }
    
    public void SetColorImmediate()
    {
        isGrayscale = false;
        SetColor(originalColor);
    }
    
    IEnumerator TransitionToGrayscale()
    {
        isGrayscale = true;
        float elapsed = 0f;
        Color startColor = GetCurrentColor();
        Color targetColor = ConvertToGrayscale(originalColor);
        
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / transitionDuration;
            Color currentColor = Color.Lerp(startColor, targetColor, progress);
            SetColor(currentColor);
            yield return null;
        }
        
        SetColor(targetColor);
    }
    
    IEnumerator TransitionToColor()
    {
        isGrayscale = false;
        float elapsed = 0f;
        Color startColor = GetCurrentColor();
        Color targetColor = originalColor;
        
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / transitionDuration;
            Color currentColor = Color.Lerp(startColor, targetColor, progress);
            SetColor(currentColor);
            yield return null;
        }
        
        SetColor(targetColor);
    }
    
    Color ConvertToGrayscale(Color color)
    {
        // Fórmula estándar para convertir a escala de grises
        float gray = (color.r * 0.299f + color.g * 0.587f + color.b * 0.114f);
        
        // Mezclar el color original con el gris según la intensidad
        Color grayColor = new Color(gray, gray, gray, color.a);
        return Color.Lerp(color, grayColor, grayscaleIntensity);
    }
    
    Color GetCurrentColor()
    {
        if (spriteRenderer != null)
            return spriteRenderer.color;
        else if (uiImage != null)
            return uiImage.color;
        return Color.white;
    }
    
    void SetColor(Color color)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = color;
        else if (uiImage != null)
            uiImage.color = color;
    }
    
    // Método para verificar si el objeto está en escala de grises
    public bool IsGrayscale()
    {
        return isGrayscale;
    }
    
    // Método para resetear el color original
    public void ResetOriginalColor()
    {
        originalColor = GetCurrentColor();
    }
}

