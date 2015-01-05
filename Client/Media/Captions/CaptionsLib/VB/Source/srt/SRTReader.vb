'Description: SRTReader class
'Version: 20140327

Imports ClipFlair.CaptionsLib.SRT.SRTUtils

Imports System.IO
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.SRT

  Public Class SRTReader
    Inherits BaseCaptionReader

#Region "--- Fields ---"

    Protected fLineNumber As Integer
    Protected fLine As String

#End Region

#Region "--- Properties ---"

    Public ReadOnly Property LineNumber() As Integer
      Get
        Return fLineNumber
      End Get
    End Property

#End Region

#Region "--- Methods ---"

    Public Overrides Sub ReadHeader(reader As System.IO.TextReader)
      fLineNumber = 0 'assuming we're reading a "file" from start, so resetting counter
    End Sub

    Public Overrides Sub ReadCaption(ByVal Caption As CaptionElement, ByVal reader As TextReader)
      fLineNumber += 1

      If (String.IsNullOrEmpty(fLine)) Then fLine = reader.ReadLine() 'do not use IsNullOrWhitespace here since we use a single space char for empty caption rows

      Dim c As String = ""
      While Not String.IsNullOrEmpty(fLine) 'do not use IsNullOrWhitespace here since we use a single space char for empty caption row
        If (c <> "") Then c += vbCrLf
        If (fLine <> " ") Then c += fLine Else c += "" 'treating saved single space lines as empty lines
        fLine = reader.ReadLine()
      End While

      While (fLine IsNot Nothing) AndAlso (fLine = "") 'skip any empty lines between captions
        fLine = reader.ReadLine()
      End While

      If (c <> "") Then SRTStringToCaption(c, Caption)
    End Sub

#End Region

  End Class

End Namespace
