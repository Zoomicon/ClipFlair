'Description: EncoreWriter class
'Authors: George Birbilis (birbilis@kagi.com)
'Version: 20090309

Imports System.IO
Imports LvS.models.Captions
Imports LvS.utilities.Captions.encore.EncoreUtils

Namespace LvS.utilities.Captions.encore

  Public Class EncoreWriter
    Inherits BaseCaptionWriter

#Region "Methods"

    Protected Overrides Sub WriteCaption(ByVal Caption As models.Captions.ICaption, ByVal writer As System.IO.TextWriter)
      With Caption
        writer.WriteLine(SecondsToEncoreTime(.StartTime) + " " + SecondsToEncoreTime(.EndTime) + " " + .Caption1)
        If (.Caption2 <> "") Then writer.WriteLine(.Caption2) 'write 2nd Caption line only if non-empty
      End With
    End Sub

#End Region

  End Class

End Namespace
