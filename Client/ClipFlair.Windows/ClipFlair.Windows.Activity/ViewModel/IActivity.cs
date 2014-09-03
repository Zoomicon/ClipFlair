//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IActivity.cs
//Version: 20140903

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.Windows;
using System.Windows.Controls;

namespace ClipFlair.Windows.Views
{
 
  public interface IActivity: IView
  {
    Uri Source { get; set; }
    CaptionRegion Captions { get; set; }
    Point ViewPosition { get; set; }
    double ViewWidth { get; set; }
    double ViewHeight { get; set; }
    double ContentZoom { get; set; }
    bool ContentZoomable { get; set; }
    bool ContentZoomToFit { get; set; }
    bool ContentPartsConfigurable { get; set; }
    bool IconbarVisible { get; set; } //20140903
    bool ToolbarVisible { get; set; }
    Orientation ToolbarOrientation { get; set; } //20140901
  }

}
