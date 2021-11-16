using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyBrowser.Core.Entities;

public class AssemblyInformation
{
    public readonly IEnumerable<NamespaceInformation> Namespaces;

    public AssemblyInformation(Assembly assembly)
    {
        Namespaces = ExtractAssemblyInformation(assembly)
            .Select(namespaceToTypesPair =>
                new NamespaceInformation(namespaceToTypesPair.Key, namespaceToTypesPair.Value))
            .Where(namespaceInformation => namespaceInformation.Types.Any());
    }

    private static Dictionary<string, List<Type>> ExtractAssemblyInformation(Assembly assembly)
    {
        IEnumerable<Type> types = GetLoadedAssemblyTypes(assembly);
        return GetNamespacesToTypesDictionary(types);
    }

    private static IEnumerable<Type> GetLoadedAssemblyTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException e)
        {
            return e.Types.Where(type => type is not null).ToList()!;
        }
    }

    private static Dictionary<string, List<Type>> GetNamespacesToTypesDictionary(IEnumerable<Type> types)
    {
        var namespacesToClassesDictionary = new Dictionary<string, List<Type>>();
        foreach (Type? type in types)
        {
            string name = type.Namespace ?? "<>";
            if (!namespacesToClassesDictionary.ContainsKey(name))
            {
                namespacesToClassesDictionary.Add(name, new List<Type>());
            }

            namespacesToClassesDictionary.TryGetValue(name, out List<Type>? classes);
            classes?.Add(type);
        }

        return namespacesToClassesDictionary;
    }
}