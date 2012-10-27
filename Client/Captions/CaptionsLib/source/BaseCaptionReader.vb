'Filename: BaseCaptionReader.vb
'Version: 20121016

Imports ClipFlair.CaptionsLib.models

Imports System.IO
Imports System.Text
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib

  Public MustInherit Class BaseCaptionReader
    Implements ICaptionsReader

#Region "Methods"

    Public Overloads Sub ReadCaptions(ByVal captions As CaptionRegion, ByVal path As String, ByVal theEncoding As Encoding) Implements ICaptionsReader.ReadCaptions
      'not clearing any existing captions, just appending to the end (the CaptionRegion object can choose whether it will sort the Captions by start time or not after the appending)
      Using reader As New StreamReader(path, theEncoding, True) '"Using" makes sure the resource is immediately deallocated at "End Using"
        ReadCaptions(captions, reader)
      End Using
    End Sub

    Public Overloads Sub ReadCaptions(ByVal captions As CaptionRegion, ByVal stream As Stream, ByVal theEncoding As Encoding) Implements ICaptionsReader.ReadCaptions
      'not clearing any existing captions, just appending to the end (the CaptionRegion object can choose whether it will sort the Captions by start time or not after the appending)
      Using reader As New StreamReader(stream, theEncoding, True) '"Using" makes sure the resource is immediately deallocated at "End Using"
        ReadCaptions(captions, reader)
      End Using
    End Sub

    Public Overloads Sub ReadCaptions(ByVal captions As CaptionRegion, ByVal reader As TextReader) Implements ICaptionsReader.ReadCaptions
      Try
        ReadHeader(reader)
        While reader.Peek <> -1
          Dim caption As New CaptionElement()
          ReadCaption(caption, reader)
          captions.Children.Add(caption)
        End While
        ReadFooter(reader)
      Finally
        reader.Close()
      End Try
    End Sub

    Public Overridable Sub ReadHeader(ByVal reader As TextReader) Implements ICaptionsReader.ReadHeader
      'can override at descendents
    End Sub

    Public MustOverride Sub ReadCaption(ByVal caption As CaptionElement, ByVal reader As TextReader) Implements ICaptionsReader.ReadCaption

    Public Overridable Sub ReadFooter(ByVal reader As TextReader) Implements ICaptionsReader.ReadFooter
      'can override at descendents
    End Sub

#End Region

  End Class

End Namespace