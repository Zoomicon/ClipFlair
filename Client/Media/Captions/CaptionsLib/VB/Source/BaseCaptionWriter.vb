'Project: ClipFlair (http://ClipFlair.codeplex.com)
'Filename: BaseCaptionWriter.vb
'Version: 20131105

Imports ClipFlair.CaptionsLib.Models

Imports System.IO
Imports System.Text
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib

  Public MustInherit Class BaseCaptionWriter
    Implements ICaptionsWriter

#Region "--- Methods ---"

    Public Overloads Sub WriteCaptions(ByVal captions As CaptionRegion, ByVal path As String, ByVal theEncoding As Encoding) Implements ICaptionsWriter.WriteCaptions
      Using writer As New StreamWriter(path, False, theEncoding) 'the Using statement will close the file created when finished
        WriteCaptions(captions, writer)
      End Using
    End Sub

    Public Overloads Sub WriteCaptions(ByVal captions As CaptionRegion, ByVal stream As Stream, ByVal theEncoding As Encoding) Implements ICaptionsWriter.WriteCaptions
      Dim writer As New StreamWriter(stream, theEncoding) 'not Using statement, do not close the stream when finished
      WriteCaptions(captions, writer)
    End Sub

    Public Overloads Sub WriteCaptions(ByVal captions As CaptionRegion, ByVal writer As TextWriter) Implements ICaptionsWriter.WriteCaptions
      writer.NewLine = vbCrLf
      WriteHeader(writer)
      For Each c As CaptionElement In captions.Children
        WriteCaption(c, writer)
      Next
      WriteFooter(writer)
      writer.Flush() 'write out any buffered data
    End Sub

    Public Overridable Sub WriteHeader(ByVal writer As TextWriter) Implements ICaptionsWriter.WriteHeader
      'can override at descendents
    End Sub

    Public MustOverride Sub WriteCaption(ByVal caption As CaptionElement, ByVal writer As TextWriter) Implements ICaptionsWriter.WriteCaption

    Public Overridable Sub WriteFooter(ByVal writer As TextWriter) Implements ICaptionsWriter.WriteFooter
      'can override at descendents
    End Sub

#End Region

  End Class

End Namespace
