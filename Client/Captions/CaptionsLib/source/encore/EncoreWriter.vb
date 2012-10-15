'Filename: EncoreWriter.vb
'Version: 20121015

Imports ClipFlair.CaptionsLib
Imports ClipFlair.CaptionsLib.Models
Imports ClipFlair.CaptionsLib.Encore.EncoreUtils

Imports System.IO

Namespace ClipFlair.CaptionsLib.Encore

  Public Class EncoreWriter
    Inherits BaseCaptionWriter

#Region "Methods"

    Public Overrides Sub WriteCaption(ByVal Caption As ICaption, ByVal writer As System.IO.TextWriter)
      With Caption
        writer.WriteLine(SecondsToEncoreTime(.StartTime) + " " + SecondsToEncoreTime(.EndTime) + " " + .Caption) 'TODO: assuming Caption alredy contains CRLF between rows (not at ending row) - may should first convert LFs to CRLFs, then also trip CRLF's at end
      End With
    End Sub

#End Region

  End Class

End Namespace
