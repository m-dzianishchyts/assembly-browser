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
        var nonGenericName = $"{withoutSimpleName}.{noNestingName}";
        return nonGenericName;
    }

    private static string GetGenericName(Type type)
    {
        var typeName = "";
        string temp = type.GetGenericTypeDefinition().Name;
        int indexOfBackQuote = temp.LastIndexOf('`');
        typeName += string.Concat(temp.AsSpan(0, indexOfBackQuote), "<");
        Type[] argumentTypes = type.GetGenericArguments();
        typeName = argumentTypes.Aggregate(typeName, (current, argumentType) => current + GetName(argumentType) + ", ");

        typeName = string.Concat(typeName.AsSpan(0, typeName.Length - 2), ">");
        return typeName;
    }
}