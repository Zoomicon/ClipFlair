'Filename: EncoreWriter.vb
'Version: 20131114

Imports ClipFlair.CaptionsLib.Encore.EncoreUtils

Imports System.IO
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.Encore

  Public Class EncoreWriter
    Inherits BaseCaptionWriter

#Region "--- Methods ---"

    Public Overrides Sub WriteCaption(ByVal caption As CaptionElement, ByVal writer As TextWriter)
      writer.WriteLine(SecondsToEncoreTime(caption.Begin.TotalSeconds) + " " + SecondsToEncoreTime(caption.End.TotalSeconds) + " " + caption.Content) 'TODO: assuming Caption alredy contains CRLF between rows (not at ending row) - may should first convert LFs to CRLFs, then also trip CRLF's at end
    End Sub

#End Region

  End Class

End Namespace
