using System;
using System.Collections.Generic;
namespace Simplified
{
    /// <summary>Реализация RelayCommand для методов с обобщённым параметром.</summary>
    /// <typeparam name="T">Тип параметра методов.</typeparam>
    public class RelayCommand<T> : RelayCommand, IEquatable<RelayCommand<T>>
    {
        /// <summary> Command constructor. </summary>
        /// <param name="execute"> Command method to execute. </param>
        /// <param name="canExecute"> Method that returns the state of the command. </param>
        /// <param name="converter">Optional converter to convert <see cref="object"/> to <typeparamref name="T"/>. <br/>
        /// It is called when the parameter
        /// <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/is">
        /// is not compatible</see> with a <typeparamref name="T"/> type.
        /// </param>
        public RelayCommand(ExecuteHandler<T> execute, CanExecuteHandler<T> canExecute, ConverterFromObjectHandler<T> converter = null)
           : this(new CommandArgs(execute, canExecute ?? throw new ArgumentException(nameof(canExecute)), converter))
        { }

        /// <summary> Command constructor. </summary>
        /// <param name="execute"> Command method to execute.</param>
        /// <param name="converter">Optional converter to convert <see cref="object"/> to <typeparamref name="T"/>.</param>
        public RelayCommand(ExecuteHandler<T> execute, ConverterFromObjectHandler<T> converter = null)
           : this (new CommandArgs(execute, null, converter))
        { }

        private RelayCommand(CommandArgs args)
            : base(args.Execute, args.CanExecute)
        { 
            this.args = args;
        }

        private readonly CommandArgs args;
        private struct CommandArgs : IEquatable<CommandArgs>
        {
            private readonly ExecuteHandler<T> execute;
            private readonly CanExecuteHandler<T> canExecute;
            private readonly ConverterFromObjectHandler<T> converter;

            public readonly ExecuteHandler<object> Execute;
            public readonly CanExecuteHandler<object> CanExecute;

            public CommandArgs(ExecuteHandler<T> execute,
                               CanExecuteHandler<T> canExecute,
                               ConverterFromObjectHandler<T> converter)
            {
                this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
                this.canExecute = canExecute;
                this.converter = converter;

                if (converter is null)
                {
                    Execute = obj => { if (obj is T t) execute(t); };
                    CanExecute = (canExecute is null) ? obj => obj is T : obj => obj is T t && canExecute(t);
                }
                else
                {
                    Execute = obj => { if (obj is T t || converter(obj, out t)) execute(t); };
                    CanExecute = (canExecute is null) ? obj => (obj is T t || converter(obj, out t))
                    : obj => (obj is T t || converter(obj, out t)) && canExecute(t);
                }
            }

            public override bool Equals(object obj)
            {
                return obj is CommandArgs args && Equals(args);
            }

            public bool Equals(CommandArgs other)
            {
                return EqualityComparer<ExecuteHandler<T>>.Default.Equals(execute, other.execute) &&
                       EqualityComparer<CanExecuteHandler<T>>.Default.Equals(canExecute, other.canExecute) &&
                       EqualityComparer<ConverterFromObjectHandler<T>>.Default.Equals(converter, other.converter);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(execute, canExecute, converter);
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RelayCommand<T>);
        }

        public bool Equals(RelayCommand<T> other)
        {
            return other is not null &&
                   args.Equals(other.args);
        }

        public override int GetHashCode()
        {
            return args.GetHashCode();
        }
    }
}
