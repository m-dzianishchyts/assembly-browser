using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace AssemblyBrowser;

public class ApplicationViewModel : INotifyPropertyChanged
{
    private string? _assemblyFilePath;
    private string? _assemblyName;

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


    public ActionCommand LoadAssemblyCommand
    {
        get
        {
            return new ActionCommand(() =>
            {
                var openFileDialog = new OpenFileDialog();
                bool? dialogResult = openFileDialog.ShowDialog();
                if (dialogResult != true)
                {
                    return;
                }

                // todo: Load assembly data to tree-like collection
                AssemblyFilePath = openFileDialog.FileName;
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