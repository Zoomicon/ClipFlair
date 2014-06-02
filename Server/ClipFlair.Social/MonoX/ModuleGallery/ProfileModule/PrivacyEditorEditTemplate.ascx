<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.PrivacyManager.PrivacyEditorEditTemplate, MonoX" %>
<div class="privacy-box <%= SelectedPrivacyLevel %>">
    <div class="privacy-header">        
        <span style="float: left;"><asp:Literal ID="labTitle" runat="server"></asp:Literal></span>
    </div>
    <div class="privacy-main">
        <asp:RadioButtonList ID="chkPrivacyLevels" runat="server">
        </asp:RadioButtonList>
    </div>    
</div>
