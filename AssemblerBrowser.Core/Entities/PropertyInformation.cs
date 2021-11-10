using System.Reflection;
using AssemblerBrowser.Core.Utilities;

namespace AssemblerBrowser.Core.Entities;

public class PropertyInformation
{
    public readonly string Name;

    public PropertyInformation(PropertyInfo property)
    {
        Name =
            $"{TypeUtilities.GetName(property.PropertyType)} {property.Name} {{ {ModifierUtilities.GetPropertyModifiers(property)} }}";
    }
}