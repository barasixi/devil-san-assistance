﻿// Simplified Additive Particle shader. Differences from regular Additive Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

Shader "Mobile/Particles/Additive Stencil" {
Properties {
    _MainTex ("Particle Texture", 2D) = "white" {}
}

Category {
    Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
    Blend SrcAlpha One
    Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
    
    BindChannels {
        Bind "Color", color
        Bind "Vertex", vertex
        Bind "TexCoord", texcoord
    }
    
    SubShader {
        Stencil {
            Ref 101
            Comp Equal
        }
        Pass {
            SetTexture [_MainTex] {
                combine texture * primary
            }
        }
    }
}
}