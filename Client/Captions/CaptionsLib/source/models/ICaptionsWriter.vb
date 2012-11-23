'Project: ClipFlair (http://ClipFlair.codeplex.com)
'Filename: ICaptionsWriter.vb
'Version: 20121016

Imports System.Text
Imports System.IO
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.Models

  Public Interface ICaptionsWriter

    Sub WriteCaptions(ByVal captions As CaptionRegion, ByVal path As String, ByVal theEncoding As Encoding)
    Sub WriteCaptions(ByVal captions As CaptionRegion, ByVal stream As Stream, ByVal theEncoding As Encoding)
    Sub WriteCaptions(ByVal captions As CaptionRegion, ByVal writer As TextWriter)

    Sub WriteHeader(ByVal writer As TextWriter)
    Sub WriteCaption(ByVal caption As CaptionElement, ByVal writer As TextWriter)
    Sub WriteFooter(ByVal writer As TextWriter)

  End Interface

End Namespace
