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

    public ToggleButton Button { get; private set; }

    private ToggleCommand()
    {
      throw new NotImplementedException();
    }

    public ToggleCommand(ToggleButton theToggleButton)
    {
      this.Button = theToggleButton;
    }
    
    public override void Execute(object parameter)
    {
      if (ExecuteAction != null)
        if (Button.IsChecked == true)
          ExecuteAction();
        else
          ExecuteUncheckAction();
    }

  }

}
