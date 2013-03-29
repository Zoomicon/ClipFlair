//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageWindowFactory.cs
//Version: 20130326

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
