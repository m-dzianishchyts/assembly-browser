using System;
using System.Windows.Input;

namespace AssemblerBrowser.WpfApplication.ViewModels;

public class ActionCommand : ICommand
{
    private readonly Action _execute;

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public ActionCommand(Action execute)
    {
        this._execute = execute;
    }

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        this._execute();
    }
}