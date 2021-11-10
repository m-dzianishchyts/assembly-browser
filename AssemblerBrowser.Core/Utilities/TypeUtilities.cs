using System;

namespace AssemblerBrowser.Core.Utilities;

public class TypeUtilities
{
    public static string GetName(Type type)
    {
        return type.IsGenericType ? GetGenericName(type) : type.Name;
    }

    private static string GetGenericName(Type type)
    {
        var typeName = "";
        string temp = type.GetGenericTypeDefinition().Name;
        int indexOfBackQuote = temp.LastIndexOf('`');
        typeName += string.Concat(temp.AsSpan(0, indexOfBackQuote), "<");
        var argumentTypes = type.GetGenericArguments();
        foreach (var argumentType in argumentTypes)
            if (argumentType.IsGenericType)
                typeName += GetName(argumentType) + ", ";
            else
                typeName += argumentType.Name + ", ";

        typeName = string.Concat(typeName.AsSpan(0, typeName.Length - 2), ">");
        return typeName;
    }
}