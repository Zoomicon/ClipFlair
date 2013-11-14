'Filename: TTSWriter.vb
'Version: 20131105

Imports ClipFlair.CaptionsLib.TTS.TTSUtils

Imports System.IO
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.TTS

  Public Class TTSWriter
    Inherits BaseCaptionWriter

#Region "--- Methods ---"

    Public Overrides Sub WriteCaption(ByVal caption As CaptionElement, ByVal writer As TextWriter)
      writer.WriteLine(CaptionToTTSString(caption))
    End Sub

#End Region

  End Class

End Namespace
