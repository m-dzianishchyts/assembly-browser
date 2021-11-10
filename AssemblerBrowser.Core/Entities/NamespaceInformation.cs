using System;
using System.Collections.Generic;
using System.Linq;
using AssemblerBrowser.Core.Utilities;

namespace AssemblerBrowser.Core.Entities
{
    public class NamespaceInformation
    {
        public readonly IEnumerable<TypeInformation> Types;
        public readonly string Name;

        public NamespaceInformation(string space, IEnumerable<Type> types)
        {
            Name = space;
            Types = types.Where(type => !CompilerUtilities.IsCompilerGenerated(type))
                .Select(type => new TypeInformation(type));
        }
    }
}