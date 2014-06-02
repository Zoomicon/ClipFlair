<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/Installer/Installer.master" 
    AutoEventWireup="true" 
    Inherits="MonoX_Installer_Default" 
    Title="Welcome" 
    Theme="Installer"
    Codebehind="Default.aspx.cs" %>
<%@ MasterType VirtualPath="~/MonoX/Installer/Installer.master" %>    
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>   
<%@ Register Src="~/MonoX/Installer/PermissionCheckBox.ascx" TagPrefix="monox" TagName="PermissionCheckBox" %>    
    
    
<asp:Content ID="Content1" ContentPlaceHolderID="cp_hd" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp_cnt" Runat="Server">

    <div class="installer_default">
        <div>
            <h5><asp:Literal ID="labRequirements" runat="server" Text='<%$ Code: InstallerResources.Default_labRequirements %>'></asp:Literal></h5>
        </div>
        <div>
            <ul>
                <li>
                    <b><asp:Literal ID="labMSNet" runat="server" Text='<%$ Code: InstallerResources.Default_labMSNet %>'></asp:Literal></b><br />
                    <asp:Label ID="labMSNetInfo" runat="server" CssClass="item_info" Text='<%$ Code: InstallerResources.Default_labMSNetInfo %>'></asp:Label><br />                    
                    <asp:HyperLink ID="linkMSNet1" runat="server" NavigateUrl="http://msdn.microsoft.com/en-us/library/vstudio/w0x726c2(v=vs.100).aspx" Text='<%$ Code: InstallerResources.Default_linkMSNet40 %>' Target="_blank"></asp:HyperLink>                    
                    <asp:HyperLink ID="linkMSNet2" runat="server" NavigateUrl="http://msdn.microsoft.com/en-us/library/vstudio/w0x726c2.aspx" Text='<%$ Code: InstallerResources.Default_linkMSNet45 %>' Target="_blank"></asp:HyperLink>                    
                </li>
                <li>
                    <b><asp:Literal ID="labIIS" runat="server" Text='<%$ Code: InstallerResources.Default_labIIS %>'></asp:Literal></b><br />
                    <asp:Label ID="labIISInfo" runat="server" CssClass="item_info" Text='<%$ Code: InstallerResources.Default_labIISInfo %>'></asp:Label><br />
                    <asp:HyperLink ID="linkIIS" runat="server" NavigateUrl="http://www.microsoft.com/technet/prodtechnol/WindowsServer2003/Library/IIS/750d3137-462c-491d-b6c7-5f370d7f26cd.mspx?mfr=true" Text='<%$ Code: InstallerResources.Default_linkIIS %>' Target="_blank"></asp:HyperLink>
                </li>
                <li>
                    <b><asp:Literal ID="labSQL" runat="server" Text='<%$ Code: InstallerResources.Default_labSQL %>'></asp:Literal></b>
                </li>
                <li>
                    <b><%= String.Format(InstallerResources.InstallComplete_Note, System.Security.Principal.WindowsIdentity.GetCurrent().Name) %></b>
                </li>
            </ul>
        </div>
    </div>
    <div class="installer_default permissions">        
        <asp:Repeater ID="rptPermissions" runat="server">
            <ItemTemplate>
                <div class="permissions-box">
                    <monox:PermissionCheckBox id="permissionCheckBox" runat="server"></monox:PermissionCheckBox>
                </div>
            </ItemTemplate>
        </asp:Repeater>        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_ft" Runat="Server">
</asp:Content>

