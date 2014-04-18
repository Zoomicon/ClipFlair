//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BrowserWindowFactory.cs
//Version: 20140415

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.Browser
{

  //Supported views
  [Export("ClipFlair.Windows.Views.BrowserView", typeof(IWindowFactory))]
  //MEF creation policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class BrowserWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new BrowserWindow();
    }
  }

}
