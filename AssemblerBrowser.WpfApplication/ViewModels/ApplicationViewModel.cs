using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using AssemblerBrowser.Core.Entities;
using Microsoft.Win32;

namespace AssemblerBrowser.WpfApplication.ViewModels;

public class ApplicationViewModel : INotifyPropertyChanged
{
    private string? _assemblyFilePath;
    private string? _assemblyName;
    private IEnumerable<NamespaceViewModel>? _namespaces;

    public string? AssemblyFilePath
    {
        get => _assemblyFilePath;
        set
        {
            _assemblyFilePath = value;
            OnPropertyChanged(nameof(AssemblyFilePath));
        }
    }

    public string? AssemblyName
    {
        get => _assemblyName;
        set
        {
            _assemblyName = value;
            OnPropertyChanged(nameof(AssemblyName));
        }
    }

    public IEnumerable<NamespaceViewModel>? Namespaces
    {
        get => _namespaces;
        set
        {
            _namespaces = value;
            OnPropertyChanged(nameof(Namespaces));
        }
    }

    public ActionCommand LoadAssemblyCommand
    {
        get
        {
            return new ActionCommand(() =>
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Assemblies (*.dll, *.exe) | *.dll; *.exe"
                };
                bool? dialogResult = openFileDialog.ShowDialog();
                if (dialogResult != dialogResult) return;

                string assemblyFilePath = openFileDialog.FileName;
                var assembly = Assembly.LoadFile(assemblyFilePath);
                var assemblyInformation = new AssemblyInformation(assembly);
                Namespaces = assemblyInformation.Namespaces
                    .Select(namespaceInformation => new NamespaceViewModel(namespaceInformation));
                AssemblyFilePath = assemblyFilePath;
                AssemblyName = Path.GetFileNameWithoutExtension(AssemblyFilePath);
            });
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}