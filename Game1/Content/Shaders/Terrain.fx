float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 WorldInverseTranspose;

//ambient lighting
float4 AmbientColor = float4(1, 1, 1, 1);
float AmbientIntensity = 0.1;

//diffuse lighting
float3 DiffuseLightDirection = float3(0, 1, 0);
float4 DiffuseColor = float4(1, 1, 1, 1);
float DiffuseIntensity = 1.0;

//specular lighting
float Shininess = 200;
float4 SpecularColor = float4(1, 1, 1, 1);
float SpecularIntensity = 1;
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
	//float4 Normal : NORMAL0;
    float4 Color : COLOR0;
	float2 TextureCoordinate : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	//float3 Normal : TEXCOORD0;
	float2 TextureCoordinate : TEXCOORD1;
	//float3 WPos :TEXCOORD2;					//WORLD POSITION 
};

float blendOverlay(float base, float blend)
{
    return base < 0.5 ? (2.0 * base * blend) : (1.0 - 2.0 * (1.0 - base) * (1.0 - blend));
}

float3 blendOverlay(float3 base, float3 blend)
{
    return float3(blendOverlay(base.r, blend.r), blendOverlay(base.g, blend.g), blendOverlay(base.b, blend.b));
}

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);

	//float4 normal = mul(input.Normal, WorldInverseTranspose);
	//float lightIntensity = dot(normal.rgb, DiffuseLightDirection);
	//uncomment to enforce a minimal ambient lighting value
	//float lightIntensity = max(0.0f, dot(normal, DiffuseLightDirection));	

    float3 colour = blendOverlay(saturate(DiffuseColor * DiffuseIntensity).rgb, input.Color.rgb);

    output.Color = float4(colour.r, colour.g, colour.b, 1.0);

	//output.Normal = normalize(normal.rgb);

	output.TextureCoordinate = input.TextureCoordinate;
	//output.WPos = worldPosition;
	return output;
}

//float4 PixelShaderFunction(VertexShaderOutput input, in float4 screenSpace : SV_Position) : SV_TARGET0
float4 PixelShaderFunction(VertexShaderOutput input) : SV_TARGET0
{
	float3 light = normalize(DiffuseLightDirection);
	//float3 normal = normalize(input.Normal);
	//float3 r = normalize(2 * dot(light, normal) * normal - light);
	float3 v = normalize(mul(normalize(ViewVector), World));
	//float dotProduct = dot(r, v);

	//float4 specular = SpecularIntensity * SpecularColor * max(pow(abs(dotProduct), Shininess), 0) * length(input.Color);

	float4 textureColor = tex2D(textureSampler, input.TextureCoordinate);
	textureColor.a = 1;

	//if(input.WPos.y > 0)
	//	textureColor.r=1;

	//if (screenSpace.x > 250 )
	//	textureColor.g = 1;

	return saturate(textureColor * (input.Color) + AmbientColor * AmbientIntensity /*+ specular*/);
}

technique Terrain
{
	pass Pass1
	{
		VertexShader = compile vs_4_0 VertexShaderFunction();
		PixelShader = compile ps_4_0 PixelShaderFunction();
	}
}
