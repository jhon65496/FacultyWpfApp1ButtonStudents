using System.Windows;
using System.Windows.Input;

namespace Simplified
{
    public static class CommandExtensionMethods
    {
        /// <summary>Проверяет состояние комадны и,
        /// если она разрешена, то исполныет её.</summary>
        /// <param name="command">Команда.</param>
        /// <param name="parameter">Параметр команды.</param>
        /// <param name="target">Целевой элемент для <see cref="RoutedCommand"/>.</param>
        /// <returns>Возвращает <see langword="true"/>,
        /// если команда была выполнена.
        /// Если <paramref name="command"/> <see cref="RoutedCommand"/>,
        /// то вызывается <see cref="TryExecute(RoutedCommand, object, IInputElement)"/>.</returns>
        public static bool TryExecute(this ICommand command, object parameter, IInputElement target)
        {
            if (command is RoutedCommand routedCommand)
                return routedCommand.TryExecute(parameter, target);

            bool can = command.CanExecute(parameter);
            if (can)
                command.Execute(parameter);
            return can;
        }

         /// <inheritdoc cref="TryExecute(ICommand, object, IInputElement)"/>
       public static bool TryExecute(this ICommand command, object parameter)
            =>TryExecute(command, parameter, null);

        /// <inheritdoc cref="TryExecute(ICommand, object)"/>
        public static bool TryExecute(this ICommand command)
          => TryExecute(command, null);

        /// <inheritdoc cref="TryExecute(ICommand, object)"/>
        public static bool TryExecute(this ICommand command, IInputElement target)
          => TryExecute(command, null, target);

        /// <summary>Проверяет состояние всплывающей комадны и,
        /// если она разрешена, то исполныет её.</summary>
        /// <param name="command">Всплывающая Команда.</param>
        /// <param name="parameter">Параметр команды.</param>
        /// <param name="target">Целевой элемент для <see cref="RoutedCommand"/>.</param>
        /// <returns>Возвращает <see langword="true"/>,
        /// если команда была выполнена.</returns>
        public static bool TryExecute(this RoutedCommand command, object parameter, IInputElement target)
        {
            bool can = command.CanExecute(parameter, target);
            if (can)
                command.Execute(parameter, target);
            return can;
        }

        /// <inheritdoc cref="TryExecute(RoutedCommand, object, IInputElement)"/>
        public static bool TryExecute(this RoutedCommand command, IInputElement target)
          => TryExecute(command, null, target);
    }

}
