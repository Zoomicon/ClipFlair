//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionExtraDataHelper.cs
//Version: 20140707

#define WRITE_FORMATTED_XML

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace ClipFlair.CaptionsGrid
{

  /// <summary>
  /// Extra Data extensions class for CaptionElementExt
  /// </summary>
  public static class CaptionElementExt_ExtraData
  {

    #if WRITE_FORMATTED_XML
    private static XmlWriterSettings XML_WRITER_SETTINGS = new XmlWriterSettings() { Indent = true, IndentChars = "  " };
    #endif

    public static bool HasExtraData(this CaptionElement caption)
    {
      CaptionElementExt c = caption as CaptionElementExt;
      return (c != null) ? (string.IsNullOrWhiteSpace(c.Comments) || c.RTL) : false; //not saving empty or plain whitespace comments
    }

    public static CaptionElementExtraData GetExtraData(this CaptionElement caption)
    {
      CaptionElementExt c = caption as CaptionElementExt;
      return (c != null) ? c.ExtraData : null;
    }

    public static void LoadExtraData(this CaptionElement caption, Stream stream) //does not close stream
    {
      CaptionElementExt captionExt = caption as CaptionElementExt;
      if (captionExt == null) return;

      MemoryStream buffer = new MemoryStream();

      DataContractSerializer serializer = new DataContractSerializer(typeof(CaptionElementExtraData));
      captionExt.ExtraData = (CaptionElementExtraData)serializer.ReadObject(stream); //this will set a new View that defaults to Busy=false
    }

    public static void SaveExtraData(this CaptionElement caption, Stream stream) //does not close stream
    {
      CaptionElementExt captionExt = caption as CaptionElementExt;
      if (captionExt == null) return;

      DataContractSerializer serializer = new DataContractSerializer(typeof(CaptionElementExtraData));
      #if WRITE_FORMATTED_XML
      using (XmlWriter writer = XmlWriter.Create(stream, XML_WRITER_SETTINGS))
        serializer.WriteObject(writer, captionExt.ExtraData);
      #else
      serializer.WriteObject(stream, captionExt.ExtraData);
      #endif
    }

  }

}
