//Filename: ToggleCommand.cs
//Version: 20120912

using System;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Zoomicon.AudioRecorder
{

  public class ToggleCommand : SimpleCommand
  {
    public Action ExecuteUncheckAction { get; set; }

    protected ToggleButton toggleButton;

    public bool IsChecked
    {
      get { return (toggleButton.IsChecked == true); }
      set {
        if (MayBeExecuted)
        {
          toggleButton.IsChecked = value;
          Execute(null);
        }
      }
    }

    private ToggleCommand()
    {
      throw new NotImplementedException();
    }

    public ToggleCommand(ToggleButton theToggleButton)
    {
      this.toggleButton = theToggleButton;
    }
    
    public override void Execute(object parameter)
    {
      if (ExecuteAction != null)
        if (toggleButton.IsChecked == true)
          ExecuteAction();
        else
          ExecuteUncheckAction();
    }
    
  }

}
