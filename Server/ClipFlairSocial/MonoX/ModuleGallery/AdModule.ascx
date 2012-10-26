<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.AdModule" 
    Codebehind="AdModule.ascx.cs" %>
    
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %> 

<div class="ad-module">    
    <mono:MonoAdRotator ID="monoAdRotator" runat="server" Target="_blank" >
    </mono:MonoAdRotator>
</div>