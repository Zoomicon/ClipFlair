//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsWindowFactory.cs
//Version: 20150525

using System.ComponentModel.Composition;
using System.IO;

namespace ClipFlair.Windows.Captions
{

  //Supported file extensions
  [Export(".SRT", typeof(IFileWindowFactory))]
  [Export(".TTS", typeof(IFileWindowFactory))]
  //[Export(".FAB", typeof(IFileWindowFactory))]
  //[Export(".ENC", typeof(IFileWindowFactory))]
  [Export(".SSA", typeof(IFileWindowFactory))]
  //Supported views
  [Export("ClipFlair.Windows.Views.CaptionsGridView", typeof(IWindowFactory))]
  //MEF creation policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class CaptionsWindowFactory : IFileWindowFactory
  {

    public const string LOAD_FILTER =  "Subtitle files (*.srt, *.tts, *.ssa)|*.srt;*.tts;*.ssa|SRT files (*.srt)|*.srt|TTS files (*.tts)|*.tts|SSA files (*.ssa)|*.ssa";

    public static string[] SUPPORTED_FILE_EXTENSIONS = new string[] { ".SRT", ".TTS", /*".FAB", ".ENC", */ ".SSA" };

    public string[] SupportedFileExtensions()
    {
      return SUPPORTED_FILE_EXTENSIONS;
    }

    public BaseWindow CreateWindow()
    {
      return new CaptionsWindow();
    }

  }

}
