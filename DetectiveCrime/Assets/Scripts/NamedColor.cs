using UnityEngine;

public struct NamedColor
{
    public string ColorName { get; set; }
    public Color InnerColor { get; set; }

    public NamedColor(string colorName, Color innerColor)
    {
        ColorName = colorName;
        InnerColor = innerColor;
    }
}
