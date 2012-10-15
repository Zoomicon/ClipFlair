'Description: SRTReader class
'Version: 20121015

Imports ClipFlair.CaptionsLib.Models
Imports ClipFlair.CaptionsLib.SRT.SRTUtils

Imports System.IO
Imports System.Text

Namespace ClipFlair.CaptionsLib.SRT

  Public Class SRTReader
    Inherits BaseCaptionReader

    Public Overrides Sub ReadCaption(ByVal Caption As ICaption, ByVal reader As TextReader)
      Dim subline As String = ""
      Dim line As String = reader.ReadLine()
      While (line IsNot Nothing)
        If line <> "" Then
          subline = subline & vbCrLf & line
        Else
          SRTStringToCaption(subline, Caption)
          Exit While
        End If
        line = reader.ReadLine()
      End While
    End Sub

  End Class

End Namespace
