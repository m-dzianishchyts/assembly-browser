using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyBrowser.Core.Utilities;

public class CompilerUtilities
{
    public static bool IsCompilerGenerated(MemberInfo member)
    {
        var compilerGenerated = false;
        compilerGenerated |= Attribute.GetCustomAttribute(member, typeof(CompilerGeneratedAttribute)) != null;

        switch (member)
        {
            case MethodInfo method:
                compilerGenerated |= method.IsSpecialName;
                break;
            case PropertyInfo property:
                compilerGenerated |= property.IsSpecialName;
                break;
            case TypeInfo type:
                compilerGenerated |= type.IsSpecialName;
                break;
            case Type type:
                compilerGenerated |= type.IsSpecialName;
                break;
        }

        return compilerGenerated;
    }
}