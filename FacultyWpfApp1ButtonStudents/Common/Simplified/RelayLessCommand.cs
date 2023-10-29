using System;
using System.Collections.Generic;
namespace Simplified
{
    /// <summary>Реализация RelayCommand для методов без параметра.</summary>
    /// <typeparam name="T">Тип параметра методов.</typeparam>
    public class RelayLessCommand : RelayCommand, IEquatable<RelayLessCommand>
    {
        /// <summary> Command constructor. </summary>
        /// <param name="execute"> Command method to execute. </param>
        /// <param name="canExecute"> Method that returns the state of the command. </param>
        /// <param name="converter">Optional converter to convert <see cref="object"/> to <typeparamref name="T"/>. <br/>
        /// It is called when the parameter
        /// <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/is">
        /// is not compatible</see> with a <typeparamref name="T"/> type.
        /// </param>
        public RelayLessCommand(ExecuteHandler execute, CanExecuteHandler canExecute)
           : this(new CommandArgs(execute, canExecute ?? throw new ArgumentException(nameof(canExecute))))
        { }

        /// <summary> Command constructor. </summary>
        /// <param name="execute"> Command method to execute.</param>
        /// <param name="converter">Optional converter to convert <see cref="object"/> to <typeparamref name="T"/>.</param>
        public RelayLessCommand(ExecuteHandler execute)
           : this (new CommandArgs(execute, null))
        { }

        private RelayLessCommand(CommandArgs args)
            : base(args.Execute, args.CanExecute)
        { 
            this.args = args;
        }

        private readonly CommandArgs args;
        private struct CommandArgs : IEquatable<CommandArgs>
        {
            private readonly ExecuteHandler execute;
            private readonly CanExecuteHandler canExecute;

            public readonly ExecuteHandler<object> Execute;
            public readonly CanExecuteHandler<object> CanExecute;

            public CommandArgs(ExecuteHandler execute,
                               CanExecuteHandler canExecute) : this()
            {
                this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
                this.canExecute = canExecute;

                    Execute = obj =>  execute();
                    CanExecute = (canExecute is null) ? _ => true : _ => canExecute();
            }

            public override bool Equals(object obj)
            {
                return obj is CommandArgs args && Equals(args);
            }

            public bool Equals(CommandArgs other)
            {
                return EqualityComparer<ExecuteHandler>.Default.Equals(execute, other.execute) &&
                       EqualityComparer<CanExecuteHandler>.Default.Equals(canExecute, other.canExecute);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(execute, canExecute);
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RelayLessCommand);
        }

        public bool Equals(RelayLessCommand other)
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
