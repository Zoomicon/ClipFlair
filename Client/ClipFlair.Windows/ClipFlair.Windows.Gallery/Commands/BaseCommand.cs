//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseCommand.cs
//Version: 20130828

using System;
using System.Windows.Controls.Pivot;

namespace ClipFlair.Windows.Gallery.Commands
{

  public abstract class BaseCommand : IPivotViewerUICommand
  {
     public BaseCommand()
    {
      DisplayName = "";
      Icon = null;
      ToolTip = "";
      IsExecutable = true;
    }

    #region --- Properties ---
    
    public virtual string DisplayName { get; protected set; }
    public virtual Uri Icon { get; protected set; }
    public virtual object ToolTip { get; protected set; }
    public virtual bool IsExecutable { get; protected set; }

    #endregion

    #region --- Methods ---

    public virtual bool CanExecute(object parameter)
    {
      return IsExecutable; //can override at descendents to use "parameter" (else they can set it at constructor based on "item" passed there)
    }

    public virtual void Execute(object parameter)
    {
      //NOP (override at descendents)
    }

    #endregion
    
    #region --- Events ---
    
    public event System.EventHandler CanExecuteChanged;

    #endregion
  }

}
