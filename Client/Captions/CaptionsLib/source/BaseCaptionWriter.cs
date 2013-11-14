//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseCaptionWriter.cs
//Version: 20131113

using ClipFlair.CaptionsLib.Utils;
using ClipFlair.CaptionsLib.Models;

using System.IO;
using System.Text;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace ClipFlair.CaptionsLib
{

	public abstract class BaseCaptionWriter : ICaptionsWriter
	{

		#region --- Methods ---

		public void WriteCaptions(CaptionRegion captions, string path, Encoding theEncoding)
		{
			using (StreamWriter writer = new StreamWriter(path, false, theEncoding)) {
				//the Using statement will close the file created when finished
				WriteCaptions(captions, writer);
			}
		}

		public void WriteCaptions(CaptionRegion captions, Stream stream, Encoding theEncoding)
		{
			StreamWriter writer = new StreamWriter(stream, theEncoding);
			//not Using statement, do not close the stream when finished
			WriteCaptions(captions, writer);
		}

		public void WriteCaptions(CaptionRegion captions, TextWriter writer)
		{
			writer.NewLine =  StringUtils.vbCrLf;
			WriteHeader(writer);
			foreach (CaptionElement c in captions.Children) {
				WriteCaption(c, writer);
			}
			WriteFooter(writer);
			writer.Flush();
			//write out any buffered data
		}

		public virtual void WriteHeader(TextWriter writer)
		{
			//can override at descendents
		}

		public abstract void WriteCaption(CaptionElement caption, TextWriter writer);

		public virtual void WriteFooter(TextWriter writer)
		{
			//can override at descendents
		}

		#endregion

	}

}