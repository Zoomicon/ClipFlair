//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: NewsWindowFactory.cs
//Version: 20130326

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.News
{

  [Export("ClipFlair.Windows.Views.NewsView", typeof(IWindowFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class NewsWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new NewsWindow();
    }
  }

}
