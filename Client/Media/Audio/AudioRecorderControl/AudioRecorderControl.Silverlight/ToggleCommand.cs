//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ToggleCommand.cs
//Version: 20121125

using System;
using System.Windows;

namespace ClipFlair.AudioRecorder
{

  public class ToggleCommand : Command
  {
    public Action ExecuteUncheckAction { get; set; }

    #region IsChecked

    /// <summary>
    /// IsChecked Dependency Property
    /// </summary>
    public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.Register("IsChecked", typeof(bool), typeof(ToggleCommand),
            new FrameworkPropertyMetadata(false,
                new PropertyChangedCallback(OnIsCheckedChanged)));

    /// <summary>
    /// Gets or sets the IsChecked property.
    /// </summary>
    public bool IsChecked
    {
      get { return (bool)GetValue(IsCheckedProperty); }
      set { SetValue(IsCheckedProperty, value); }
    }

    /// <summary>
    /// Handles changes to the IsChecked property.
    /// </summary>
    private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ToggleCommand target = (ToggleCommand)d;
      bool oldIsChecked = (bool)e.OldValue;
      bool newIsChecked = target.IsChecked;
      target.OnIsCheckedChanged(oldIsChecked, newIsChecked);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsChecked property.
    /// </summary>
    protected virtual void OnIsCheckedChanged(bool oldIsChecked, bool newIsChecked)
    {
      if (newIsChecked != oldIsChecked) 
        Execute(null);
    }

    #endregion

    public override void Execute(object parameter)
    {
        if (IsChecked) { if (ExecuteAction != null) ExecuteAction(); }
        else { if (ExecuteUncheckAction != null) ExecuteUncheckAction(); }
    }
    
  }

}
