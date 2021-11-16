using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssemblyBrowser.Core.Utilities;

namespace AssemblyBrowser.Core.Entities;

public class TypeInformation
{
    private const BindingFlags TypeBindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static |
                                                  BindingFlags.Instance | BindingFlags.DeclaredOnly;

    public readonly IEnumerable<FieldInformation> Fields;
    public readonly IEnumerable<MethodInformation> Methods;
    public readonly IEnumerable<TypeInformation> NestedTypes;
    public readonly IEnumerable<PropertyInformation> Properties;

    public TypeInformation(Type type)
    {
        Name = $"{ModifierUtilities.GetClassModifiers(type)} {TypeUtilities.GetName(type)}";
        Methods = GetMethods(type.GetMethods(TypeBindingFlags));
        Fields = GetFields(type.GetFields(TypeBindingFlags));
        Properties = GetProperties(type.GetProperties(TypeBindingFlags));
        NestedTypes = GetNestedTypes(type.GetNestedTypes(TypeBindingFlags));
    }

    public string Name { get; }

    private static IEnumerable<TypeInformation> GetNestedTypes(IEnumerable<Type> members)
    {
        var nestedTypes = new List<TypeInformation>();
        foreach (Type member in members)
        {
            if (!CompilerUtilities.IsCompilerGenerated(member))
            {
                nestedTypes.Add(new TypeInformation(member));
            }
        }

        return nestedTypes;
    }

    private static IEnumerable<MethodInformation> GetMethods(IEnumerable<MethodInfo> members)
    {
        return members.Where(member => !CompilerUtilities.IsCompilerGenerated(member))
            .Select(member => new MethodInformation(member));
    }

    private static IEnumerable<FieldInformation> GetFields(IEnumerable<FieldInfo> members)
    {
        return members.Where(member => !CompilerUtilities.IsCompilerGenerated(member))
            .Select(member => new FieldInformation(member));
    }

    private static IEnumerable<PropertyInformation> GetProperties(IEnumerable<PropertyInfo> members)
    {
        return members.Where(member => !CompilerUtilities.IsCompilerGenerated(member))
            .Select(member => new PropertyInformation(member));
    }
}