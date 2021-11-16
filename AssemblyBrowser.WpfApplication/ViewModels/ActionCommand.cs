using System;
using System.Windows.Input;

namespace AssemblyBrowser.WpfApplication.ViewModels;

public class ActionCommand : ICommand
{
    private readonly Action _execute;

    public ActionCommand(Action execute)
    {
        _execute = execute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _execute();
    }
}