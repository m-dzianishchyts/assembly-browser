using System.Collections.Generic;
using System.Linq;
using AssemblyBrowser.Core.Entities;
using AssemblyBrowser.WpfApplication.TreeItem;

namespace AssemblyBrowser.WpfApplication.ViewModels;

public class TypeViewModel : LabeledTreeItem
{
    public TypeViewModel(TypeInformation typeInformation) : base(typeInformation.Name)
    {
        IEnumerable<MemberViewModel> propertyViewModels = typeInformation.Properties
            .Select(property => new MemberViewModel(property))
            .ToList();
        foreach (MemberViewModel propertyViewModel in propertyViewModels)
        {
            Children.Add(propertyViewModel);
        }

        IEnumerable<MemberViewModel> methodViewModels = typeInformation.Methods
            .Select(method => new MemberViewModel(method))
            .ToList();
        foreach (MemberViewModel methodViewModel in methodViewModels)
        {
            Children.Add(methodViewModel);
        }

        IEnumerable<MemberViewModel> fieldViewModels = typeInformation.Fields
            .Select(field => new MemberViewModel(field))
            .ToList();
        foreach (MemberViewModel fieldViewModel in fieldViewModels)
        {
            Children.Add(fieldViewModel);
        }

        IEnumerable<TypeViewModel> nestedTypeViewModels = typeInformation.NestedTypes
            .Select(nestedType => new TypeViewModel(nestedType))
            .ToList();
        foreach (TypeViewModel nestedTypeViewModel in nestedTypeViewModels)
        {
            Children.Add(nestedTypeViewModel);
        }
    }
}