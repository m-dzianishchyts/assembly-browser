﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AssemblyBrowser.Core.Entities;

namespace AssemblyBrowser.WpfApplication.ViewModels;

public class NamespaceViewModel : INotifyPropertyChanged
{
    private string _name;
    private IEnumerable<TypeViewModel> _types;

    public NamespaceViewModel(NamespaceInformation namespaceInformation)
    {
        Name = namespaceInformation.Name;
        Types = namespaceInformation.Types.Select(type => new TypeViewModel(type));
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

    public IEnumerable<TypeViewModel> Types
    {
        get => _types;
        set
        {
            _types = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}