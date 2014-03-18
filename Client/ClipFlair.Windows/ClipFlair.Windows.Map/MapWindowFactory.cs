//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MapWindowFactory.cs
//Version: 20140318

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.Map
{

  //Supported views
  [Export("ClipFlair.Windows.Views.MapView", typeof(IWindowFactory))]
  //MEF creation policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class MapWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new MapWindow();
    }
  }

}
