using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController
{
    public static NamedColor[] GetAllNamedColors()
    {
        Type colorType = typeof(Color);
        PropertyInfo[] allColorProperties = colorType.GetProperties();
        PropertyInfo[] onlyStaticColorProperties = allColorProperties.Where(cp => cp.GetGetMethod().IsStatic).ToArray();

        Color c = new Color();
        string c_Name = string.Empty;
        List<NamedColor> allColors = new List<NamedColor>();
        for (int i = 0; i < onlyStaticColorProperties.Length; i++) {
            MethodInfo mi = onlyStaticColorProperties[i].GetGetMethod();
            c_Name = onlyStaticColorProperties[i].Name;
            c = (Color)mi.Invoke(onlyStaticColorProperties[i], new object[] { });
            if (c.Equals(Color.clear)) continue;
            else allColors.Add(new NamedColor(c_Name, c));
        }

        return allColors.ToArray();
    }
}
