'Filename: BaseCaptionWriter.vb
'Version: 20121016

Imports ClipFlair.CaptionsLib.Models

Imports System.IO
Imports System.Text
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib

  Public MustInherit Class BaseCaptionWriter
    Implements ICaptionsWriter

#Region "Methods"

    Public Overloads Sub WriteCaptions(ByVal captions As CaptionRegion, ByVal path As String, ByVal theEncoding As Encoding) Implements ICaptionsWriter.WriteCaptions
      Using writer As New StreamWriter(path, False, theEncoding)
        WriteCaptions(captions, writer)
      End Using
    End Sub

    Public Overloads Sub WriteCaptions(ByVal captions As CaptionRegion, ByVal stream As Stream, ByVal theEncoding As Encoding) Implements ICaptionsWriter.WriteCaptions
      Using writer As New StreamWriter(stream, theEncoding)
        WriteCaptions(captions, writer)
      End Using
    End Sub

    Public Overloads Sub WriteCaptions(ByVal captions As CaptionRegion, ByVal writer As TextWriter) Implements ICaptionsWriter.WriteCaptions
      Try
        WriteHeader(writer)
        For Each s As CaptionElement In captions.Children
          WriteCaption(s, writer)
        Next
        WriteFooter(writer)
      Finally
        writer.Close()
      End Try
    End Sub

    Public Overridable Sub WriteHeader(ByVal writer As TextWriter) Implements ICaptionsWriter.WriteHeader
      'can override at descendents
    End Sub

    Public MustOverride Sub WriteCaption(ByVal Caption As CaptionElement, ByVal writer As TextWriter) Implements ICaptionsWriter.WriteCaption

    Public Overridable Sub WriteFooter(ByVal writer As TextWriter) Implements ICaptionsWriter.WriteFooter
      'can override at descendents
    End Sub

#End Region

  End Class

End Namespace
