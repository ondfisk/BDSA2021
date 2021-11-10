namespace AsyncAndParallel.UI.ViewModels;

public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;

    private readonly Func<object?, bool> _canExecute;

    public event EventHandler? CanExecuteChanged;

    public RelayCommand(Action<object?> execute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = _ => true;

    }

    public RelayCommand(Action<object?> execute, Func<object?, bool> canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecute(parameter);

    public void Execute(object? parameter)
    {
        _execute(parameter);
    }

    public void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
