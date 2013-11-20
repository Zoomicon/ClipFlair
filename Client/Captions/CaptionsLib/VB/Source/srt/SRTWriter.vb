'Filename: SRTWriter.vb
'Version: 20131119

Imports ClipFlair.CaptionsLib.SRT.SRTUtils

Imports System.IO
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.SRT

  Public Class SRTWriter
    Inherits BaseCaptionWriter

#Region "--- Fields ---"

    Protected fLineNumber As Integer

#End Region

#Region "--- Properties ---"

    Public ReadOnly Property LineNumber() As Integer
      Get
        Return fLineNumber
      End Get
    End Property

#End Region

#Region "--- Methods ---"

    Public Overrides Sub WriteHeader(ByVal writer As TextWriter)
      fLineNumber = 0 'assuming we're writing a new "file", so resetting counter
    End Sub

    Public Overrides Sub WriteCaption(ByVal caption As CaptionElement, ByVal writer As TextWriter)
      fLineNumber += 1
      writer.WriteLine(LineNumber) 'assuming NewLine property of writer has been set to vbCrLf
      writer.WriteLine(SecondsToSRTtime(caption.Begin.TotalSeconds) + SRT_TIME_SEPARATOR + SecondsToSRTtime(caption.End.TotalSeconds))
      If (CStr(caption.Content) <> "") Then writer.WriteLine(CStr(caption.Content).Replace(vbCrLf + vbCrLf, vbCrLf + " " + vbCrLf)) 'never write an empty line (since the parser can treat it as a caption end)
      writer.WriteLine()  'SRT format has an empty line AFTER each Caption, resuling to the file ending up at two empty lines (maybe done so that more Captions can be easily appended later on to the file)
    End Sub

#End Region

  End Class

End Namespace
