Shader "Custom/SketchShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SketchTex ("Sketch Texture", 2D) = "white" {}
        _SketchIntensity ("Sketch Intensity", Range(0, 1)) = 0.5
        _ContrastPower ("Contrast", Range(0.5, 3)) = 1.2
        _EdgeThreshold ("Edge Threshold", Range(0, 1)) = 0.1
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            sampler2D _SketchTex;
            float4 _MainTex_ST;
            float _SketchIntensity;
            float _ContrastPower;
            float _EdgeThreshold;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // Obtener color base
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // Convertir a escala de grises
                float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));
                
                // Aplicar contraste
                gray = pow(gray, _ContrastPower);
                
                // Obtener textura de sketch
                float2 sketchUV = i.uv * 4;
                fixed4 sketch = tex2D(_SketchTex, sketchUV);
                float sketchValue = sketch.r;
                
                // Combinar sketch con imagen base
                float finalGray = lerp(gray, gray * sketchValue, _SketchIntensity);
                
                // Crear efecto de bordes
                float edge = 1.0 - smoothstep(_EdgeThreshold - 0.05, _EdgeThreshold + 0.05, finalGray);
                finalGray = lerp(finalGray, 0, edge * 0.3);
                
                return fixed4(finalGray, finalGray, finalGray, col.a);
            }
            ENDCG
        }
    }
}