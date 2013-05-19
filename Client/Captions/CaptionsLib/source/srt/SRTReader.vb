'Description: SRTReader class
'Version: 20130511

Imports ClipFlair.CaptionsLib.SRT.SRTUtils

Imports System.IO
Imports System.Text
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.SRT

  Public Class SRTReader
    Inherits BaseCaptionReader

    Public Overrides Sub ReadCaption(ByVal Caption As CaptionElement, ByVal reader As TextReader)
      Dim line As String = reader.ReadLine()
      Dim c As String = ""
      While (line IsNot Nothing) AndAlso (line <> "") 'TODO: must change this to detect a blank line (treated as separator) before the end of the file or just before a line with the next number
        If (c <> "") Then c += vbCrLf
        c += line
        line = reader.ReadLine()
      End While
      If (c <> "") Then SRTStringToCaption(c, Caption)
    End Sub

  End Class

End Namespace
