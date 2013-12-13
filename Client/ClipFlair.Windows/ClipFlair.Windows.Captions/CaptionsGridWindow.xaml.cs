//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridWindow.xaml.cs
//Version: 20130606

//TODO: add Source property to CaptionsGrid control and use data-binding to bind it to CaptionsGridView's Source property

using ClipFlair.CaptionsGrid;
using ClipFlair.Windows.Views;
using Ionic.Zip;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.IO;

namespace ClipFlair.Windows
{

  public partial class CaptionsGridWindow : BaseWindow
  {

    public const string DEFAULT_CAPTIONS = "captions.srt";
    public const string ALTERNATIVE_CAPTIONS = "captions.tts";

    public CaptionsGridWindow()
    {
      View = new CaptionsGridView(); //must set the view first
      InitializeComponent();
    }

    #region --- Properties ---

    public ICaptionsGrid CaptionsGridView
    {
      get { return (ICaptionsGrid)View; }
      set { View = value; }
    }
    
    #endregion

    #region Load / Save Options

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder);

      //load captions
      ZipEntry captionsEntry = zip[zipFolder + "/" + DEFAULT_CAPTIONS];
      if (captionsEntry == null) captionsEntry = zip[zipFolder + "/" + ALTERNATIVE_CAPTIONS]; //if one of SRT/TTS not found, look for the other type
      if (captionsEntry != null) 
      {
        gridCaptions.LoadCaptions(captionsEntry.OpenReader(), captionsEntry.FileName);
        CaptionsGridView.Captions = gridCaptions.Captions; //TODO: this is temprorary till it is found out why the binding in the XAML between grid and the window doesn't work (may be cause of the findancestor in the binding - probably need to set the binding in code)
      } else 
        CaptionsGridView.Captions = new CaptionRegion();

      //load any audio associated to each caption
      foreach (CaptionElement caption in CaptionsGridView.Captions.Children)
        LoadAudio(caption, zip, zipFolder);
    }

    public void LoadAudio(CaptionElement caption, ZipFile zip, string zipFolder = "")
    {
      ZipEntry entry = zip[zipFolder + AudioFilename(caption)];
      if (entry != null)
        CaptionsGrid.CaptionsGrid.LoadAudio(caption, entry.OpenReader());
    }

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);

      zip.AddEntry(zipFolder + "/" + DEFAULT_CAPTIONS, SaveCaptions); //save captions //saving even when no captions are available as a placeholder for user to edit manually

      foreach (CaptionElement caption in CaptionsGridView.Captions.Children) //save any audio associated to each caption
        SaveAudio(caption, zip, zipFolder);
    }

    public void SaveCaptions(string entryName, Stream stream)
    {
      gridCaptions.SaveCaptions(stream, entryName);
    }

    public void SaveAudio(CaptionElement caption, ZipFile zip, string zipFolder = "")
    {
      if ((caption != null) && caption.HasAudio())
        zip.AddEntry(
          zipFolder + AudioFilename(caption),
          new WriteDelegate((entryName, stream) => { CaptionsGrid.CaptionsGrid.SaveAudio(caption, stream); }) );
    }

    private string AudioFilename(CaptionElement caption)
    {
      string startTime = caption.BeginText ?? "00:00:00"; //if null using "00:00:00"
      string endTime = caption.EndText ?? "00:00:00"; //if null using "00:00:00"
      return "/Audio/" + startTime  + "-" + endTime + ".wav";
    }

    #endregion

  }

}