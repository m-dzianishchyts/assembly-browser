using System.ComponentModel;
using System.Runtime.CompilerServices;
using AssemblerBrowser.Core.Entities;

namespace AssemblerBrowser.WpfApplication.ViewModels;

public class MemberViewModel
{
    private string _name;

    public MemberViewModel(FieldInformation field)
    {
        Name = field.Name;
    }

    public MemberViewModel(PropertyInformation property)
    {
        Name = property.Name;
    }

    public MemberViewModel(MethodInformation method)
    {
        Name = method.Name;
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

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}