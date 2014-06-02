<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.ModuleGallery.MonoXHtmlEditor" Codebehind="MonoXHtmlEditor.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/MonoX/controls/MonoXRating.ascx" TagPrefix="monox" TagName="MonoXRating" %>
<%@ Register Src="~/MonoX/controls/SyntaxHighlighter/SyntaxHighlighter.ascx" TagPrefix="mono" TagName="SyntaxHighlighter" %>

<div class="html-editor">
    <mono:SyntaxHighlighter ID="syntaxHighlighter" runat="server" />
    <asp:Panel ID="pnlEditor" runat="server">
    <asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
    <div><asp:TextBox runat="server" ID="txtTitle"></asp:TextBox></div>
    <telerik:radeditor AutoResizeHeight="true" 
    ID="htmlEditor"
    Runat="server"
    Width="100%"
    Skin="Metro"
    OnClientCommandExecuting="OnClientCommandExecuting"
    ContentAreaMode="Div"
    
    EditModes="Design, Html"
    ToolsFile="~/MonoX/ModuleGallery/MonoXHtmlEditorToolsFile.xml"
    EnableViewState="true">
    </telerik:radeditor>
    </asp:Panel>
    <asp:Literal ID="lblText" runat="server"></asp:Literal>
    <MonoX:StyledButton ID="btnEdit" CommandName="Login" runat="server" EnableNativeButtonMode="true" Visible="false"></MonoX:StyledButton>    
    <asp:Panel ID="pnlButtonHolder" runat="server">
        <asp:Button id="btnMonoXHtmlEditor" runat="server" Text="Save" UseSubmitBehavior="false"></asp:Button>   
    </asp:Panel>

    <monox:MonoXRating id="rating" runat="server"></monox:MonoXRating>
    <asp:PlaceHolder ID="panTellAFriend" runat="server">
    </asp:PlaceHolder>
</div>