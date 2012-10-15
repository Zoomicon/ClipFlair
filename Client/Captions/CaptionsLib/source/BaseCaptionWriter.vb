﻿'Filename: BaseCaptionWriter.vb
'Version: 20121015

Imports CaptionsLib.Models

Imports System.IO
Imports System.Text

Namespace CaptionsLib

  Public MustInherit Class BaseCaptionWriter
    Implements ICaptionsWriter

#Region "Methods"

    Public Overloads Sub WriteCaptions(ByVal captions As Icaptions, ByVal path As String, ByVal theEncoding As Encoding) Implements ICaptionsWriter.WriteCaptions
      Using writer As New StreamWriter(path, False, theEncoding)
        WriteCaptions(captions, writer)
      End Using
    End Sub

    Public Overloads Sub WriteCaptions(ByVal captions As ICaptions, ByVal stream As Stream, ByVal theEncoding As Encoding) Implements ICaptionsWriter.WriteCaptions
      Using writer As New StreamWriter(stream, theEncoding)
        WriteCaptions(captions, writer)
      End Using
    End Sub

    Public Overloads Sub WriteCaptions(ByVal captions As Icaptions, ByVal writer As TextWriter) Implements ICaptionsWriter.WriteCaptions
      Try
        WriteHeader(writer)
        For Each s As ICaption In captions
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

    Public MustOverride Sub WriteCaption(ByVal Caption As ICaption, ByVal writer As TextWriter) Implements ICaptionsWriter.WriteCaption

    Public Overridable Sub WriteFooter(ByVal writer As TextWriter) Implements ICaptionsWriter.WriteFooter
      'can override at descendents
    End Sub

#End Region

  End Class

End Namespace
