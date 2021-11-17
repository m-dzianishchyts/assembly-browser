using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblyBrowser.Core.Utilities;

public class TypeUtilities
{
    public static string GetName(Type type)
    {
        return type.IsGenericType ? GetGenericName(type) : GetNonGenericName(type);
    }

    private static string GetNonGenericName(Type type)
    {
        string fullName = type.FullName ?? type.Name;
        IEnumerable<string> fullNameParts = fullName.Split(".");
        string simpleName = fullNameParts.Last();
        string withoutSimpleName = string.Join(".", fullNameParts.SkipLast(1));
        string noNestingName = simpleName.Split("+").Last();
        string nonGenericName = string.IsNullOrEmpty(withoutSimpleName)
            ? noNestingName
            : $"{withoutSimpleName}.{noNestingName}";
        return nonGenericName;
    }

    private static string GetGenericName(Type type)
    {
        string genericTypeName = type.GetGenericTypeDefinition().Name;
        int indexOfBackQuote = genericTypeName.LastIndexOf('`');
        ReadOnlySpan<char> typeName = genericTypeName.AsSpan(0, indexOfBackQuote);
        Type[] argumentTypes = type.GetGenericArguments();
        IEnumerable<string> argumentTypeNames = argumentTypes.Select(GetName);
        var argumentsDeclaration = $"<{string.Join(", ", argumentTypeNames)}>";
        string genericName = string.Concat(typeName, argumentsDeclaration);
        return genericName;
    }
}