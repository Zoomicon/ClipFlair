//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: GalleryWindowFactory.cs
//Version: 20130326

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.Gallery
{

  [Export("ClipFlair.Windows.Views.GalleryView", typeof(IWindowFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class GalleryWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new GalleryWindow();
    }
  }

}
