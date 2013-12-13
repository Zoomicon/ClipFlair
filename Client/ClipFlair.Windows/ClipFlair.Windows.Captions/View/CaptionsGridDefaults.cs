//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridDefaults.cs
//Version: 20131213

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class CaptionsGridDefaults
  {

    #region IView defaults - overrides
    
    public const string DefaultTitle = "Captions";
    public const double DefaultWidth = 600;
    public const double DefaultHeight = 400;
    public static readonly Color DefaultBorderColor = Color.FromArgb(0xFF, 0xEC, 0x46, 0x14); //#EC4614
      //Color.FromArgb(0xFF, 0x00, 0xAA, 0xF0); //#00AAF0
      //Color.FromArgb(0xFF, 0x00, 0x83, 0xFF); //#0083FF

    public const double DefaultWidth_Revoicing = 330;
    
    #endregion

    public const Uri DefaultSource = null;
    public const CaptionRegion DefaultCaptions = null; //don't make this "static readonly", it's an object reference, not a struct (better check for "null" in the view and create default instance if needed)
    public const bool DefaultToolbarVisible = true;
    public const bool DefaultStartTimeVisible = true;
    public const bool DefaultEndTimeVisible = true;
    public const bool DefaultDurationVisible = false;
    public const bool DefaultRoleVisible = true;
    public const bool DefaultCaptionVisible = true;
    public const bool DefaultCPSVisible = false;
    public const bool DefaultWPMVisible = false;
    public const bool DefaultAudioVisible = false;
    public const bool DefaultCommentsVisible = false;

    #region Methods

    public static void SetDefaults(ICaptionsGrid view)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(view);
      view.Title = DefaultTitle;
      view.Width = DefaultWidth;
      view.Height = DefaultHeight;
      view.BorderColor = DefaultBorderColor;

      //ICaptionsGrid defaults
      view.Source = DefaultSource;
      view.Captions = DefaultCaptions;
      view.ToolbarVisible = DefaultToolbarVisible;
      view.StartTimeVisible = DefaultStartTimeVisible;
      view.EndTimeVisible = DefaultEndTimeVisible;
      view.DurationVisible = DefaultDurationVisible;
      view.RoleVisible = DefaultRoleVisible;
      view.CaptionVisible = DefaultCaptionVisible;
      view.CPSVisible = DefaultCPSVisible;
      view.WPMVisible = DefaultWPMVisible;
      view.AudioVisible = DefaultAudioVisible;
      view.CommentsVisible = DefaultCommentsVisible;

      //Dirty flag
      view.Dirty = ViewDefaults.DefaultDirty; //must do last - this should be set again at the end of any SetDefaults method (at descendents)
    }

    #endregion

  }

}