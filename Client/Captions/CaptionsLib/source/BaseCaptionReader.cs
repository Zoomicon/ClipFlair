//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseCaptionReader.cs
//Version: 20131105

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
			//not clearing any existing captions, just appending to the end (the CaptionRegion object can choose whether it will sort the Captions by start time or not after the appending)
			using (StreamReader reader = new StreamReader(path, theEncoding, true)) {
				//the Using statement will close the file created when finished
				ReadCaptions<T>(captions, reader);
			}
		}

		public void ReadCaptions<T>(CaptionRegion captions, Stream stream, Encoding theEncoding) where T : CaptionElement, new()
		{
			//not clearing any existing captions, just appending to the end (the CaptionRegion object can choose whether it will sort the Captions by start time or not after the appending)
			StreamReader reader = new StreamReader(stream, theEncoding, true);
			//no Using statement, do not close the stream when finished
			ReadCaptions<T>(captions, reader);
		}

		public void ReadCaptions<T>(CaptionRegion captions, TextReader reader) where T : CaptionElement, new()
		{
			ReadHeader(reader);
			while (reader.Peek() != -1) {
				T caption = new T();
				ReadCaption(caption, reader);
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