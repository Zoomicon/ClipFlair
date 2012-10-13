'Description: FABWriter class
'Authors: George Birbilis (birbilis@kagi.com)
'Version: 20090309

Imports System.IO
Imports LvS.models.Captions
Imports LvS.utilities.Captions.fab.FABUtils

Namespace LvS.utilities.Captions.fab

  Public Class FABWriter
    Inherits BaseCaptionWriter

#Region "Methods"

    Protected Overrides Sub WriteCaption(ByVal Caption As models.Captions.ICaption, ByVal writer As System.IO.TextWriter)
      With Caption
        writer.WriteLine(SecondsToFABtime(.StartTime) + "  " + SecondsToFABtime(.EndTime))  'separator is double space
        writer.WriteLine(.Caption1)
        If (.Caption2 <> "") Then writer.WriteLine(.Caption2) 'write 2nd Caption line only if non-empty
        writer.WriteLine()  'FAB format has an empty line AFTER each Caption, resuling to the file ending up at two empty lines (maybe done so that more Captions can be easily appended later on to the file)
      End With
    End Sub

#End Region

  End Class

End Namespace
