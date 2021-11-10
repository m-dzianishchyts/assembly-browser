using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssemblerBrowser.Core.Utilities;

namespace AssemblerBrowser.Core.Entities
{
    public class TypeInformation
    {
        private static readonly BindingFlags TypeBindingFlags =
            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static |
            BindingFlags.Instance | BindingFlags.DeclaredOnly;

        public readonly IEnumerable<FieldInformation> Fields;
        public readonly IEnumerable<MethodInformation> Methods;
        public readonly IEnumerable<PropertyInformation> Properties;

        public TypeInformation(Type type)
        {
            Name = $"{ModifierUtilities.GetClassModifiers(type)}{TypeUtilities.GetName(type)}";
            Methods = GetMethods(type.GetMethods(TypeBindingFlags));
            Fields = GetFields(type.GetFields(TypeBindingFlags));
            Properties = GetProperties(type.GetProperties(TypeBindingFlags));
        }

        public string Name { get; }

        private static IEnumerable<MethodInformation> GetMethods(IEnumerable<MemberInfo> members)
        {
            return members.Where(member => !CompilerUtilities.IsCompilerGenerated(member))
                .Select(member => new MethodInformation((MethodInfo) member));
        }

        private static IEnumerable<FieldInformation> GetFields(IEnumerable<MemberInfo> members)
        {
            return members.Where(member => !CompilerUtilities.IsCompilerGenerated(member))
                .Select(member => new FieldInformation((FieldInfo) member));
        }

        private static IEnumerable<PropertyInformation> GetProperties(IEnumerable<MemberInfo> members)
        {
            return members.Where(member => !CompilerUtilities.IsCompilerGenerated(member))
                .Select(member => new PropertyInformation((PropertyInfo) member));
        }
    }
}