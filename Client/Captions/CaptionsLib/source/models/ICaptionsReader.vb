'Description: ICaptionsReader interface
'Authors: George Birbilis (birbilis@kagi.com)
'Version: 20090310

Imports System.IO
Imports System.Text

Namespace LvS.models.Captions

  Public Interface ICaptionsReader

#Region "Methods"

    Sub ReadCaptions(ByVal Captions As ICaptions, ByVal path As String, ByVal theEncoding As Encoding)  'not clearing any existing Captions, just appending to the end (the ICaptions object can choose whether it will sort the Captions by start time or not after the appending)

#End Region

  End Interface

End Namespace
