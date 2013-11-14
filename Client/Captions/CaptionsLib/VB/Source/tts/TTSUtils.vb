'Filename: TTSUtils.vb
'Version: 20131113

Imports ClipFlair.CaptionsLib.Utils.DateTimeUtils

Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.TTS

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

    Public Shared Function CaptionToTTSString(ByVal caption As CaptionElement) As String
      Return SecondsToTTStime(caption.Begin.TotalSeconds) + "," + SecondsToTTStime(caption.End.TotalSeconds) + TTS_TIME_END + CStr(caption.Content).Replace(vbCrLf, "|")
    End Function

    Public Shared Sub TTSStringToCaption(ByVal ttsString As String, ByVal caption As CaptionElement)
      Try
        Dim TimesAndCaptions() As String = Split(ttsString, TTS_TIME_END)

        Dim TimesOnly() As String = Split(TimesAndCaptions(0), ",")
        caption.Begin = TimeSpan.FromSeconds(TTStimeToSeconds(TimesOnly(0)))
        caption.End = TimeSpan.FromSeconds(TTStimeToSeconds(TimesOnly(1)))

        caption.Content = TimesAndCaptions(1).Replace("|", vbCrLf)
      Catch
        Throw New Exception("Invalid TTS") 'TODO: localize
      End Try
    End Sub

  End Class

End Namespace
