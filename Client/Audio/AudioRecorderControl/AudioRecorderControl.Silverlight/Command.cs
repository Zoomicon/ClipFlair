//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: Command.cs
//Version: 20121125

using System;
using System.Windows;
using System.Windows.Input;

namespace ClipFlair.AudioRecorder
{

  public class Command : DependencyObject, ICommand
  {
    public Action ExecuteAction { get; set; }

    private bool _canExecute;

    #region IsEnabled

    /// <summary>
    /// IsEnabled Dependency Property
    /// </summary>
    public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.Register("IsEnabled", typeof(bool), typeof(Command),
            new FrameworkPropertyMetadata(true,
                new PropertyChangedCallback(OnIsEnabledChanged)));

    /// <summary>
    /// Gets or sets the IsEnabled property.
    /// </summary>
    public bool IsEnabled
    {
      get { return (bool)GetValue(IsEnabledProperty); }
      set { SetValue(IsEnabledProperty, value); }
    }

    /// <summary>
    /// Handles changes to the IsEnabled property.
    /// </summary>
    private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Command target = (Command)d;
      bool oldIsEnabled = (bool)e.OldValue;
      bool newIsEnabled = target.IsEnabled;
      target.OnIsEnabledChanged(oldIsEnabled, newIsEnabled);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsEnabled property.
    /// </summary>
    protected virtual void OnIsEnabledChanged(bool oldIsEnabled, bool newIsEnabled)
    {
      if (oldIsEnabled != newIsEnabled)
          if (CanExecuteChanged != null)
            CanExecuteChanged(this, new EventArgs());
    }

    #endregion 

    #region ICommand

    public bool CanExecute(object parameter)
    {
      return IsEnabled;
    }

    public event EventHandler CanExecuteChanged;

    public virtual void Execute(object parameter)
    {
      if (ExecuteAction != null)
        ExecuteAction();
    }

    #endregion
  }

}
