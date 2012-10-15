'Filenam: SRTUtils.vb
'Version: 20121015

Imports CaptionsLib.Models
Imports CaptionsLib.Utils.DateTimeUtils

Namespace CaptionsLib.SRT

  Public NotInheritable Class SRTUtils

    Public Const SRTtimeFormat As String = "HH:mm:ss,fff"
    Public Const SignificantDigits As Integer = 2 'Must use 2 instead of 3 as done at all the CaptionsLib controls
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
            Dim TimesAndCaptions() As String = Split(srtString, vbCrLf)

            Dim TimesOnly() As String = Split(TimesAndCaptions(1), SRT_TIME_SEPARATOR)
            .SetTimes(SRTtimeToSeconds(TimesOnly(0)), SRTtimeToSeconds(TimesOnly(1)))

            .Caption = TimesAndCaptions(2)
            For i As Integer = 3 To TimesAndCaptions.Length
              .Caption += vbCrLf + TimesAndCaptions(i)
            Next
         End With
        End If
      Catch
        Throw New Exception("Invalid SRT") 'TODO: localize
      End Try
    End Sub

  End Class

End Namespace
