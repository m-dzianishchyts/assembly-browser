using System;
using System.Reflection;

namespace AssemblerBrowser.Core.Utilities;

public class ModifierUtilities
{
    public static string GetMethodModifiers(MethodInfo method)
    {
        var result = "";

        if (method.IsPrivate)
            result += "private ";
        if (method.IsFamily)
            result += "protected ";
        if (method.IsFamilyOrAssembly)
            result += "protected internal ";
        if (method.IsAssembly)
            result += "internal ";
        if (method.IsPublic)
            result += "public ";
        if (method.IsStatic)
            result += "static ";
        if (method.IsAbstract)
            result += "abstract ";
        if (method.IsVirtual)
            result += "virtual ";
        return result;
    }

    public static string GetClassModifiers(Type type)
    {
        var result = "";

        if (type.IsPublic)
            result += "public ";
        if (type.IsNestedPrivate)
            result += "private ";
        if (type.IsInterface)
            return result + "interface ";
        if (type.IsAbstract)
            result += "abstract ";
        if (type.IsNestedFamily)
            result += "protected ";
        return result;
    }

    public static string GetFieldModifiers(FieldInfo field)
    {
        var result = "";

        if (field.IsPrivate)
            result += "private ";
        if (field.IsFamily)
            result += "protected ";
        if (field.IsFamilyOrAssembly)
            result += "protected internal ";
        if (field.IsAssembly)
            result += "internal ";
        if (field.IsPublic)
            result += "public ";
        if (field.IsStatic)
            result += "static ";
        if (field.IsInitOnly)
            result += "readonly ";
        return result;
    }

    public static string GetPropertyModifiers(PropertyInfo property)
    {
        var result = "";

        if (property.CanRead)
            result += "public";
        else
            result += "private ";

        result += " get; ";
        if (property.CanWrite)
            result += "public";
        else
            result += "private";

        result += " set;";
        return result;
    }
}