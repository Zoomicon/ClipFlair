//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridWindow.xaml.cs
//Version: 20140515

//TODO: add Source property to CaptionsGrid control and use data-binding to bind it to CaptionsGridView's Source property

using ClipFlair.CaptionsGrid;
using ClipFlair.Windows.Captions;
using ClipFlair.Windows.Views;
using Ionic.Zip;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.IO;
using Utils.Extensions;

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

    #region Load / Save

    public override string LoadFilter
    {
      get
      {
        return base.LoadFilter + "|" + CaptionsGridWindowFactory.LOAD_FILTER;
      }
    }

    public override void LoadOptions(FileInfo f)
    {
      if (!f.Name.EndsWith(new string[] { CLIPFLAIR_EXTENSION, CLIPFLAIR_ZIP_EXTENSION }))
        gridCaptions.LoadCaptions(new CaptionRegion(), f);
      else
        base.LoadOptions(f);
    }

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder);

      CaptionRegion newCaptions = new CaptionRegion();
      LoadCaptions(newCaptions, zip, zipFolder);
      LoadAudio(newCaptions, zip, zipFolder);

      CaptionsGridView.Captions = newCaptions;
    }

    private void LoadCaptions(CaptionRegion captions, ZipFile zip, string zipFolder = "")
    {
      ZipEntry captionsEntry = zip[zipFolder + "/" + DEFAULT_CAPTIONS];
      if (captionsEntry == null)
        captionsEntry = zip[zipFolder + "/" + ALTERNATIVE_CAPTIONS]; //if one of SRT/TTS not found, look for the other type

      if (captionsEntry != null)
        gridCaptions.LoadCaptions(captions, captionsEntry.OpenReader(), captionsEntry.FileName);
    }
    
    public void LoadAudio(CaptionRegion captions, ZipFile zip, string zipFolder = "")
    {
      //load any audio associated to each caption
      foreach (CaptionElement caption in captions.Children)
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

      //save captions
      zip.AddEntry(zipFolder + "/" + DEFAULT_CAPTIONS, SaveCaptions); //save captions //saving even when no captions are available as a placeholder for user to edit manually

      //save revoicing audio
      //if (CaptionsGridView.AudioVisible || CaptionsGridView.SaveInvisibleAudio) //TODO: removed, need to fix this (maybe with separate Audio property or something?), since currently Captions is synced between components and if only some save the audio, it may be lost at load, depending on the load order of those components by their parent (activity)
      SaveAudio(CaptionsGridView.Captions, zip, zipFolder); //...maybe if captions property is bound to a parent (activity), save the captions/audio there once
    }

    public void SaveCaptions(string entryName, Stream stream)
    {
      gridCaptions.SaveCaptions(stream, entryName);
    }

    public void SaveAudio(CaptionRegion captions, ZipFile zip, string zipFolder = "")
    {
      foreach (CaptionElement caption in captions.Children) //save any audio associated to each caption
        SaveAudio(caption, zip, zipFolder);
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

    public void LoadCaptions(Stream stream, string filename) //doesn't close stream
    {
      gridCaptions.LoadCaptions(new CaptionRegion(), stream, filename);
    }

    #endregion

  }

}