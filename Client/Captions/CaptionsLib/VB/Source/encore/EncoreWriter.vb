'Filename: EncoreWriter.vb
'Version: 20140322

Imports ClipFlair.CaptionsLib.Utils.StringUtils
Imports ClipFlair.CaptionsLib.Encore.EncoreUtils

Imports System.IO
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.Encore

  Public Class EncoreWriter
    Inherits BaseCaptionWriter

#Region "--- Methods ---"

    Public Overrides Sub WriteCaption(ByVal caption As CaptionElement, ByVal writer As TextWriter)
      writer.WriteLine(SecondsToEncoreTime(caption.Begin.TotalSeconds) + " " + SecondsToEncoreTime(caption.End.TotalSeconds) + " " + CStr(caption.Content).CrToCrLf())
    End Sub

#End Region

  End Class

End Namespace
