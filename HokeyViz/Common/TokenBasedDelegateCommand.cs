using System;
using System.Windows.Input;

namespace HokeyViz.Common
{
    class TokenBasedDelegateCommand : ICommand
    {
        private bool canExecute;
        private Action<object> action;

        private TokenBasedDelegateCommand(Action<object> action)
        {
            this.action = action;
        }

        private void SetCanExecute(bool canExecute)
        {
            if (this.canExecute == canExecute)
            {
                return;
            }

            this.canExecute = canExecute;
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute;
        }

        public void Execute(object parameter)
        {
            this.action(parameter);
        }

        public event EventHandler CanExecuteChanged;
        
        public class Token
        {
            private Token(TokenBasedDelegateCommand command)
            {
                this.Command = command;
            }

            public static Token Create(Action<object> action)
            {
                var command = new TokenBasedDelegateCommand(action);
                return new Token(command);
            }

            public void SetCanExecute(bool canExecute)
            {
                this.Command.SetCanExecute(canExecute);
            }

            public TokenBasedDelegateCommand Command { get; }
        }
    }
}
