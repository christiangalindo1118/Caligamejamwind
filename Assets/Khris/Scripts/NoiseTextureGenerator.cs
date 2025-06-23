using UnityEngine;

[System.Serializable]
public class NoiseTextureGenerator : MonoBehaviour
{
    [Header("Configuración de Textura")]
    public int textureSize = 512;
    public float noiseScale = 50f;
    public float contrast = 1.5f;
    public float brightness = 0.5f;
    
    [Header("Output")]
    public string fileName = "SketchTexture";
    
    [Space]
    [SerializeField] private Texture2D generatedTexture;
    
    void Start()
    {
        GenerateNoiseTexture();
    }
    
    [ContextMenu("Generate Noise Texture")]
    public void GenerateNoiseTexture()
    {
        // Crear nueva textura
        generatedTexture = new Texture2D(textureSize, textureSize, TextureFormat.RGB24, false);
        
        // Generar ruido
        for (int x = 0; x < textureSize; x++)
        {
            for (int y = 0; y < textureSize; y++)
            {
                // Generar ruido Perlin
                float noiseValue = Mathf.PerlinNoise(
                    (float)x / textureSize * noiseScale, 
                    (float)y / textureSize * noiseScale
                );
                
                // Aplicar contraste y brillo
                noiseValue = (noiseValue - 0.5f) * contrast + brightness;
                noiseValue = Mathf.Clamp01(noiseValue);
                
                // Asignar color (escala de grises)
                Color pixelColor = new Color(noiseValue, noiseValue, noiseValue, 1f);
                generatedTexture.SetPixel(x, y, pixelColor);
            }
        }
        
        // Aplicar cambios
        generatedTexture.Apply();
        
        // Mostrar en el objeto si tiene Renderer
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = generatedTexture;
        }
        
        Debug.Log("Textura de ruido generada: " + textureSize + "x" + textureSize);
    }
    
    [ContextMenu("Save Texture as PNG")]
    public void SaveTextureAsPNG()
    {
        if (generatedTexture == null)
        {
            GenerateNoiseTexture();
        }
        
        // Convertir a PNG
        byte[] pngData = generatedTexture.EncodeToPNG();
        
        // Guardar archivo
        string path = Application.dataPath + "/" + fileName + ".png";
        System.IO.File.WriteAllBytes(path, pngData);
        
        Debug.Log("Textura guardada en: " + path);
        
        // Refrescar el proyecto en Unity
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }
    
    // Función para crear diferentes tipos de ruido
    [ContextMenu("Generate Paper Texture")]
    public void GeneratePaperTexture()
    {
        generatedTexture = new Texture2D(textureSize, textureSize, TextureFormat.RGB24, false);
        
        for (int x = 0; x < textureSize; x++)
        {
            for (int y = 0; y < textureSize; y++)
            {
                // Combinar diferentes escalas de ruido para efecto papel
                float noise1 = Mathf.PerlinNoise(x * 0.1f, y * 0.1f) * 0.5f;
                float noise2 = Mathf.PerlinNoise(x * 0.05f, y * 0.05f) * 0.3f;
                float noise3 = Mathf.PerlinNoise(x * 0.02f, y * 0.02f) * 0.2f;
                
                float finalNoise = (noise1 + noise2 + noise3) + 0.7f;
                finalNoise = Mathf.Clamp01(finalNoise);
                
                Color pixelColor = new Color(finalNoise, finalNoise, finalNoise, 1f);
                generatedTexture.SetPixel(x, y, pixelColor);
            }
        }
        
        generatedTexture.Apply();
        
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = generatedTexture;
        }
        
        Debug.Log("Textura de papel generada");
    }
}
