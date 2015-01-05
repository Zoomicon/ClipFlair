//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ICaptionsWriter.cs
//Version: 20131105

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.IO;
using System.Text;

namespace ClipFlair.CaptionsLib.Models
{

	public interface ICaptionsWriter
	{

		void WriteCaptions(CaptionRegion captions, string path, Encoding theEncoding);
		void WriteCaptions(CaptionRegion captions, Stream stream, Encoding theEncoding);

		void WriteCaptions(CaptionRegion captions, TextWriter writer);
		void WriteHeader(TextWriter writer);
		void WriteCaption(CaptionElement caption, TextWriter writer);

		void WriteFooter(TextWriter writer);
	}

}