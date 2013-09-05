//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: InfoCommand.cs
//Version: 20130905

using Utils.Extensions;
using System;
using System.Collections;
using System.Windows.Controls.Pivot;

namespace ClipFlair.Windows.Gallery.Commands
{

  public class InfoCommand : BaseCommand
  {

    public InfoCommand(PivotViewerItem item) : base()
    {
      Icon = new System.Uri("/ClipFlair.Windows.Gallery;component/Images/Info.png", UriKind.Relative); //must specify that this is a relative Uri
      ToolTip = makeTooltip(item);
      IsExecutable = true; //needed to show the tooltip
    }

    protected string makeTooltip(PivotViewerItem item)
    {
      IList nameData = item["Name"];
      string name = (nameData != null && nameData.Count > 0)? (string)nameData[0] : "";
      if (name == null)
        name = ""; //just to be safe

      IList descriptionData = item["Description"];
      string description = (descriptionData != null && descriptionData.Count > 0) ? (string)descriptionData[0] : "";
      if (description == null)
        description = ""; //just to be safe
      
      return name +
             ((name.IsEmpty() || description.IsEmpty())? "" : "\n\n") + 
             description;
    }

    public override void Execute(object parameter)
    {
      //TODO: toggle visibility of Details pane (may need to pass pivot viewer itself at constructor or a reference to the details pane)
    }

  }

}
