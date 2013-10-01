//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IViewFactory.cs
//Version: 20131001

using System.ComponentModel;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public interface IViewFactory
  {
    IView CreateView();
  }

}
