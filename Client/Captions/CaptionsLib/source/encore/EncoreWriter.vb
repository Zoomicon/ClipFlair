'Filename: EncoreWriter.vb
'Version: 20121016

Imports ClipFlair.CaptionsLib.Encore.EncoreUtils

Imports System.IO
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.Encore

  Public Class EncoreWriter
    Inherits BaseCaptionWriter

#Region "Methods"

    Public Overrides Sub WriteCaption(ByVal caption As CaptionElement, ByVal writer As System.IO.TextWriter)
      With caption
        writer.WriteLine(SecondsToEncoreTime(.Begin.TotalSeconds) + " " + SecondsToEncoreTime(.End.TotalSeconds) + " " + .Content) 'TODO: assuming Caption alredy contains CRLF between rows (not at ending row) - may should first convert LFs to CRLFs, then also trip CRLF's at end
      End With
    End Sub

#End Region

  End Class

End Namespace
