Shader "Custom/SpriteFillCircle_OneWay"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _StartAngle ("Start Angle", Range(0,360)) = 0 // 添加起始角度属性
        _FillAmount ("Fill Amount", Range(0,360)) = 90 // 填充角度
        _NearClip ("Near Clip", Range(0,1)) = 0.1
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }
 
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
 
        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha
 
        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment MySpriteFrag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"
            
            float _FillAmount;
            float _StartAngle;
            float _NearClip;

            // 计算两点间的角度，范围为 0 到 360 度
            fixed GetAngle(fixed2 from, fixed2 to)
            {
                float denominator = sqrt((from.x * from.x + from.y * from.y) * (to.x * to.x + to.y * to.y));
                if (denominator < 0.000001)
                    return 0;

                float dotNum = clamp(dot(from, to) / denominator, -1.0, 1.0);
                float angle = degrees(acos(dotNum));
                
                // 使用叉积确定方向，确保角度范围为 0 到 360 度
                float cross = from.x * to.y - from.y * to.x;
                if (cross < 0)
                    angle = 360 - angle;

                return angle;
            }

            // 核心逻辑的片段着色器
            fixed4 MySpriteFrag(v2f IN) : SV_Target
            {
                fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
                fixed2 uvCenter = fixed2(0.5, 0.5); // 圆心坐标
                fixed2 direction = fixed2(0, 1); // 计算的基准方向 (竖直向上)
                
                // 获取当前像素的角度
                fixed absAngle = GetAngle(direction, IN.texcoord - uvCenter);

                // 将角度范围控制在 0 到 360 度之间
                float adjustedAngle = absAngle - _StartAngle;
                if (adjustedAngle < 0)
                    adjustedAngle += 360;

                // 填充判定，超过 FillAmount 的部分变透明
                c.a *= (adjustedAngle <= _FillAmount) && (distance(IN.texcoord, uvCenter) * 2 > _NearClip);
                c.rgb *= c.a;
                return c;
            }
        ENDCG
        }
    }
}