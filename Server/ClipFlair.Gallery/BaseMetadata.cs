//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseMetadtata.cs
//Version: 20130719

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ClipFlair.Gallery
{

  public abstract class BaseMetadata : IMetadata
  {

    public IMetadata Load(string key, string cxmlFilename, string cxmlFallbackFilename)
    {
      XDocument doc = null;
      try
      {
        doc = XDocument.Load(cxmlFilename);
      }
      catch
      {
        try
        {
          doc = XDocument.Load(cxmlFallbackFilename);
        }
        catch
        {
          //NOP
        }
      }

      return Load(key, doc); //Load should return default metadata when doc==null
    }

    public abstract void Clear();
    public abstract IMetadata Load(string key, XDocument doc);
    public abstract void Save(string cxmlFilename);

  }

}