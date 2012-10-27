<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.PasswordRecovery" 
    Theme="Default"
    Codebehind="PasswordRecovery.aspx.cs" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Register TagPrefix="MonoX" TagName="PasswordRecovery" Src="~/MonoX/ModuleGallery/Membership/PasswordRecovery.ascx" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
<!-- Main Start -->
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>        
            <td class="left-section">
                <MonoX:PasswordRecovery runat="server" ID="ctlPasswordRecovery"  />
            </td>
        </tr>
    </table>
</asp:Content>
