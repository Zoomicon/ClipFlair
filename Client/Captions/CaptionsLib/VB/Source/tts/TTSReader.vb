'Filename: TTSReader.vb
'Version: 20131105

Imports ClipFlair.CaptionsLib.TTS.TTSUtils

Imports System.IO
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.TTS

  Public Class TTSReader
    Inherits BaseCaptionReader

    Public Overrides Sub ReadCaption(ByVal caption As CaptionElement, ByVal reader As TextReader)
      TTSStringToCaption(reader.ReadLine(), caption)
    End Sub

  End Class

End Namespace
