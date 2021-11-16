using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyBrowser.Core.Utilities;

public class ModifierUtilities
{
    public static string GetMethodModifiers(MethodInfo method)
    {
        IEnumerable<string> modifiers = new List<string>
        {
            DetermineAccessModificator(method),
            DetermineStatic(method),
            DetermineSpecialModificator(method)
        };
        modifiers = modifiers.Where(modifier => !string.IsNullOrEmpty(modifier));

        return string.Join(" ", modifiers);
    }

    public static string GetClassModifiers(Type type)
    {
        IEnumerable<string> modifiers = new List<string>
        {
            DetermineAccessModificator(type),
            DetermineSpecialModificator(type),
            DetermineTypeKind(type)
        };
        modifiers = modifiers.Where(modifier => !string.IsNullOrEmpty(modifier));

        return string.Join(" ", modifiers);
    }

    public static string GetFieldModifiers(FieldInfo field)
    {
        IEnumerable<string> modifiers = new List<string>
        {
            DetermineAccessModificator(field),
            DetermineStatic(field),
            DetermineConsistencyModificator(field)
        };
        modifiers = modifiers.Where(modifier => !string.IsNullOrEmpty(modifier));

        return string.Join(" ", modifiers);
    }

    public static string GetPropertyModifiers(PropertyInfo property)
    {
        var propertyModifiers = new List<string>();

        if (property.GetMethod is { } getMethod)
        {
            IEnumerable<string> getModifiers = new List<string>
            {
                DetermineAccessModificator(getMethod),
                DetermineStatic(getMethod),
                DetermineSpecialModificator(getMethod)
            };
            getModifiers = getModifiers.Where(modifier => !string.IsNullOrEmpty(modifier));

            propertyModifiers.Add(string.Join(" ", getModifiers) + " get;");
        }

        if (property.SetMethod is { } setMethod)
        {
            IEnumerable<string> setModifiers = new List<string>
            {
                DetermineAccessModificator(setMethod),
                DetermineStatic(setMethod),
                DetermineSpecialModificator(setMethod)
            };
            setModifiers = setModifiers.Where(modifier => !string.IsNullOrEmpty(modifier));

            propertyModifiers.Add(string.Join(" ", setModifiers) + " set;");
        }

        return string.Join(" ", propertyModifiers);
    }

    private static string DetermineSpecialModificator(MethodInfo method)
    {
        if (method.IsAbstract)
        {
            return "abstract";
        }

        if (method.IsFinal)
        {
            return "sealed";
        }

        return method.IsVirtual ? "virtual" : string.Empty;
    }

    private static string DetermineStatic(MethodBase method)
    {
        return method.IsStatic ? "static" : string.Empty;
    }

    private static string DetermineStatic(FieldInfo field)
    {
        return field.IsStatic ? "static" : string.Empty;
    }

    private static string DetermineAccessModificator(MethodInfo method)
    {
        if (method.IsPublic)
        {
            return "public";
        }

        if (method.IsPrivate)
        {
            return "private";
        }

        if (method.IsFamily)
        {
            return "protected";
        }

        if (method.IsAssembly)
        {
            return "internal";
        }

        if (method.IsFamilyOrAssembly)
        {
            return "protected internal";
        }

        return method.IsFamilyAndAssembly ? "private protected" : string.Empty;
    }

    private static string DetermineAccessModificator(Type type)
    {
        if (type.IsPublic || type.IsNestedPublic)
        {
            return "public";
        }

        if (type.IsNestedPrivate)
        {
            return "private";
        }

        if (type.IsNestedFamily)
        {
            return "protected";
        }

        if (type.IsNestedAssembly)
        {
            return "internal";
        }

        if (type.IsNestedFamORAssem)
        {
            return "protected internal";
        }

        return type.IsNestedFamANDAssem ? "private protected" : string.Empty;
    }

    private static string DetermineAccessModificator(FieldInfo field)
    {
        if (field.IsPublic)
        {
            return "public";
        }

        if (field.IsPrivate)
        {
            return "private";
        }

        if (field.IsFamily)
        {
            return "protected";
        }

        if (field.IsAssembly)
        {
            return "internal";
        }

        if (field.IsFamilyOrAssembly)
        {
            return "protected internal";
        }

        return field.IsFamilyAndAssembly ? "private protected" : string.Empty;
    }

    private static string DetermineSpecialModificator(Type type)
    {
        return type.IsAbstract switch
        {
            true when type.IsSealed => "static",
            true => "abstract",
            false => type.IsSealed ? "sealed" : string.Empty
        };
    }

    private static string DetermineTypeKind(Type type)
    {
        if (type.IsInterface)
        {
            return "interface";
        }

        if (type.IsClass)
        {
            return "class";
        }

        return type.IsValueType ? "struct" : string.Empty;
    }

    private static string DetermineConsistencyModificator(FieldInfo field)
    {
        if (field.IsLiteral)
        {
            return "const";
        }

        return field.IsInitOnly ? "readonly" : string.Empty;
    }
}