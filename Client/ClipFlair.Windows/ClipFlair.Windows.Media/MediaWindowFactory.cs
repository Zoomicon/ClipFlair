//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridWindowFactory.cs
//Version: 2012121

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.Media
{

  [Export("ClipFlair.Windows.Views.MediaPlayerView", typeof(IWindowFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class MediaPlayerWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new MediaPlayerWindow();
    }
  }

}
