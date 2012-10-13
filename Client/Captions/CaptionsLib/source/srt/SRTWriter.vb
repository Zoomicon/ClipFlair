'Description: SRTWriter class
'Authors: George Birbilis (birbilis@kagi.com)
'Version: 20090309

Imports System.IO
Imports LvS.models.Captions
Imports LvS.utilities.Captions.srt.SRTUtils

Namespace LvS.utilities.Captions.srt

  Public Class SRTWriter
    Inherits BaseCaptionWriter

#Region "Fields"

    Protected fLineNumber As Integer

#End Region

#Region "Properties"

    Public ReadOnly Property LineNumber() As Integer
      Get
        Return fLineNumber
      End Get
    End Property

#End Region

#Region "Methods"

    Protected Overrides Sub WriteHeader()
      fLineNumber = 0
    End Sub

    Protected Overrides Sub WriteCaption(ByVal Caption As models.Captions.ICaption, ByVal writer As System.IO.TextWriter)
      fLineNumber += 1
      writer.WriteLine(LineNumber)
      With Caption
        writer.WriteLine(SecondsToSRTtime(.StartTime) + SRT_TIME_SEPARATOR + SecondsToSRTtime(.EndTime))
        If (.Caption1 <> "") Then writer.WriteLine(.Caption1) 'if 1st line is missing, 2nd will become 1st (SRT doesn't support otherwise)
        If (.Caption2 <> "") Then writer.WriteLine(.Caption2) 'write 2nd Caption line only if non-empty
        writer.WriteLine()  'SRT format has an empty line AFTER each Caption, resuling to the file ending up at two empty lines (maybe done so that more Captions can be easily appended later on to the file)
      End With
    End Sub

#End Region

  End Class

End Namespace
