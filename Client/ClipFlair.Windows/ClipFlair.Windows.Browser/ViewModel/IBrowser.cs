//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IBrowser.cs
//Version: 20140415

using System;

namespace ClipFlair.Windows.Views
{

  public interface IBrowser : IView
  {
    Uri Source { get; set; }
  }

}
