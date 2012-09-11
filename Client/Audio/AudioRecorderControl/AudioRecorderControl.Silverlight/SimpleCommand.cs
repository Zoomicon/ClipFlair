//Filename: SimpleCommand.cs
//Version: 20120911

using System;
using System.Windows.Input;

namespace AudioRecorder
{

  public class SimpleCommand : ICommand
  {
    public Action ExecuteAction { get; set; }

    private bool _canExecute;
    public bool MayBeExecuted
    {
      get { return _canExecute; }
      set
      {
        if (_canExecute != value)
        {
          _canExecute = value;
          if (CanExecuteChanged != null)
            CanExecuteChanged(this, new EventArgs());
        }
      }
    }

    #region ICommand

    public bool CanExecute(object parameter)
    {
      return MayBeExecuted;
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
      if (ExecuteAction != null)
        ExecuteAction();
    }

    #endregion
  }

}
