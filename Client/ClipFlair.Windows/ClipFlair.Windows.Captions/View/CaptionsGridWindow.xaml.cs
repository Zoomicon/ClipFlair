//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridWindow.xaml.cs
//Version: 20140707

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

    #region --- LoadOptions dialog ---

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
      {
        gridCaptions.LoadCaptions(f);
        CaptionsGridView.Captions = gridCaptions.Captions; //TODO: see why this is needed (two-way data-binding doesn't seem to work?)
      }
      else
        base.LoadOptions(f);
    }

    #endregion

    #region --- Load captions file from stream ---
    
    public override void LoadContent(Stream stream, string filename) //doesn't close stream
    {
      gridCaptions.LoadCaptions(new CaptionRegion(), stream, filename);
      CaptionsGridView.Captions = gridCaptions.Captions; //TODO: see why this is needed (two-way data-binding doesn't seem to work?)
    }
    
    #endregion

    #region --- Caption specific file paths for saved state ---

    private string CaptionSpecificPath(CaptionElement caption, string pathPrefix, string pathSuffix)
    {
      string startTime = caption.BeginText ?? "00:00:00"; //if null using "00:00:00"
      string endTime = caption.EndText ?? "00:00:00"; //if null using "00:00:00"
      return pathPrefix + startTime + "-" + endTime + pathSuffix;
    }

    private string CaptionExtraDataPath(CaptionElement caption)
    {
      return CaptionSpecificPath(caption, "/ExtraData/", ".xml");
    }
    
    private string CaptionAudioPath(CaptionElement caption)
    {
      return CaptionSpecificPath(caption, "/Audio/", ".wav"); //TODO: need to update this if storing to formats other than WAV in the future
    }

    private string CaptionCommentsAudioPath(CaptionElement caption)
    {
      return CaptionSpecificPath(caption, "/CommentsAudio/", ".wav"); //TODO: need to update this if storing to formats other than WAV in the future
    }
    
    #endregion

    #region --- Load saved state ---

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder);

      CaptionRegion newCaptions = new CaptionRegion();

      //load captions
      LoadCaptions(newCaptions, zip, zipFolder);
      LoadCaptionsExtraData(newCaptions, zip, zipFolder); //load Comments, RTL, etc. fields that can't be stored in SRT file

      //load revoicing audio
      LoadCaptionsAudio(newCaptions, zip, zipFolder);

      //load comments audio
      LoadCaptionsCommentsAudio(newCaptions, zip, zipFolder);

      CaptionsGridView.Captions = newCaptions; //TODO: see why this is needed (two-way data-binding doesn't seem to work?)
    }
 
    private void LoadCaptions(CaptionRegion captions, ZipFile zip, string zipFolder = "")
    {
      foreach (string ext in CaptionsGridWindowFactory.SUPPORTED_FILE_EXTENSIONS)
        foreach (ZipEntry captionsEntry in zip.SelectEntries("*" + ext, zipFolder))
          using (Stream zipStream = captionsEntry.OpenReader()) //closing stream when done
            gridCaptions.LoadCaptions(captions, zipStream, captionsEntry.FileName); //merge multiple embedded caption files (if user added them by hand to the saved state file, since we save only DEFAULT_CAPTIONS file, not multiple caption files)
    }

    #region Caption Extra Data

    public void LoadCaptionsExtraData(CaptionRegion captions, ZipFile zip, string zipFolder = "")
    {
      //load any caption extra data associated to each caption
      foreach (CaptionElement caption in captions.Children)
        LoadCaptionExtraData(caption, zip, zipFolder);
    }

    public void LoadCaptionExtraData(CaptionElement caption, ZipFile zip, string zipFolder = "")
    {
      ZipEntry entry = zip[zipFolder + CaptionExtraDataPath(caption)];
      if (entry != null)
        caption.LoadExtraData(entry.OpenReader());
    }

    #endregion

    #region Caption Audio

    public void LoadCaptionsAudio(CaptionRegion captions, ZipFile zip, string zipFolder = "")
    {
      //load any audio associated to each caption
      foreach (CaptionElement caption in captions.Children)
        LoadCaptionAudio(caption, zip, zipFolder);
    }

    public void LoadCaptionAudio(CaptionElement caption, ZipFile zip, string zipFolder = "")
    {
      ZipEntry entry = zip[zipFolder + CaptionAudioPath(caption)];
      if (entry != null)
        caption.LoadAudio(entry.OpenReader());
    }

    #endregion

    #region Caption CommentsAudio

    public void LoadCaptionsCommentsAudio(CaptionRegion captions, ZipFile zip, string zipFolder = "")
    {
      //load any CommentsAudio associated to each caption
      foreach (CaptionElement caption in captions.Children)
        LoadCaptionCommentsAudio(caption, zip, zipFolder);
    }

    public void LoadCaptionCommentsAudio(CaptionElement caption, ZipFile zip, string zipFolder = "")
    {
      ZipEntry entry = zip[zipFolder + CaptionCommentsAudioPath(caption)];
      if (entry != null)
        caption.LoadCommentsAudio(entry.OpenReader());
    }

    #endregion

    #endregion

    #region --- Save state ---

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);

      CaptionRegion captions = CaptionsGridView.Captions;

      //save captions
      SaveCaptions(captions, zip, zipFolder);
      SaveCaptionsExtraData(captions, zip, zipFolder);

      //save revoicing audio
      //if (CaptionsGridView.AudioVisible || CaptionsGridView.SaveInvisibleAudio) //TODO: removed, need to fix this (maybe with separate Audio property or something?), since currently Captions is synced between components and if only some save the audio, it may be lost at load, depending on the load order of those components by their parent (activity)
      SaveCaptionsAudio(captions, zip, zipFolder); //...maybe if captions property is bound to a parent (activity), save the captions/audio there once

      //save comments audio
      SaveCaptionsCommentsAudio(captions, zip, zipFolder);
    }

    private void SaveCaptions(CaptionRegion captions, ZipFile zip, string zipFolder = "")
    {
      zip.AddEntry(zipFolder + "/" + DEFAULT_CAPTIONS, SaveCaptions); //SaveCaptions(string, Stream) is callback method //saving even when no captions are available as a placeholder for user to edit manually
    }

    public void SaveCaptions(string entryName, Stream stream) //callback
    {
      gridCaptions.SaveCaptions(stream, entryName);
    }

    #region Caption Extra Data

    public void SaveCaptionsExtraData(CaptionRegion captions, ZipFile zip, string zipFolder = "")
    {
      foreach (CaptionElement caption in captions.Children) //save any ExtraData associated to each caption
        SaveCaptionExtraData(caption, zip, zipFolder);
    }

    public void SaveCaptionExtraData(CaptionElement caption, ZipFile zip, string zipFolder = "")
    {
      if ((caption != null) && caption.HasExtraData())
        zip.AddEntry(
          zipFolder + CaptionExtraDataPath(caption),
          new WriteDelegate((entryName, stream) => { caption.SaveExtraData(stream); }) );
    }

    #endregion
    
    #region Caption Audio

    public void SaveCaptionsAudio(CaptionRegion captions, ZipFile zip, string zipFolder = "")
    {
      foreach (CaptionElement caption in captions.Children) //save any audio associated to each caption
        SaveCaptionAudio(caption, zip, zipFolder);
    }

    public void SaveCaptionAudio(CaptionElement caption, ZipFile zip, string zipFolder = "")
    {
      if ((caption != null) && caption.HasAudio())
        zip.AddEntry(
          zipFolder + CaptionAudioPath(caption),
          new WriteDelegate((entryName, stream) => { caption.SaveAudio(stream); }) );
    }

    #endregion

    #region Caption CommentsAudio

    public void SaveCaptionsCommentsAudio(CaptionRegion captions, ZipFile zip, string zipFolder = "")
    {
      foreach (CaptionElement caption in captions.Children) //save any CommentsAudio associated to each caption
        SaveCaptionCommentsAudio(caption, zip, zipFolder);
    }

    public void SaveCaptionCommentsAudio(CaptionElement caption, ZipFile zip, string zipFolder = "")
    {
      if ((caption != null) && caption.HasCommentsAudio())
        zip.AddEntry(
          zipFolder + CaptionCommentsAudioPath(caption),
          new WriteDelegate((entryName, stream) => { caption.SaveCommentsAudio(stream); }));
    }

    #endregion

    #endregion

  }

}