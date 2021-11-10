using System.Reflection;
using AssemblerBrowser.Core.Utilities;

namespace AssemblerBrowser.Core.Entities;

public class FieldInformation
{
    public readonly string Name;

    public FieldInformation(FieldInfo field)
    {
        Name = $"{ModifierUtilities.GetFieldModifiers(field)}{TypeUtilities.GetName(field.FieldType)} {field.Name}";
    }
}