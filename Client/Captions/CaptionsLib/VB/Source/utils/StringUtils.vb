'Filename: StringUtils.vb
'Version: 20140322

Imports System.Runtime.CompilerServices

Namespace ClipFlair.CaptionsLib.Utils

  Public Module StringUtils

#Region "--- Methods ---"

    <Extension()>
    Public Function CrToCrLf(ByVal s As String) As String
      Return s.Replace(vbCrLf, vbCr).Replace(vbCr, vbCrLf) 'doing CrLf->Cr, Cr->CrLf to make sure that mixed Cr, CrLF strings ae converted to CrLf ok
    End Function

    <Extension()>
    Public Function PrefixEmptyLines(ByVal s As String, ByVal prefix As String) As String
      Return s.Replace(vbCrLf + vbCrLf, vbCrLf + prefix + vbCrLf)
    End Function

#End Region

  End Module

End Namespace
