'Description: TTSWriter class
'Authors: George Birbilis (birbilis@kagi.com)
'Version: 20090309

Imports System.IO
Imports LvS.models.Captions
Imports LvS.utilities.Captions.tts.TTSUtils

Namespace LvS.utilities.Captions.tts

  Public Class TTSUnicodeWriter
    Inherits BaseCaptionWriter

#Region "Methods"

    Protected Overrides Sub WriteCaption(ByVal Caption As ICaption, ByVal writer As TextWriter)
      writer.WriteLine(CaptionToTTSString(Caption))
    End Sub

#End Region

  End Class

End Namespace
