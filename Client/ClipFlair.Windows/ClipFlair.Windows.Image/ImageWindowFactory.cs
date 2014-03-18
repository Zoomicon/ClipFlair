//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageWindowFactory.cs
//Version: 20140318

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.Image
{

  //Supported views
  [Export("ClipFlair.Windows.Views.ImageView", typeof(IWindowFactory))]
  //MEF creation Policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class ImageWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new ImageWindow();
    }
  }

}
