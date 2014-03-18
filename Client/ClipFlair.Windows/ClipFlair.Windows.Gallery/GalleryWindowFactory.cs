//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: GalleryWindowFactory.cs
//Version: 20140318

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.Gallery
{

  //Supported views
  [Export("ClipFlair.Windows.Views.GalleryView", typeof(IWindowFactory))]
  //MEF creation policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class GalleryWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new GalleryWindow();
    }
  }

}
