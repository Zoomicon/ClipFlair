//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityWindowFactory.cs
//Version: 2012121

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.Activity
{

  [Export("ClipFlair.Windows.Views.ActivityView", typeof(IWindowFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class ActivityWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new ActivityWindow();
    }
  }

}
