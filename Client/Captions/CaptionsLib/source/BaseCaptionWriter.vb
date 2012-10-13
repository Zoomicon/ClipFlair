'Description: BaseCaptionWriter class
'Authors: George Birbilis (birbilis@kagi.com)
'Version: 20090310

Imports System.IO
Imports System.Text
Imports LvS.models.Captions

Namespace LvS.utilities.Captions

  Public MustInherit Class BaseCaptionWriter
    Implements ICaptionsWriter

#Region "Methods"

    Public Overloads Sub WriteCaptions(ByVal Captions As models.Captions.ICaptions, ByVal path As String, ByVal theEncoding As Encoding) Implements models.Captions.ICaptionsWriter.WriteCaptions
      Using writer As New StreamWriter(path, False, theEncoding)
        WriteCaptions(Captions, writer)
      End Using
    End Sub

    Public Overloads Sub WriteCaptions(ByVal Captions As models.Captions.ICaptions, ByVal writer As TextWriter)
      For Each s As ICaption In Captions
        WriteCaption(s, writer)
      Next
    End Sub

    Protected Overridable Sub WriteHeader()
      'can override at descendents
    End Sub

    Protected Overridable Sub WriteFooter()
      'can override at descendents
    End Sub

    Protected MustOverride Sub WriteCaption(ByVal Caption As ICaption, ByVal writer As TextWriter)

#End Region

  End Class

End Namespace
