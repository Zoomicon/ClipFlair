'Description: ICaptions interface
'Authors: George Birbilis (birbilis@kagi.com)
'Version: 20080418

Namespace LvS.models.Captions

  Public Interface ICaptions
    Inherits IList(Of ICaption)

#Region "Properties"

    Property Source() As String 'allow URLs (maybe) 'throws EFileNotFound exception
    Property Encoding() As System.Text.Encoding
    Property CurrentIndex() As Integer
    Property CurrentTime() As Double 'avoid spurious events when player notifies for new time and caption grid sets its time to nearest start time (make sure it doesn't send event back to player in that case)

#End Region

#Region "Methods"

    Sub Save()
    Overloads Function NewCaption() As ICaption
    Overloads Function NewCaption(ByVal theStartTime As Double, ByVal theEndTime As Double) As ICaption 'does not add to the Captions collection, just creates and initializes a new Caption with given parameters
    Function FindIndexByTime(ByVal theTime As Double) As Integer 'not only start time, but any time between start and end, inclusive
    Function FindCurrentOrPreviousIndexByTime(ByVal theTime As Double) As Integer 'not only start time, but any time after start
    Function FindCurrentOrNextIndexByTime(ByVal theTime As Double) As Integer 'not only start time, but any time after start
    Function FindStartTimeByIndex(ByVal theIndex As Integer) As Double
    Function FindStartTimeByTime(ByVal theTime As Double) As Double

#End Region

#Region "Events"

    Event CurrentIndexChanged(ByVal source As ICaptions, ByVal newIndex As Integer)
    Event CaptionChanged(ByVal source As ICaptions, ByVal theIndex As Integer, ByVal theCaption As ICaption)
    Event TimeChanged(ByVal source As ICaptions, ByVal newTime As Double)

#End Region

  End Interface

End Namespace
