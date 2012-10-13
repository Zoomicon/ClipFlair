'Description: FileUtils class (cut-down version from http://LvS.codeplex.com)
'Authors: George Birbilis (birbilis@kagi.com)
'Version: 20121013

Namespace LvS.utilities

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
