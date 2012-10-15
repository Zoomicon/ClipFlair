'Filename: ICaption.vb
'Version: 20121015

Namespace ClipFlair.CaptionsLib.Models

  Public Interface ICaption

#Region "Properties"

    Property StartTime() As Double  'see SetTimes method
    Property EndTime() As Double  'see SetTimes method
    Property Duration() As Double
    Property Caption() As String

#End Region

#Region "Methods"

    Sub SetTimes(ByVal theStartTime As Double, ByVal theEndTime As Double) 'set StartTime and EndTime at a single step (setting the times separately can raise time constraint exceptions)
    Function IsTimeIncluded(ByVal theTime As Double) As Boolean

#End Region

#Region "Events"

    Event StartTimeChanged(ByVal source As ICaption, ByVal newTime As Double)
    Event EndTimeChanged(ByVal source As ICaption, ByVal newTime As Double)
    Event DurationChanged(ByVal source As ICaption, ByVal newDuration As Double)
    Event Caption1Changed(ByVal source As ICaption, ByVal newLine As String)
    Event Caption2Changed(ByVal source As ICaption, ByVal newLine As String)

#End Region

  End Interface

End Namespace
