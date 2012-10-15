'Filename: TTSUtils.vb
'Version: 20121015

Imports CaptionsLib.Models
Imports CaptionsLib.Utils.DateTimeUtils

Namespace CaptionsLib.TTS

  Public NotInheritable Class TTSUtils

    Public Const TTStimeFormat As String = "H:mm:ss.ff" 'do not use HH since TTS doesn't require two digits for the hour
    Public Const SignificantDigits As Integer = 2
    Public Const TTS_TIME_END As String = ",NTP "

    Public Shared BaseTime As DateTime = DATETIMEZERO

    Public Shared Function SecondsToTTStime(ByVal seconds As Double) As String
      Return SecondsToDateTimeStr(seconds, BaseTime, TTStimeFormat, SignificantDigits)
    End Function

    Public Shared Function TTStimeToSeconds(ByVal ttsTime As String) As Double
      Return TimeStrToSeconds(ttsTime, BaseTime, TTStimeFormat, SignificantDigits)
    End Function

    Public Shared Function CaptionToTTSString(ByVal Caption As ICaption) As String
      With Caption
        Return SecondsToTTStime(.StartTime) + "," + SecondsToTTStime(.EndTime) + TTS_TIME_END + .Caption.Replace(vbCrLf, "|")
      End With
    End Function

    Public Shared Sub TTSStringToCaption(ByVal ttsString As String, ByVal Caption As ICaption)
      Try
        With Caption
          Dim TimesAndCaptions() As String = Split(ttsString, TTS_TIME_END)

          Dim TimesOnly() As String = Split(TimesAndCaptions(0), ",")
          .SetTimes(TTStimeToSeconds(TimesOnly(0)), TTStimeToSeconds(TimesOnly(1)))

          .Caption = TimesAndCaptions(1).Replace("|", vbCrLf)
        End With
      Catch
        Throw New Exception("Invalid TTS") 'TODO: localize
      End Try
    End Sub

  End Class

End Namespace
