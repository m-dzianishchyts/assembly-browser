using AssemblyBrowser.Core.Entities;

namespace AssemblyBrowser.WpfApplication.ViewModels;

public class MemberViewModel : TreeItemViewModel
{
    public MemberViewModel(FieldInformation field) : base(field.Name)
    {
    }

    public MemberViewModel(PropertyInformation property) : base(property.Name)
    {
    }

    public MemberViewModel(MethodInformation method) : base(method.Name)
    {
    }
}