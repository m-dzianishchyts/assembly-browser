using System;
using System.Collections.Generic;
using System.Linq;
using AssemblyBrowser.Core.Utilities;

namespace AssemblyBrowser.Core.Entities;

public class NamespaceInformation
{
    public readonly string Name;
    public readonly IEnumerable<TypeInformation> Types;

    public NamespaceInformation(string space, IEnumerable<Type> types)
    {
        Name = space;
        Types = types.Where(type => !CompilerUtilities.IsCompilerGenerated(type) && !type.IsNested)
            .Select(type => new TypeInformation(type));
    }
}