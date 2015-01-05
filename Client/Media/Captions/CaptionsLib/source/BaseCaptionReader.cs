//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseCaptionReader.cs
//Version: 20140809

using ClipFlair.CaptionsLib.Models;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.IO;
using System.Text;

namespace ClipFlair.CaptionsLib
{

  public abstract class BaseCaptionReader : ICaptionsReader
  {

    #region --- Methods ---

    public void ReadCaptions<T>(CaptionRegion captions, string path, Encoding theEncoding) where T : CaptionElement, new()
    {
      using (StreamReader reader = new StreamReader(path, theEncoding, true)) //not clearing any existing captions, just appending to the end (the CaptionRegion object can choose whether it will sort the Captions by start time or not after the appending)
        ReadCaptions<T>(captions, reader); //the Using statement will close the file created when finished
    }

    public void ReadCaptions<T>(CaptionRegion captions, Stream stream, Encoding theEncoding) where T : CaptionElement, new()
    {
      StreamReader reader = new StreamReader(stream, theEncoding, true); //not clearing any existing captions, just appending to the end (the CaptionRegion object can choose whether it will sort the Captions by start time or not after the appending)
      ReadCaptions<T>(captions, reader); //no Using statement, do not close the stream when finished
    }

    public void ReadCaptions<T>(CaptionRegion captions, TextReader reader) where T : CaptionElement, new()
    {
      ReadHeader(reader);
      int index = 0;
      while (reader.Peek() != -1) {
        T caption = new T();
        ReadCaption(caption, reader);
        caption.Index = ++index; //prefix increment: first increment, then evaluate (first caption will have Index=1)
        captions.Children.Add(caption);
      }
      ReadFooter(reader);
    }

    public virtual void ReadHeader(TextReader reader)
    {
      //can override at descendents
    }

    public abstract void ReadCaption(CaptionElement caption, TextReader reader);

    public virtual void ReadFooter(TextReader reader)
    {
      //can override at descendents
    }

    #endregion

  }

}