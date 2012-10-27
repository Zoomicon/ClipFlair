'Filename: FileUtils.vb
'Version: 20121016

Namespace ClipFlair.CaptionsLib.Utils

  Public NotInheritable Class FileUtils

#Region "Methods"

    Public Overloads Shared Function CheckExtension(ByVal filename As String, ByVal extension As String) As String
      If (filename IsNot Nothing) AndAlso filename.EndsWith(extension, StringComparison.InvariantCultureIgnoreCase) Then Return extension Else Return Nothing
    End Function

    Public Overloads Shared Function CheckExtension(ByVal filename As String, ByVal extensions As String()) As String
      If filename IsNot Nothing Then
        For Each s As String In extensions
          If filename.EndsWith(s, StringComparison.InvariantCultureIgnoreCase) Then Return s
        Next s
      End If
      Return Nothing
    End Function

#End Region

  End Class

End Namespace
