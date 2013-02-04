//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IActivity.cs
//Version: 20130204

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class IActivityProperties
  {
    public const string PropertySource = "Source";
    public const string PropertyTime = "Time";
    public const string PropertyCaptions = "Captions";
    public const string PropertyViewPosition = "ViewPosition";
    public const string PropertyViewWidth = "ViewWidth";
    public const string PropertyViewHeight = "ViewHeight";
    public const string PropertyContentZoom = "ContentZoom";
    public const string PropertyContentZoomable = "ContentZoomable";
    public const string PropertyContentZoomToFit = "ContentZoomToFit";
    public const string PropertyContentPartsConfigurable = "ContentPartsConfigurable";
    public const string PropertyToolbarVisible = "ToolbarVisible";

    //TODO: maybe also add WindowsMoveable/WindowsResizable/WindowZoomable to override child settings
  }
 
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
