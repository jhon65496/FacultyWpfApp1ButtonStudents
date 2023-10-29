using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Simplified
{
    /// <summary>Класс реализующий <see cref="ICommand"/>.<br/>
    /// Реализация взята из <see href="https://www.cyberforum.ru/wpf-silverlight/thread2390714-page4.html#post13535649"/>
    /// и дополнена конструктором для методов без параметра.</summary>
    public class RelayCommand : ICommand, IEquatable<RelayCommand>
    {
        private readonly CanExecuteHandler<object> canExecute;
        private readonly ExecuteHandler<object> execute;
        private readonly EventHandler requerySuggested;

        /// <summary>Событие извещающее об изменении состояния команды.</summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>Конструктор команды.</summary>
        /// <param name="execute">Выполняемый метод команды.</param>
        /// <param name="canExecute">Метод, возвращающий состояние команды.</param>
        public RelayCommand(ExecuteHandler<object> execute, CanExecuteHandler<object> canExecute)
           : this()
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        /// <inheritdoc cref="RelayCommand(ExecuteHandler{object}, CanExecuteHandler{object})"/>
        public RelayCommand(ExecuteHandler<object> execute)
          : this(execute, obj => true)
        { }

        private static readonly Dispatcher dispatcher = Application.Current.Dispatcher;

        /// <summary>Метод, подымающий событие <see cref="CanExecuteChanged"/>.</summary>
        public void RaiseCanExecuteChanged()
        {
            if (dispatcher.CheckAccess())
            {
                invalidate();
            }
            else
            {
                _ = dispatcher.BeginInvoke(invalidate);
            }
        }
        private readonly Action invalidate;
        private RelayCommand()
        {
            invalidate = () => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            requerySuggested = (o, e) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            CommandManager.RequerySuggested += requerySuggested;
        }

        /// <summary>Вызов метода, возвращающего состояние команды.</summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns><see langword="true"/> - если выполнение команды разрешено.</returns>
        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        /// <summary>Вызов выполняющего метода команды.</summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RelayCommand);
        }

        public bool Equals(RelayCommand other)
        {
            return other is not null &&
                   EqualityComparer<CanExecuteHandler<object>>.Default.Equals(canExecute, other.canExecute) &&
                   EqualityComparer<ExecuteHandler<object>>.Default.Equals(execute, other.execute);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(canExecute, execute);
        }
    }
}
