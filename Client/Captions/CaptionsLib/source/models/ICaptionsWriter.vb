'Filename: ICaptionsWriter.vb
'Version: 20121015

Imports System.Text
Imports System.IO

Namespace CaptionsLib.Models

  Public Interface ICaptionsWriter

#Region "Methods"

    Sub WriteCaptions(ByVal captions As Icaptions, ByVal path As String, ByVal theEncoding As Encoding)
    Sub WriteCaptions(ByVal captions As Icaptions, ByVal stream As Stream, ByVal theEncoding As Encoding)
    Sub WriteCaptions(ByVal captions As ICaptions, ByVal writer As TextWriter)

    Sub WriteHeader(ByVal writer As TextWriter)
    Sub WriteCaption(ByVal caption As ICaption, ByVal writer As TextWriter)
    Sub WriteFooter(ByVal writer As TextWriter)

#End Region

  End Interface

End Namespace
