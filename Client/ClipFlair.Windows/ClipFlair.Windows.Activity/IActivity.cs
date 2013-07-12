//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IActivity.cs
//Version: 20130710

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{
 
  public interface IActivity: IView
  {
    Uri Source { get; set; }
    TimeSpan Time { get; set; }
    CaptionRegion Captions { get; set; }
    Point ViewPosition { get; set; }
    double ViewWidth { get; set; }
    double ViewHeight { get; set; }
    double ContentZoom { get; set; }
    bool ContentZoomable { get; set; }
    bool ContentZoomToFit { get; set; }
    bool ContentPartsConfigurable { get; set; }
    bool ToolbarVisible { get; set; }
  }

}
