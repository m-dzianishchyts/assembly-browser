using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblerBrowser.Core.Utilities;

public class CompilerUtilities
{
    public static bool IsCompilerGenerated(MemberInfo member)
    {
        var compilerGenerated = false;
        compilerGenerated |= Attribute.GetCustomAttribute(member, typeof(CompilerGeneratedAttribute)) != null;

        switch (member.MemberType)
        {
            case MemberTypes.Method:
                compilerGenerated |= ((MethodInfo)member).IsSpecialName;
                break;
            case MemberTypes.Property:
                compilerGenerated |= ((PropertyInfo)member).IsSpecialName;
                break;
        }

        return compilerGenerated;
    }
}