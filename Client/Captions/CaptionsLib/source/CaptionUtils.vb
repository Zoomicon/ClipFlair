'Description: CaptionUtils class
'Authors: George Birbilis (birbilis@kagi.com)
'Version: 20090310

Imports System.Text
Imports LvS.models.Captions
Imports LvS.utilities.FileUtils

Imports LvS.utilities.Captions.tts
Imports LvS.utilities.Captions.srt
Imports LvS.utilities.Captions.fab
Imports LvS.utilities.Captions.encore

Namespace LvS.utilities.Captions

  Public NotInheritable Class CaptionUtils

    Public Const EXTENSION_TTS As String = ".TTS"
    Public Const EXTENSION_SRT As String = ".SRT"
    Public Const EXTENSION_FAB As String = ".FAB" 'Note: Adobe ENCORE uses .TXT file extension for this
    Public Const EXTENSION_ENCORE As String = ".ENC"  'Note: Adobe ENCORE uses .TXT file extension for this

    Public Shared ReadOnly EXTENSIONS_TTS As String() = {EXTENSION_TTS}
    Public Shared ReadOnly EXTENSIONS_SRT As String() = {EXTENSION_SRT}
    Public Shared ReadOnly EXTENSIONS_FAB As String() = {EXTENSION_FAB}
    Public Shared ReadOnly EXTENSIONS_ENCORE As String() = {EXTENSION_ENCORE}

    Public Shared Function GetCaptionsReader(ByVal path As String) As ICaptionsReader
      If CheckExtension(path, EXTENSIONS_TTS) IsNot Nothing Then
        Return New TTSReader
      ElseIf CheckExtension(path, EXTENSIONS_SRT) IsNot Nothing Then
        Return New SRTReader
      End If
      Return Nothing
    End Function

    Public Shared Function GetCaptionsWriter(ByVal path As String) As ICaptionsWriter
      If CheckExtension(path, EXTENSIONS_TTS) IsNot Nothing Then
        Return New TTSUnicodeWriter
      ElseIf CheckExtension(path, EXTENSIONS_SRT) IsNot Nothing Then
        Return New SRTWriter
      ElseIf CheckExtension(path, EXTENSIONS_FAB) IsNot Nothing Then
        Return New FABWriter
      ElseIf CheckExtension(path, EXTENSIONS_ENCORE) IsNot Nothing Then
        Return New EncoreWriter
      End If
      Return Nothing
    End Function

    Public Shared Sub ReadCaptions(ByVal Captions As ICaptions, ByVal path As String, ByVal theEncoding As Encoding)
      Dim reader As ICaptionsReader = GetCaptionsReader(path)
      If reader IsNot Nothing Then reader.ReadCaptions(Captions, path, theEncoding)
    End Sub

    Public Shared Sub WriteCaptions(ByVal Captions As ICaptions, ByVal path As String, ByVal theEncoding As Encoding)
      Dim writer As ICaptionsWriter = GetCaptionsWriter(path)
      If writer IsNot Nothing Then writer.WriteCaptions(Captions, path, theEncoding)
    End Sub

  End Class

End Namespace
