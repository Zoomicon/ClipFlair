'Filenam: SRTUtils.vb
'Version: 20121119

Imports ClipFlair.CaptionsLib.Utils.DateTimeUtils

Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.SRT

  Public NotInheritable Class SRTUtils

    Public Const SRTtimeFormat As String = "HH:mm:ss,fff"
    Public Const SignificantDigits As Integer = 2 'Must use 2 instead of 3 as done at all the ClipFlair.CaptionsLib controls
    Public Const SRT_TIME_SEPARATOR As String = " --> "

    Public Shared BaseTime As DateTime = DATETIMEZERO

    Public Shared Function SecondsToSRTtime(ByVal seconds As Double) As String
      Return SecondsToDateTimeStr(seconds, BaseTime, SRTtimeFormat, SignificantDigits)
    End Function

    Public Shared Function SRTtimeToSeconds(ByVal srtTime As String) As Double
      Return TimeStrToSeconds(srtTime, BaseTime, SRTtimeFormat, SignificantDigits)
    End Function

    Public Shared Sub SRTStringToCaption(ByVal srtString As String, ByVal caption As CaptionElement)
      Try
        If srtString IsNot Nothing Then
          Dim TimesAndCaptions() As String = Split(srtString, vbCrLf)

          Dim TimesOnly() As String = Split(TimesAndCaptions(1), SRT_TIME_SEPARATOR)
          caption.Begin = TimeSpan.FromSeconds(SRTtimeToSeconds(TimesOnly(0)))
          caption.End = TimeSpan.FromSeconds(SRTtimeToSeconds(TimesOnly(1)))

          caption.Content = ""
          For i As Integer = 2 To TimesAndCaptions.Length - 1
            If (CStr(caption.Content) <> "") Then caption.Content += vbCrLf
            caption.Content += TimesAndCaptions(i)
          Next
        End If
      Catch
        Throw New Exception("Invalid SRT") 'TODO: localize
      End Try
    End Sub

  End Class

End Namespace
