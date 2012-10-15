'Filename: SRTWriter.vb
'Version: 20121015

Imports ClipFlair.CaptionsLib.Models
Imports ClipFlair.CaptionsLib.SRT.SRTUtils

Imports System.IO

Namespace ClipFlair.CaptionsLib.SRT

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

    Public Overrides Sub WriteHeader(ByVal writer As TextWriter)
      fLineNumber = 0 'assuming we're writing a new "file", so resetting counter
    End Sub

    Public Overrides Sub WriteCaption(ByVal Caption As ICaption, ByVal writer As TextWriter)
      fLineNumber += 1
      writer.WriteLine(LineNumber)
      With Caption
        writer.WriteLine(SecondsToSRTtime(.StartTime) + SRT_TIME_SEPARATOR + SecondsToSRTtime(.EndTime))
        writer.WriteLine(.Caption) 'TODO: should take care of empty lines, possibly convert them to a space char
        writer.WriteLine()  'SRT format has an empty line AFTER each Caption, resuling to the file ending up at two empty lines (maybe done so that more Captions can be easily appended later on to the file)
      End With
    End Sub

#End Region

  End Class

End Namespace
