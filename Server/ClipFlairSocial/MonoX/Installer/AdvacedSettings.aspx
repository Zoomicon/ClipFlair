<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/Installer/Installer.master" 
    AutoEventWireup="true" 
    Inherits="MonoX_Installer_AdvacedSettings" 
    Title="License Agreement" 
    Theme="Installer"
    Codebehind="AdvacedSettings.aspx.cs" %>
<%@ MasterType VirtualPath="~/MonoX/Installer/Installer.master" %>    
<%@ Register Src="~/MonoX/Admin/controls/PortalSettings.ascx" TagPrefix="monox" TagName="PortalSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp_hd" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp_cnt" Runat="Server">

    <div class="installer-advanced-settings" >
        <monox:PortalSettings ID="portalSettings" runat="server" ShowButtons="false" />
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_ft" Runat="Server">
</asp:Content>

