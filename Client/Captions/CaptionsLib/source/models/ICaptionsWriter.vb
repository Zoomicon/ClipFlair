'Description: ICaptionsWriter interface
'Authors: George Birbilis (birbilis@kagi.com)
'Version: 20090310

Imports System.Text

Namespace LvS.models.Captions

  Public Interface ICaptionsWriter

#Region "Methods"

    Sub WriteCaptions(ByVal Captions As ICaptions, ByVal path As String, ByVal theEncoding As Encoding)

#End Region

  End Interface

End Namespace
