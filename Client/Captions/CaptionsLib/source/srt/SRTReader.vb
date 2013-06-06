'Description: SRTReader class
'Version: 20130606

Imports ClipFlair.CaptionsLib.SRT.SRTUtils

Imports System.IO
Imports System.Text
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.SRT

  Public Class SRTReader
    Inherits BaseCaptionReader

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

    Public Overrides Sub ReadHeader(reader As System.IO.TextReader)
      fLineNumber = 0 'assuming we're reading a "file" from start, so resetting counter
    End Sub

    Public Overrides Sub ReadCaption(ByVal Caption As CaptionElement, ByVal reader As TextReader)
      fLineNumber += 1

      Dim line As String = reader.ReadLine()
      Dim c As String = ""
      While (line IsNot Nothing) AndAlso (line <> "") 'TODO: must change this to detect a blank line (treated as separator) before the end of the file or just before a line with the next number
        If (c <> "") Then c += vbCrLf
        c += line
        line = reader.ReadLine()
      End While
      If (c <> "") Then SRTStringToCaption(c, Caption)
    End Sub

#End Region

  End Class

End Namespace
