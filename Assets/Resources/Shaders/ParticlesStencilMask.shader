// Simplified Additive Particle shader. Differences from regular Additive Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

Shader "Mobile/Particles/Stencil Mask" {
Properties {
    _MainTex ("Particle Texture", 2D) = "white" {}
}

Category {
    Tags { "Queue"="Transparent" "IgnoreProjector"="True"}
    ColorMask 0
    ZTest Always
    ZWrite Off Fog { Color (0,0,0,0) }

    
    SubShader {
        Stencil {
            Ref 101
            Comp always
            Pass replace
        }
        Pass {
            SetTexture [_MainTex] {
                combine texture * primary
            }
        }
    }
}
}