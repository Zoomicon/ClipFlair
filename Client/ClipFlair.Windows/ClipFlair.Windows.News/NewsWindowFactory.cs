//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: NewsWindowFactory.cs
//Version: 20140318

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.News
{

  //Supported views
  [Export("ClipFlair.Windows.Views.NewsView", typeof(IWindowFactory))]
  //MEF creation policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class NewsWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new NewsWindow();
    }
  }

}
