Shader "Cutsom/FogOfWar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct MeshData {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 worldPos : TEXCOORD1;
            };
            
            #define MAX_GLOWING_OBJECT_POSITIONS 1024
            #define MAX_DISTANCE_TO_GLOWING_OBJECT 1000

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float     _FogIntensity;
            float     _VisibilitySmoothness;
            
            float3 _GlowingObjects[MAX_GLOWING_OBJECT_POSITIONS];
            int    _GlowingObjectCount;

            Interpolators vert(MeshData meshData)
            {
                Interpolators interpolators;
                interpolators.vertex = UnityObjectToClipPos(meshData.vertex);
                interpolators.uv = TRANSFORM_TEX(meshData.uv, _MainTex);
                interpolators.worldPos = mul(UNITY_MATRIX_M, meshData.vertex);
                return interpolators;
            }

            fixed4 frag(Interpolators interpolators) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, interpolators.uv);

                float distanceToNearestObject = MAX_DISTANCE_TO_GLOWING_OBJECT;
                float objectVisibilityRadius = 0;
                
                for(int i = 0; i < _GlowingObjectCount; i++)
                {
                    float2 objectPos = _GlowingObjects[i].xy;
                    objectVisibilityRadius = _GlowingObjects[i].z;
                    
                    float  distanceToObject = distance(interpolators.worldPos, objectPos);

                    if (distanceToObject < distanceToNearestObject)
                        distanceToNearestObject = distanceToObject;
                }
                
                float fogMask = smoothstep(0, 1, (distanceToNearestObject - objectVisibilityRadius) / _VisibilitySmoothness);
                
                float4 fogIntensivity = float4((1 - _FogIntensity).xxx, fogMask);
                
                return color * fogIntensivity;
            }
            ENDCG
        }
    }
}