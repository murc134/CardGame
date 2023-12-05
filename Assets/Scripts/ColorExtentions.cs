using UnityEngine;


public static class ColorExtensions
{
    public static Color CurrentPlayerColor = Color.green.Darker();
    public static Color OtherPlayerColor = Color.red.Darker();
    public static Color DefaultColor = Color.gray.Darker();
    public static Color TextColor = Color.white;
    public static Color Darker(this Color color, float value = 0.5f)
    {
        float clampedValue = Mathf.Clamp01(value);
        return new Color(color.r * clampedValue, color.g * clampedValue, color.b * clampedValue, color.a);
    }

    public static Color Lighter(this Color color, float value = 0.5f)
    {
        float clampedValue = Mathf.Clamp01(value);
        float inverseValue = 1.0f - clampedValue;
        return new Color(color.r * inverseValue + clampedValue, color.g * inverseValue + clampedValue, color.b * inverseValue + clampedValue, color.a);
    }

    public static Color Orange(this Color color)
    {
        return new Color(1.0f, 0.5f, 0.0f, color.a); // RGB values for orange
    }
}