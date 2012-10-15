'Filename: TTSReader.vb
'Version: 20121015

Imports CaptionsLib
Imports CaptionsLib.Models
Imports CaptionsLib.TTS.TTSUtils

Imports System.IO
Imports System.Text

Namespace CaptionsLib.TTS

  Public Class TTSReader
    Inherits BaseCaptionReader

    Public Overrides Sub ReadCaption(ByVal caption As ICaption, ByVal reader As TextReader)
      TTSStringToCaption(reader.ReadLine(), caption)
    End Sub

  End Class

End Namespace
