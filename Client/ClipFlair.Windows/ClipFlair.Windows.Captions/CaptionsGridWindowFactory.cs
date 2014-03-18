﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridWindowFactory.cs
//Version: 20140318

using System.ComponentModel.Composition;
using System.IO;

namespace ClipFlair.Windows.Captions
{

  //Supported file extensions
  [Export(".SRT", typeof(IFileWindowFactory))]
  [Export(".TTS", typeof(IFileWindowFactory))]
  [Export(".FAB", typeof(IFileWindowFactory))]
  [Export(".ENC", typeof(IFileWindowFactory))]
  //Supported views
  [Export("ClipFlair.Windows.Views.CaptionsGridView", typeof(IWindowFactory))]
  //MEF creation policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class CaptionsGridWindowFactory : IFileWindowFactory
  {

    public const string LOAD_FILTER =  "Subtitle files (*.srt, *.tts)|*.srt;*.tts|SRT files (*.srt)|*.srt|TTS files (*.tts)|*.tts";

    private static string[] SUPPORTED_FILE_EXTENSIONS = new string[] { ".SRT", ".TTS", ".FAB", ".ENC" };

    public string[] SupportedFileExtensions()
    {
      return SUPPORTED_FILE_EXTENSIONS;
    }

    public BaseWindow CreateWindow()
    {
      return new CaptionsGridWindow();
    }

    public BaseWindow CreateWindow(string filename, Stream stream)
    {
      CaptionsGridWindow window = new CaptionsGridWindow();
      window.LoadCaptions(stream, filename);
      return window;
    }

  }

}
