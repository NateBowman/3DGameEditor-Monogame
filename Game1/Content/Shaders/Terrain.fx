float4x4 World;
float4x4 View;
float4x4 Projection;

//ambient lighting
float4 AmbientColor = float4(1, 1, 1, 1);
float AmbientIntensity = 0.1;

//diffuse lighting
float3 DiffuseLightDirection = float3(0, 1, 0);
float4 DiffuseColor = float4(1, 1, 1, 1);
float DiffuseIntensity = 1.0;

float3 ViewVector = float3(1, 0, 0);

//texture vars
texture ModelTexture;
sampler2D textureSampler = sampler_state {
	Texture = (ModelTexture);
	MinFilter = Linear;
	MagFilter = Linear;
	AddressU = Clamp;
	AddressV = Clamp;
};

struct VertexShaderInput
{
	float4 Position : SV_POSITION;
    float4 Color : COLOR0;
	float2 TextureCoordinate : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinate : TEXCOORD1;
};

float OverlayBlend(float baseVal, float blendVal)
{
    return baseVal < 0.5 ? (2.0 * baseVal * blendVal) : (1.0 - 2.0 * (1.0 - blendVal) * (1.0 - blendVal));
}

float3 OverlayBlend(float3 base, float3 blend)
{
    return float3(OverlayBlend(base.r, blend.r), OverlayBlend(base.g, blend.g), OverlayBlend(base.b, blend.b));
}

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);

    float3 colour = OverlayBlend(saturate(DiffuseColor * DiffuseIntensity).rgb, input.Color.rgb);

    output.Color = float4(colour.r, colour.g, colour.b, 1.0);

	output.TextureCoordinate = input.TextureCoordinate;

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : SV_TARGET0
{
	float3 light = normalize(DiffuseLightDirection);

	float3 v = normalize(mul(normalize(ViewVector), World));

	float4 textureColor = tex2D(textureSampler, input.TextureCoordinate);
	textureColor.a = 1;

	return saturate(textureColor * (input.Color) + AmbientColor * AmbientIntensity);
}

technique Terrain
{
	pass Pass1
	{
		VertexShader = compile vs_4_0 VertexShaderFunction();
		PixelShader = compile ps_4_0 PixelShaderFunction();
	}
}
