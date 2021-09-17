using UnityEngine;

public struct StarsBright // Настройки для шейдара звёзд
{
    private const string brightnessScaleName = "_BrightnessVariationScale", brightnessPowerName = "_BrightnessPower";
    public const float minbScale = 0.01f, maxbScale = 0.2f, minbPower = 1f, maxbPower = 4f;
    private static float brightScale, brightPower;
    public static float BrightScale { get { return brightScale; } set { if (value <= maxbScale && value >= minbScale) { brightScale = value; } } }
    public static float BrightPower { get { return brightPower; } set { if (value <= maxbPower && value >= minbPower) { brightPower = value; } } }

    public static void SetBrightScale(in Material material, in float value)
    {
        material.SetFloat(brightnessScaleName, BrightScale = value);
    }
    public static void SetBrightPower(in Material material, in float value)
    {
        material.SetFloat(brightnessPowerName, BrightPower = value);
    }




}