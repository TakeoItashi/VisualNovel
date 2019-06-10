using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VisualNovelInterface.MVVM {
    public class RelayCommand : ICommand {
        private Action<object> execute;
        private Predicate<object> canExecute;
        private event EventHandler CanExecuteChangedInternal;

        public RelayCommand(Action<object> execute) : this(execute, DefaultCanExecute)
        {

        }

        public RelayCommand(Action<object> _execute, Predicate<object> _canExecute)
        {
            execute = _execute;
            canExecute = _canExecute;
        }

        //Überlegen ob man dies Entfernen kann
        public event EventHandler CanExecuteChanged {

            add {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object _parameter)
        {
            return canExecute != null && canExecute(_parameter);
        }

        public void Execute(object _parameter)
        {
            execute(_parameter);
        }

        public void OnCanExecuteChannged()
        {
            EventHandler handler = CanExecuteChangedInternal;

            if (handler != null) {
                //TODO warum EventArgs.Empty?
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void Destroy()
        {
            canExecute = lambdaExpression => false;
            execute = lambdaExpresission => { return; };
        }
        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }
}
