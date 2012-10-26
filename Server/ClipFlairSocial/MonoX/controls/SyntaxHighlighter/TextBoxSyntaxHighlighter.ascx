<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextBoxSyntaxHighlighter.ascx.cs"
    Inherits="MonoSoftware.MonoX.Controls.TextBoxSyntaxHighlighter" %>
<%@ Register Src="~/MonoX/controls/SyntaxHighlighter/SyntaxHighlighter.ascx" TagPrefix="mono"
    TagName="SyntaxHighlighter" %>
<%@ Register tagprefix="telerik" namespace = "Telerik.Web.UI" assembly="Telerik.Web.UI" %>      

<mono:SyntaxHighlighter ID="syntaxHighlighter" runat="server" />
<telerik:RadDialogOpener runat="server" ID="dOpener"></telerik:RadDialogOpener>

<script type="text/javascript">
    function fcbFunc_<%= this.ClientID %>(sender, args) {
        if (args)
        {
            var selectedItem = args.get_value();
            monox_sh_InsertTag('<%= TextBoxClientID %>', selectedItem);
        }
    }
</script>

<MonoX:StyledButton ID="btnInsertCode" runat="server" CssClass="discussion-styled-button" />


