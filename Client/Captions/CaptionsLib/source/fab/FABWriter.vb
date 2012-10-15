'Filename: FABWriter.vb
'Version: 20121015

Imports System.IO
Imports CaptionsLib
Imports CaptionsLib.Models
Imports CaptionsLib.FAB.FABUtils

Namespace CaptionsLib.FAB

  Public Class FABWriter
    Inherits BaseCaptionWriter

#Region "Methods"

    Public Overrides Sub WriteCaption(ByVal Caption As ICaption, ByVal writer As System.IO.TextWriter)
      With Caption
        writer.WriteLine(SecondsToFABtime(.StartTime) + "  " + SecondsToFABtime(.EndTime))  'separator is double space
        writer.WriteLine(.Caption) 'TODO: assuming Caption alredy contains CRLF between rows (not at ending row) - may should first convert LFs to CRLFs, then also trip CRLF's at end
        writer.WriteLine()  'FAB format has an empty line AFTER each Caption, resuling to the file ending up at two empty lines (maybe done so that more Captions can be easily appended later on to the file)
      End With
    End Sub

#End Region

  End Class

End Namespace
