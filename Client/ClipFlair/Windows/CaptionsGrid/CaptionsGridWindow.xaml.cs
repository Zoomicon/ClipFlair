//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridWindow.xaml.cs
//Version: 20121115

//TODO: add Source property to CaptionsGrid control and use data-binding to bind it to CaptionsGridView's Source property

using ClipFlair.Windows.Views;

using Ionic.Zip;

using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace ClipFlair.Windows
{
  public partial class CaptionsGridWindow : BaseWindow
  {

    public CaptionsGridWindow()
    {
      View = new CaptionsGridView(); //must set the view first
      InitializeComponent();
      InitializeView();
    }

    #region --- Properties ---

    #region View

    public new ICaptionsGrid View //hiding parent property
    {
      get { return (ICaptionsGrid)base.View; } //delegating to parent property
      set { base.View = value; }
    }

    protected override void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.View_PropertyChanged(sender, e);

      if (e.PropertyName == null) //multiple (not specified) properties have changed, consider all as changed
      {
        Time = View.Time;
        //...
      }
      else switch (e.PropertyName) //string equality check in .NET uses ordinal (binary) comparison semantics by default
        {
          case ICaptionsGridProperties.PropertyTime:
            Time = View.Time;
            break;
          //...
        }
    }

    #endregion
    
    #region Time

    /// <summary>
    /// Time Dependency Property
    /// </summary>
    public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register(ICaptionsGridProperties.PropertyTime, typeof(TimeSpan), typeof(CaptionsGridWindow),
            new FrameworkPropertyMetadata(ICaptionsGridDefaults.DefaultTime, new PropertyChangedCallback(OnTimeChanged)));

    /// <summary>
    /// Gets or sets the Time property.
    /// </summary>
    public TimeSpan Time
    {
      get { return (TimeSpan)GetValue(TimeProperty); }
      set { SetValue(TimeProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Time property.
    /// </summary>
    private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGridWindow target = (CaptionsGridWindow)d;
      TimeSpan oldTime = (TimeSpan)e.OldValue;
      TimeSpan newTime = target.Time;
      target.OnTimeChanged(oldTime, newTime);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Time property.
    /// </summary>
    protected virtual void OnTimeChanged(TimeSpan oldTime, TimeSpan newTime)
    {
      View.Time = newTime;
    }

    #endregion

    #region Captions

    /// <summary>
    /// Captions Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionsProperty =
        DependencyProperty.Register("Captions", typeof(CaptionRegion), typeof(CaptionsGridWindow),
            new FrameworkPropertyMetadata(new CaptionRegion(), new PropertyChangedCallback(OnCaptionsChanged)));

    /// <summary>
    /// Gets or sets the Captions property.
    /// </summary>
    public CaptionRegion Captions
    {
      get { return (CaptionRegion)GetValue(CaptionsProperty); }
      set { SetValue(CaptionsProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Captions property.
    /// </summary>
    private static void OnCaptionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGridWindow target = (CaptionsGridWindow)d;
      CaptionRegion oldCaptions = (CaptionRegion)e.OldValue;
      CaptionRegion newCaptions = target.Captions;
      target.OnCaptionsChanged(oldCaptions, newCaptions);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Captions property.
    /// </summary>
    protected virtual void OnCaptionsChanged(CaptionRegion oldCaptions, CaptionRegion newCaptions)
    {
      //NOP (using two-way data binding)
      //gridCaptions.Captions = newCaptions;
    }

    #endregion

    #endregion

    #region Load / Save Options

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder);
      gridCaptions.ReadCaptions(zip[zipFolder + "/captions.srt"].OpenReader(), "captions.srt");
      Captions = gridCaptions.Captions; //TODO: this is temprorary till it is found out why the binding in the XAML between grid and the window doesn't work (may be cause of the findancestor in the binding - probably need to set the binding in code)
    }

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);

      MemoryStream stream = new MemoryStream(); //TODO: not optimal implementation, should try to pipe streams without first saving into memory
      gridCaptions.WriteCaptions(stream, "captions.srt");
      stream.Position = 0;
      zip.AddEntry(zipFolder + "/captions.srt", stream);
    }

    #endregion

  }

}