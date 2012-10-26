<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Content.aspx.cs"
    Inherits="MonoSoftware.MonoX.Mobile.Content"
    MasterPageFile="~/MonoX/MasterPages/Mobile/Default.master"
    Theme="Mobile"
%>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>  
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <h1><asp:literal ID="ltlTitle" runat="server"></asp:literal></h1>
    <MonoX:Editor runat="server" ID="ctlEditor" Title="Editor" DefaultDocumentTitle="" EditButtonVisible="true"></MonoX:Editor>
</asp:Content>