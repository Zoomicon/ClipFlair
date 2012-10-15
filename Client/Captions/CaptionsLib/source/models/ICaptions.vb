'Filename: ICaptions.vb
'Version: 20121015

Namespace ClipFlair.CaptionsLib.Models

  Public Interface ICaptions
    Inherits IList(Of ICaption)

#Region "Methods"

    Overloads Function NewCaption() As ICaption
    Overloads Function NewCaption(ByVal theStartTime As Double, ByVal theEndTime As Double) As ICaption 'does not add to the Captions collection, just creates and initializes a new Caption with given parameters

#End Region

  End Interface

End Namespace
