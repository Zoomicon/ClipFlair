'Filename: FABUtils.vb
'Version: 20121015

Imports ClipFlair.CaptionsLib.utils.DateTimeUtils

Namespace ClipFlair.CaptionsLib.FAB

  Public NotInheritable Class FABUtils

    Public Const FABtimeFormat As String = "HH:mm:ss:ff"
    Public Const SignificantDigits As Integer = 2

    Public Shared BaseTime As DateTime = DATETIMEZERO

    Public Shared Function SecondsToFABtime(ByVal seconds As Double) As String
      Return SecondsToDateTimeStr(seconds, BaseTime, FABtimeFormat, SignificantDigits)
    End Function

    Public Shared Function FABtimeToSeconds(ByVal FABTime As String) As Double
      Return TimeStrToSeconds(FABTime, BaseTime, FABtimeFormat, SignificantDigits)
    End Function

  End Class

End Namespace
