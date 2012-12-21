//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridWindowFactory.cs
//Version: 2012121

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.Image
{

  [Export("ClipFlair.Windows.Views.ImageView", typeof(IWindowFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class ImageWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new ImageWindow();
    }
  }

}
