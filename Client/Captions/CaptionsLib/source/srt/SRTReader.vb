'Description: SRTReader class
'Authors: George Birbilis (birbilis@kagi.com), Kostas Mitropoulos (kosmitr@eap.gr)
'Version: 20090310

Imports System.IO
Imports System.Text
Imports LvS.models.Captions
Imports LvS.utilities.Captions.srt.SRTUtils

Namespace LvS.utilities.Captions.srt

  Public Class SRTReader
    Implements ICaptionsReader

    Public Overloads Sub ReadCaptions(ByVal Captions As models.Captions.ICaptions, ByVal path As String, ByVal theEncoding As Encoding) Implements models.Captions.ICaptionsReader.ReadCaptions
      'not clearing any existing Captions, just appending to the end (the ICaptions object can choose whether it will sort the Captions by start time or not after the appending)
      Using reader As New StreamReader(path, theEncoding, True) '"Using" makes sure the resource is immediately deallocated at "End Using"
        ReadCaptions(Captions, reader)
      End Using
    End Sub

    Public Overloads Sub ReadCaptions(ByVal Captions As ICaptions, ByVal reader As TextReader)
      Try
        Dim subline As String = ""
        Dim line As String = reader.ReadLine()
        While (line IsNot Nothing)
          If line <> "" Then
            subline = subline & "|" & line
          Else
            Dim Caption As ICaption = Captions.NewCaption
            ReadCaption(Caption, subline)
            Captions.Add(Caption)
            subline = ""
          End If
          line = reader.ReadLine()
        End While
      Finally
        reader.Close()
      End Try
    End Sub

    Protected Shared Sub ReadCaption(ByVal Caption As ICaption, ByVal subline As String)
      SRTStringToCaption(subline, Caption)
    End Sub

  End Class

End Namespace
