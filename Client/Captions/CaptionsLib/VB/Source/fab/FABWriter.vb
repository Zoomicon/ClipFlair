'Filename: FABWriter.vb
'Version: 20140322

Imports ClipFlair.CaptionsLib.Utils.StringUtils
Imports ClipFlair.CaptionsLib.FAB.FABUtils

Imports System.IO
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.FAB

  Public Class FABWriter
    Inherits BaseCaptionWriter

#Region "--- Methods ---"

    Public Overrides Sub WriteCaption(ByVal caption As CaptionElement, ByVal writer As TextWriter)
      writer.WriteLine(SecondsToFABtime(caption.Begin.TotalSeconds) + "  " + SecondsToFABtime(caption.End.TotalSeconds))  'separator is double space
      writer.WriteLine(CStr(caption.Content).CrToCrLf().PrefixEmptyLines(" "))
      writer.WriteLine()  'FAB format has an empty line AFTER each Caption, resuling to the file ending up at two empty lines (maybe done so that more Captions can be easily appended later on to the file)
    End Sub

#End Region

  End Class

End Namespace
