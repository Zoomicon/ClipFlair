<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/Installer/Installer.master" 
    AutoEventWireup="true" 
    Inherits="MonoX_Installer_License" 
    Title="License Agreement" 
    Theme="Installer"
    Codebehind="License.aspx.cs" %>
<%@ MasterType VirtualPath="~/MonoX/Installer/Installer.master" %>    
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp_hd" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp_cnt" Runat="Server">

    <div class="installer_license">
        <div>
            <iframe id="txtLicense" runat="server" class="installer_textarea" scrolling="auto" ></iframe>
        </div>
        <div class="installer_options">
            <div style="float:left;">
                <asp:HyperLink ID="btnPrintable" runat="server" CssClass="installer_printable" Target="_blank" Text='<%$ Code: InstallerResources.License_btnPrintable %>'></asp:HyperLink>
            </div>
            <div style="float:right;">
                <asp:CheckBox ID="chkAgree" runat="server" Text='<%$ Code: InstallerResources.License_chkAgree %>' />
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_ft" Runat="Server">
</asp:Content>

