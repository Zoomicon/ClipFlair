//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ICaptionsReader.cs
//Version: 20131105

using System.IO;
using System.Text;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace ClipFlair.CaptionsLib.Models
{

	public interface ICaptionsReader
	{

		//not clearing any existing captions, just adding new ones (the CaptionRegion object can choose whether it will sort the Captions by start time or not after the appending)
		void ReadCaptions<T>(CaptionRegion captions, string path, Encoding theEncoding) where T : CaptionElement, new();
		void ReadCaptions<T>(CaptionRegion captions, Stream stream, Encoding theEncoding) where T : CaptionElement, new();

		void ReadCaptions<T>(CaptionRegion captions, TextReader reader) where T : CaptionElement, new();
		void ReadHeader(TextReader reader);
		void ReadCaption(CaptionElement caption, TextReader reader);

		void ReadFooter(TextReader reader);
	}

}