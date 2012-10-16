'Filename: DateTimeUtils.vb
'Version: 20121016

Namespace ClipFlair.CaptionsLib.Utils

  Public NotInheritable Class DateTimeUtils

#Region "Constants"

    Public Shared ReadOnly DATETIMEZERO As New DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Local) '01/01/0001, 0:0:0.00 (dates must be from year 01 and above) '??? trying bigger year to avoid accuracy errors but it causes hichkups at the timebar

#End Region

#Region "Methods"

    Public Shared Function FixDateTime(ByVal datetime As DateTime) As DateTime
      With DATETIMEZERO
        Return New DateTime(.Year, .Month, .Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond)
      End With
    End Function

    Public Shared Function DateTimeToSeconds(ByVal theDateTime As DateTime, ByVal baseDateTime As DateTime, ByVal digits As Integer) As Double
      Dim result As Double = (theDateTime - baseDateTime).TotalSeconds
      If digits <> -1 Then Return Math.Round(result, digits)
      Return result
    End Function

    Public Shared Function SecondsToDateTime(ByVal seconds As Double, ByVal baseDateTime As DateTime, ByVal digits As Integer) As DateTime
      Try
        If digits <> -1 Then seconds = Math.Round(seconds, digits)
        Return baseDateTime.AddSeconds(seconds)
      Catch
        Return baseDateTime 'silently handle exceptions, returning the baseDateTime in that case
      End Try
    End Function

    Public Shared Function TimeStrToDateTime(ByVal datetimeStr As String, ByVal baseDateTime As DateTime, ByVal datetimeFormat As String) As DateTime
      Dim result As DateTime
      Try
        result = DateTime.ParseExact(datetimeStr, datetimeFormat, Nothing)
      Catch 'if exact datetime parsing fails, try with the plain parse method
        result = DateTime.Parse(datetimeStr)
      End Try
      result = FixDateTime(baseDateTime + (result - baseDateTime))
      Return result
    End Function

    Public Shared Function TimeStrToSeconds(ByVal datetimeStr As String, ByVal baseDateTime As DateTime, ByVal datetimeFormat As String, ByVal digits As Integer) As Double
      Return DateTimeToSeconds(TimeStrToDateTime(datetimeStr, baseDateTime, datetimeFormat), baseDateTime, digits)
    End Function

    Public Shared Function SecondsToDateTimeStr(ByVal seconds As Double, ByVal baseDateTime As DateTime, ByVal datetimeFormat As String, ByVal digits As Integer) As String
      Return SecondsToDateTime(seconds, baseDateTime, digits).ToString(datetimeFormat)
    End Function

#End Region

  End Class

End Namespace
