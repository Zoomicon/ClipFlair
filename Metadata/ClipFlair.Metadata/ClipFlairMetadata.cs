//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ClipFlairMetadtata.cs
//Version: 20130823

using Metadata.CXML;

using System.Xml;

namespace ClipFlair.Metadata
{

  public abstract class ClipFlairMetadata : CXMLMetadata, IClipFlairMetadata
  {

    #region --- Properties ---
   
    //Facets//
    public string Filename { get; set; }
    public string[] Keywords { get; set; }
    public string License { get; set; }

    #endregion

    public override ICXMLMetadata Load(string key, XmlReader cxml, XmlReader cxmlFallback)
    {
      return FixMetadata((IClipFlairMetadata)base.Load(key, cxml, cxmlFallback));
    }

    public static IClipFlairMetadata FixMetadata(IClipFlairMetadata metadata)
    {
      if (string.IsNullOrWhiteSpace(metadata.Title))
        metadata.Title = metadata.Filename;

      return metadata;
    }

    public override void Clear()
    {
      base.Clear();
 
      //Facets//
      Filename = "";
      Keywords = new string[] { };
      License = "";
    }

  }

}