'Filename: FABWriter.vb
'Version: 20121016

Imports ClipFlair.CaptionsLib.FAB.FABUtils

Imports System.IO
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.FAB

  Public Class FABWriter
    Inherits BaseCaptionWriter

#Region "Methods"

    Public Overrides Sub WriteCaption(ByVal Caption As CaptionElement, ByVal writer As System.IO.TextWriter)
      With Caption
        writer.WriteLine(SecondsToFABtime(.Begin.TotalSeconds) + "  " + SecondsToFABtime(.End.TotalSeconds))  'separator is double space
        writer.WriteLine(.Content) 'TODO: assuming Caption alredy contains CRLF between rows (not at ending row) - may should first convert LFs to CRLFs, then also trip CRLF's at end
        writer.WriteLine()  'FAB format has an empty line AFTER each Caption, resuling to the file ending up at two empty lines (maybe done so that more Captions can be easily appended later on to the file)
      End With
    End Sub

#End Region

  End Class

End Namespace
