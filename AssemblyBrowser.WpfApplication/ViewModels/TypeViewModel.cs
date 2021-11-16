using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AssemblyBrowser.Core.Entities;

namespace AssemblyBrowser.WpfApplication.ViewModels;

public class TypeViewModel
{
    private IEnumerable<MemberViewModel> _members;

    private string _name;

    public TypeViewModel(TypeInformation typeInformation)
    {
        Name = typeInformation.Name;
        IEnumerable<MemberViewModel> memberPropertyView =
            typeInformation.Properties.Select(property => new MemberViewModel(property));
        IEnumerable<MemberViewModel> memberMethodView =
            typeInformation.Methods.Select(method => new MemberViewModel(method));
        IEnumerable<MemberViewModel> memberFieldView =
            typeInformation.Fields.Select(field => new MemberViewModel(field));
        Members = memberFieldView.Concat(memberPropertyView).Concat(memberMethodView);
    }


    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public IEnumerable<MemberViewModel> Members
    {
        get => _members;
        set
        {
            _members = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}