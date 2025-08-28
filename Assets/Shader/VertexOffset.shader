Shader "Unlit/VertexOffset"
{
    Properties
    {
        _ColorA ("ColorA", Color) = (1,1,1,1)
        _ColorB ("ColorB", Color) = (1,1,1,1)
        //_Scale ("UVScale", Float) = 1
        _ColorStart ("Color Start", Range(0,1)) = 0
        _ColorEnd ("Color End", Range(0,1)) = 1
        _WaveAmp ("Wave amplitude", Range(0, 0.5)) = 0.1
        //_Offset ("Offset", Float) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" //tag to inform the render pipeline of what type this is
            "Queue"="Geometry" // changes the render order
        }
        LOD 100

        Pass
        {
            // pass tags
            //Cull Off
            //ZWrite Off // not writing to the depth buffer
            //ZTest LEqual
            //BLEND One One // Additive
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work

            #include "UnityCG.cginc"
            #define TAU 6.28318

            float4 _ColorA;
            float4 _ColorB;
            float _ColorStart;
            float _ColorEnd;
            float _WaveAmp;
            //float _Scale;
            //float _Offset;

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normal : NORMAL;
            };

            // data passed from the vertex shader to the fragment shader
            struct Interpolators
            {
                float3 normal : TEXCOORD0; // TEXCOORDO could be any data you need, it is a data stream
                float4 vertex : SV_POSITION; // clip space position - this one should always be available
                float2 uv : TEXCOORD1;
            };

            float getWave(float2 uv)
            {
                float2 uvcentered = uv * 2 - 1;
                float2 radialwave = length(uvcentered);
                float t = cos((radialwave - _Time.y * 0.1) * TAU * 5) * 0.5 + 0.5 ;
                t *= 1 - radialwave;
                return t;
                
            }

            Interpolators vert(MeshData v)
            {
                Interpolators o;
                float wave = cos((v.uv0.y - _Time.y * 0.1) * TAU * 5);

                v.vertex.y = getWave(v.uv0) * _WaveAmp;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                //o.vertex = v.vertex;
                o.uv = v.uv0;
                return o;
            }

            float InverseLerp(float a, float b, float v)
            {
                return (v - a) / (b - a);
            }


            fixed4 frag(Interpolators i) : SV_Target
            {
                //float t = saturate(InverseLerp(_ColorStart, _ColorEnd, i.uv.x)); //Same as Clamp01 method in unity
                //float offset = cos(i.uv.x * TAU * 20) * 0.01;
                float offset = 0;
                //float t = cos((i.uv.y + offset - _Time.y * 0.1) * TAU * 5) * 0.5 + 0.5 ;
                //t *= 1 - i.uv.y;
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);

                
                return getWave(i.uv) ;
                //float caps_remover = (abs(i.normal.y) < 0.9);
                //float waves = t * caps_remover;
                //float4 gradient = lerp(_ColorA, _ColorB, i.uv.y);
                //return gradient * waves;
            }
            ENDCG
        }
    }
}