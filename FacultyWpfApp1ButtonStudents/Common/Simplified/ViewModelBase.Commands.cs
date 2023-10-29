using System.Runtime.CompilerServices;

namespace Simplified
{
    // Словарь для хранения команд.
    public abstract partial class ViewModelBase
    {
        protected RelayCommand<T> GetCommand<T>(ExecuteHandler<T> execute,
                                                CanExecuteHandler<T> canExecute,
                                                ConverterFromObjectHandler<T> converter = null,
                                                [CallerMemberName] string commandName = null)
        {
            var command = Get<RelayCommand<T>>(commandName);
            if (command is null)
            {
                command = new RelayCommand<T>(execute, canExecute, converter);
                Set(command, false, commandName);
            }
            return command;
        }

        protected RelayLessCommand GetCommand(ExecuteHandler execute,
                                              CanExecuteHandler canExecute,
                                              [CallerMemberName] string commandName = null)
        {
            var command = Get<RelayLessCommand>(commandName);
            if (command is null)
            {
                command = new RelayLessCommand(execute, canExecute);
                Set(command, false, commandName);
            }
            return command;
        }

        protected RelayCommand GetCommand(ExecuteHandler<object> execute,
                                          CanExecuteHandler<object> canExecute,
                                          ConverterFromObjectHandler<object> converter = null,
                                          [CallerMemberName] string commandName = null)
        {
            var command = Get<RelayCommand>(commandName);
            if (command is null)
            {
                command = new RelayCommand(execute, canExecute);
                Set(command, false, commandName);
            }
            return command;
        }
        protected RelayCommand<T> GetCommand<T>(ExecuteHandler<T> execute,
                                                ConverterFromObjectHandler<T> converter = null,
                                                [CallerMemberName] string commandName = null)
        {
            var command = Get<RelayCommand<T>>(commandName);
            if (command is null)
            {
                command = new RelayCommand<T>(execute, converter);
                Set(command, false, commandName);
            }
            return command;
        }

        protected RelayLessCommand GetCommand(ExecuteHandler execute,
                                              [CallerMemberName] string commandName = null)
        {
            var command = Get<RelayLessCommand>(commandName);
            if (command is null)
            {
                command = new RelayLessCommand(execute);
                Set(command, false, commandName);
            }
            return command;
        }

        protected RelayCommand GetCommand(ExecuteHandler<object> execute,
                                          ConverterFromObjectHandler<object> converter = null,
                                          [CallerMemberName] string commandName = null)
        {
            var command = Get<RelayCommand>(commandName);
            if (command is null)
            {
                command = new RelayCommand(execute);
                Set(command, false, commandName);
            }
            return command;
        }

        protected bool SetCommand<T>(ExecuteHandler<T> execute,
                                     CanExecuteHandler<T> canExecute = null,
                                     ConverterFromObjectHandler<T> converter = null,
                                     [CallerMemberName] string commandName = null)
        {
            return Set(new RelayCommand<T>(execute, canExecute, converter), commandName);
        }

        protected bool SetCommand(ExecuteHandler execute,
                                  CanExecuteHandler canExecute = null,
                                  [CallerMemberName] string commandName = null)
        {
            return Set(new RelayLessCommand(execute, canExecute), commandName);
        }

        protected bool SetCommand(ExecuteHandler<object> execute,
                                  CanExecuteHandler<object> canExecute = null,
                                  [CallerMemberName] string commandName = null)
        {
            return Set(new RelayCommand(execute, canExecute), commandName);
        }

    }

}
