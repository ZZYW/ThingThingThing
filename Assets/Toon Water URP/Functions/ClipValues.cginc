void ClipValues_half(out half2 Out)
{
	Out = half2(
		UNITY_NEAR_CLIP_VALUE,
		UNITY_RAW_FAR_CLIP_VALUE
		);
}

void ClipValues_float(out half2 Out)
{
	Out = half2(
		UNITY_NEAR_CLIP_VALUE,
		UNITY_RAW_FAR_CLIP_VALUE
		);
}