using System;
using System.Windows.Input;

namespace ConsoleApp1
{
    public class MyCommand : ICommand
    {
        private readonly Action _action;

        public MyCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged;
    }
}