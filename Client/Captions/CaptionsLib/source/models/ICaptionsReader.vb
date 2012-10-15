'Filename: ICaptionsReader.vb
'Version: 20121015

Imports System.IO
Imports System.Text

Namespace ClipFlair.CaptionsLib.Models

  Public Interface ICaptionsReader

#Region "Methods"

    'not clearing any existing captions, just adding new ones (the ICaptions object can choose whether it will sort the Captions by start time or not after the appending)
    Sub ReadCaptions(ByVal captions As Icaptions, ByVal path As String, ByVal theEncoding As Encoding)
    Sub ReadCaptions(ByVal captions As Icaptions, ByVal stream As Stream, ByVal theEncoding As Encoding)
    Sub ReadCaptions(ByVal captions As ICaptions, ByVal reader As TextReader)

    Sub ReadHeader(ByVal reader As TextReader)
    Sub ReadCaption(ByVal caption As ICaption, ByVal reader As TextReader)
    Sub ReadFooter(ByVal reader As TextReader)

#End Region

  End Interface

End Namespace
