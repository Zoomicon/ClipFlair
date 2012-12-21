//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridWindowFactory.cs
//Version: 2012121

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.Captions
{

  [Export("ClipFlair.Windows.Views.CaptionsGridView", typeof(IWindowFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class CaptionsGridWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new CaptionsGridWindow();
    }
  }

}
