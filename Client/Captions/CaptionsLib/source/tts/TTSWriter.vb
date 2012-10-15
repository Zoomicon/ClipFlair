'Filename: TTSWriter.vb
'Version: 20121015

Imports ClipFlair.CaptionsLib.Models
Imports ClipFlair.CaptionsLib.TTS.TTSUtils

Imports System.IO

Namespace ClipFlair.CaptionsLib.TTS

  Public Class TTSWriter
    Inherits BaseCaptionWriter

#Region "Methods"

    Public Overrides Sub WriteCaption(ByVal Caption As ICaption, ByVal writer As TextWriter)
      writer.WriteLine(CaptionToTTSString(Caption))
    End Sub

#End Region

  End Class

End Namespace
