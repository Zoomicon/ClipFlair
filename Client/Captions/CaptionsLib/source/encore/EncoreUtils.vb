'Filename: EncoreUtils.vb
'Version: 20121015

Imports ClipFlair.CaptionsLib.utils.DateTimeUtils

Namespace ClipFlair.CaptionsLib.Encore

  Public NotInheritable Class EncoreUtils
    'for Adobe Encore

    Public Const EncoreTimeFormat As String = "HH:mm:ss:ff"
    Public Const SignificantDigits As Integer = 2

    Public Shared BaseTime As DateTime = DATETIMEZERO

    Public Shared Function SecondsToEncoreTime(ByVal seconds As Double) As String
      Return SecondsToDateTimeStr(seconds, BaseTime, EncoreTimeFormat, SignificantDigits)
    End Function

    Public Shared Function EncoreTimeToSeconds(ByVal EncoreTime As String) As Double
      Return TimeStrToSeconds(EncoreTime, BaseTime, EncoreTimeFormat, SignificantDigits)
    End Function

  End Class

End Namespace
