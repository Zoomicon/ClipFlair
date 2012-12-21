//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridWindowFactory.cs
//Version: 2012121

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.Map
{

  [Export("ClipFlair.Windows.Views.MapView", typeof(IWindowFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class MapWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new MapWindow();
    }
  }

}
