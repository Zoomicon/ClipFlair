'Description: EncoreUtils class (for Adobe Encore)
'Authors: George Birbilis (birbilis@kagi.com)
'Version: 20090310

Imports LvS.utilities.DateTimeUtils

Namespace LvS.utilities.Captions.encore

  Public NotInheritable Class EncoreUtils

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
