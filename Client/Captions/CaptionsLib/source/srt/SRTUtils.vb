'Description: SRTUtils class
'Authors: George Birbilis (birbilis@kagi.com)
'Version: 20090310

Imports LvS.models.Captions
Imports LvS.utilities.DateTimeUtils

Namespace LvS.utilities.Captions.srt

  Public NotInheritable Class SRTUtils

    Public Const SRTtimeFormat As String = "HH:mm:ss,fff"
    Public Const SignificantDigits As Integer = 2 'Must use 2 instead of 3 as done at all the LvS controls
    Public Const SRT_TIME_SEPARATOR As String = " --> "

    Public Shared BaseTime As DateTime = DATETIMEZERO

    Public Shared Function SecondsToSRTtime(ByVal seconds As Double) As String
      Return SecondsToDateTimeStr(seconds, BaseTime, SRTtimeFormat, SignificantDigits)
    End Function

    Public Shared Function SRTtimeToSeconds(ByVal srtTime As String) As Double
      Return TimeStrToSeconds(srtTime, BaseTime, SRTtimeFormat, SignificantDigits)
    End Function

    Public Shared Sub SRTStringToCaption(ByVal srtString As String, ByVal Caption As ICaption)
      Try
        If srtString IsNot Nothing Then
          With Caption
            Dim TimesAndCaptions() As String = Split(srtString, "|")
            Dim TimesOnly() As String = Split(TimesAndCaptions(2), SRT_TIME_SEPARATOR)
            .SetTimes(SRTtimeToSeconds(TimesOnly(0)), SRTtimeToSeconds(TimesOnly(1)))
            If TimesAndCaptions.Length > 3 Then 'if more than 2 lines per Caption, ignore the rest
              .Caption1 = TimesAndCaptions(3)
              If TimesAndCaptions.Length > 4 Then 'if more than 2 lines per Caption, ignore the rest
                .Caption2 = TimesAndCaptions(4)
              Else
                .Caption2 = ""
              End If
            Else
              .Caption1 = ""
            End If
          End With
        End If
      Catch
        Throw New Exception("Invalid SRT") 'TODO: localize
      End Try
    End Sub

  End Class

End Namespace
