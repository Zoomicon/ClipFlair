//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: InfoCommand.cs
//Version: 20130828

using System.Windows.Controls.Pivot;

namespace ClipFlair.Windows.Gallery.Commands
{

  public class InfoCommand : BaseCommand
  {

    public InfoCommand(PivotViewerItem item) : base()
    {
      DisplayName = "i";
      Icon = null; //new System.Uri("/ClipFlair.Windows.Gallery;component/Images/Info.png";
      ToolTip = makeTooltip(item);
      IsExecutable = true; //needed to show the tooltip
    }

    protected string makeTooltip(PivotViewerItem item)
    {
      string name = (string)item["Name"][0];
      string description = (string)item["Description"][0];
      return ((name != null)? name + ((description != null)? " - " : "") : "") + 
             ((description != null)? description : "");
    }

    public override void Execute(object parameter)
    {
      base.Execute(parameter);
      //TODO: toggle visibility of Details pane (may need to pass pivot viewer itself at constructor or a reference to the details pane)
    }

  }

}
