using System.Collections.Generic;
using System.Linq;
using AssemblyBrowser.Core.Entities;

namespace AssemblyBrowser.WpfApplication.ViewModels;

public class NamespaceViewModel : TreeItemViewModel
{
    public NamespaceViewModel(NamespaceInformation namespaceInformation) : base(namespaceInformation.Name)
    {
        IEnumerable<TypeViewModel> typeViewModels = namespaceInformation.Types.Select(type => new TypeViewModel(type));
        foreach (TypeViewModel typeViewModel in typeViewModels)
        {
            Children.Add(typeViewModel);
        }
    }
}