//Filename: TTSWriter.cs
//Version: 20131105

using System.IO;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace ClipFlair.CaptionsLib.TTS
{

	public class TTSWriter : BaseCaptionWriter
	{

		#region --- Methods ---

		public override void WriteCaption(CaptionElement caption, TextWriter writer)
		{
			writer.WriteLine(TTSUtils.CaptionToTTSString(caption));
		}

		#endregion

	}

}