﻿'Project: ClipFlair (http://ClipFlair.codeplex.com)
'Filename: CaptionUtils.vb
'Version: 20121123

Imports ClipFlair.CaptionsLib.Models
Imports ClipFlair.CaptionsLib.TTS
Imports ClipFlair.CaptionsLib.SRT
Imports ClipFlair.CaptionsLib.FAB
Imports ClipFlair.CaptionsLib.Encore
Imports ClipFlair.CaptionsLib.Utils.FileUtils

Imports System.Text
Imports Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions

Namespace ClipFlair.CaptionsLib.Utils

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
        Return New TTSWriter
      ElseIf CheckExtension(path, EXTENSIONS_SRT) IsNot Nothing Then
        Return New SRTWriter
      ElseIf CheckExtension(path, EXTENSIONS_FAB) IsNot Nothing Then
        Return New FABWriter
      ElseIf CheckExtension(path, EXTENSIONS_ENCORE) IsNot Nothing Then
        Return New EncoreWriter
      End If
      Return Nothing
    End Function

    Public Shared Sub ReadCaptions(Of T As {CaptionElement, New})(ByVal captions As CaptionRegion, ByVal path As String, ByVal theEncoding As Encoding)
      Dim reader As ICaptionsReader = GetCaptionsReader(path)
      If reader IsNot Nothing Then reader.ReadCaptions(Of T)(captions, path, theEncoding)
    End Sub

    Public Shared Sub WriteCaptions(ByVal captions As CaptionRegion, ByVal path As String, ByVal theEncoding As Encoding)
      Dim writer As ICaptionsWriter = GetCaptionsWriter(path)
      If writer IsNot Nothing Then writer.WriteCaptions(captions, path, theEncoding)
    End Sub

  End Class

End Namespace
