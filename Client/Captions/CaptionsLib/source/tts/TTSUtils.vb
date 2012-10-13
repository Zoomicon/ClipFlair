'Description: TTSUtils class
'Authors: George Birbilis (birbilis@kagi.com), Kostas Mitropoulos (kosmitr@eap.gr)
'Version: 20090310

Imports LvS.models.Captions
Imports LvS.utilities.DateTimeUtils

Namespace LvS.utilities.Captions.tts

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
        Dim result As String = SecondsToTTStime(.StartTime) + "," + SecondsToTTStime(.EndTime) + TTS_TIME_END + .Caption1
        If (.Caption2 <> "") Then result += "|" + .Caption2 'write 2nd Caption line only if non-empty
        Return result
      End With
    End Function

    Public Shared Sub TTSStringToCaption(ByVal ttsString As String, ByVal Caption As ICaption)
      Try
        With Caption
          Dim TimesAndCaptions() As String = Split(ttsString, TTS_TIME_END)
          Dim TimesOnly() As String = Split(TimesAndCaptions(0), ",")
          .SetTimes(TTStimeToSeconds(TimesOnly(0)), TTStimeToSeconds(TimesOnly(1)))
          Dim CaptionsOnly() As String = Split(TimesAndCaptions(1), "|")
          .Caption1 = CaptionsOnly(0)
          If CaptionsOnly.Length > 1 Then .Caption2 = CaptionsOnly(1) Else .Caption2 = "" 'if more than 2 lines per Caption, ignore the rest
        End With
      Catch
        Throw New Exception("Invalid TTS") 'TODO: localize
      End Try
    End Sub

  End Class

End Namespace
