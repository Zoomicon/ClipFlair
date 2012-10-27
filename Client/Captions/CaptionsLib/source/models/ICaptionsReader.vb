'Filename: ICaptionsReader.vb
'Version: 20121016

Imports System.IO
Imports System.Text
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.Models

  Public Interface ICaptionsReader

    'not clearing any existing captions, just adding new ones (the CaptionRegion object can choose whether it will sort the Captions by start time or not after the appending)
    Sub ReadCaptions(ByVal captions As CaptionRegion, ByVal path As String, ByVal theEncoding As Encoding)
    Sub ReadCaptions(ByVal captions As CaptionRegion, ByVal stream As Stream, ByVal theEncoding As Encoding)
    Sub ReadCaptions(ByVal captions As CaptionRegion, ByVal reader As TextReader)

    Sub ReadHeader(ByVal reader As TextReader)
    Sub ReadCaption(ByVal caption As CaptionElement, ByVal reader As TextReader)
    Sub ReadFooter(ByVal reader As TextReader)

  End Interface

End Namespace
