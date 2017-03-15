uniform extern texture ScreenTexture;

sampler ScreenS = sampler_state
{
	Texture = <ScreenTexture>;
};

//Varibles
float2 center;
float radius;
float2 size;

float4 PixelShaderFunction(float2 curCoord: TEXCOORD0) : COLOR
{
	float2 pixelCoord = curCoord * size;
	float2 diff = abs(pixelCoord - center);
	float dist = length(diff);

	float4 color = tex2D(ScreenS, curCoord);

	if(dist >= radius)
	{
		color.rgb = (color[0] + 0.21f, color[1] + 0.72f, color[2] + 0.07f);
	}

	return color;
}

technique Technique1
{
    pass P0
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
