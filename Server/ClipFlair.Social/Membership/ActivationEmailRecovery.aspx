<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.ActivationEmailRecovery" 
    Theme="Default"
    Codebehind="ActivationEmailRecovery.aspx.cs" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Register TagPrefix="MonoX" TagName="ActivationEmailRecovery" Src="~/MonoX/ModuleGallery/Membership/ActivationEmailRecovery.ascx" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>        
            <td class="left-section">
                <MonoX:ActivationEmailRecovery runat="server" ID="ctlActivationEmailRecovery" />
            </td>
        </tr>
    </table>
</asp:Content>
