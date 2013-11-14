//Filename: TTSReader.cs
//Version: 20131105

using System.IO;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace ClipFlair.CaptionsLib.TTS
{

	public class TTSReader : BaseCaptionReader
	{

		public override void ReadCaption(CaptionElement caption, TextReader reader)
		{
			TTSUtils.TTSStringToCaption(reader.ReadLine(), caption);
		}

	}

}