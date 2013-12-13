//Filename: TTSReader.cs
//Version: 20131105

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.IO;

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