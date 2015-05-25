//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionUtils.cs
//Version: 20150525

using ClipFlair.CaptionsLib.Encore;
using ClipFlair.CaptionsLib.FAB;
using ClipFlair.CaptionsLib.Models;
using ClipFlair.CaptionsLib.SRT;
using ClipFlair.CaptionsLib.SSA;
using ClipFlair.CaptionsLib.TTS;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.Text;

namespace ClipFlair.CaptionsLib.Utils
{

  public static class CaptionUtils
  {

    public const string EXTENSION_TTS = ".TTS";
    public const string EXTENSION_SRT = ".SRT";
    public const string EXTENSION_FAB = ".FAB";    //Note: Adobe ENCORE uses .TXT file extension for this
    public const string EXTENSION_ENCORE = ".ENC"; //Note: Adobe ENCORE uses .TXT file extension for this
    public const string EXTENSION_SSA = ".SSA";

    public static readonly string[] EXTENSIONS_TTS = { EXTENSION_TTS };
    public static readonly string[] EXTENSIONS_SRT = { EXTENSION_SRT };
    public static readonly string[] EXTENSIONS_FAB = { EXTENSION_FAB };
    public static readonly string[] EXTENSIONS_ENCORE = { EXTENSION_ENCORE };
    public static readonly string[] EXTENSIONS_SSA = { EXTENSION_SSA };

    //TODO: use MEF here to instantiate readers and writers

    public static ICaptionsReader GetCaptionsReader(string path)
    {
      if (FileUtils.CheckExtension(path, EXTENSIONS_TTS) != null)
        return new TTSReader();
      else if (FileUtils.CheckExtension(path, EXTENSIONS_SRT) != null)
        return new SRTReader();
      else if (FileUtils.CheckExtension(path, EXTENSIONS_SSA) != null)
        return new SSAReader();
      return null;
    } 
    
    public static ICaptionsWriter GetCaptionsWriter(string path)
    {
      if (FileUtils.CheckExtension(path, EXTENSIONS_TTS) != null)
        return new TTSWriter();
      else if (FileUtils.CheckExtension(path, EXTENSIONS_SRT) != null)
        return new SRTWriter();
      else if (FileUtils.CheckExtension(path, EXTENSIONS_FAB) != null)
        return new FABWriter();
      else if (FileUtils.CheckExtension(path, EXTENSIONS_ENCORE) != null)
        return new EncoreWriter();
      else if (FileUtils.CheckExtension(path, EXTENSIONS_SSA) != null)
        return new SSAWriter();
      else
      return null;
    }

    public static void ReadCaptions<T>(CaptionRegion captions, string path, Encoding theEncoding) where T : CaptionElement, new()
    {
      ICaptionsReader reader = GetCaptionsReader(path);
      if (reader != null)
        reader.ReadCaptions<T>(captions, path, theEncoding);
    }

    public static void WriteCaptions(CaptionRegion captions, string path, Encoding theEncoding)
    {
      ICaptionsWriter writer = GetCaptionsWriter(path);
      if (writer != null)
        writer.WriteCaptions(captions, path, theEncoding);
    }

  }

}