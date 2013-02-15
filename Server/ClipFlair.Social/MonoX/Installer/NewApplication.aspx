<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/Installer/Installer.master" 
    AutoEventWireup="true" 
    Inherits="MonoX_Installer_NewApplication" 
    Title="New application info" 
    Theme="Installer"
    Codebehind="NewApplication.aspx.cs" %>
<%@ MasterType VirtualPath="~/MonoX/Installer/Installer.master" %>    
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="cp_hd" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp_cnt" Runat="Server">

    <div class="installer_dbinfo">
        <div>
            <asp:Literal ID="labSubTitle" runat="server" Text='<%$ Code: InstallerResources.NewApplication_labSubTitle %>'></asp:Literal>
        </div>
        <div class="installer_data">            
            <div class="installer_editform">
                <p>
                    <asp:Label ID="labNewAppName" runat="server" AssociatedControlID="txtNewAppName" Text='<%$ Code: InstallerResources.NewApplication_labNewAppName %>'></asp:Label>
                </p>
                <p>
                    <asp:TextBox ID="txtNewAppName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredNewApp" runat="server" CssClass="ValidatorAdapter" ControlToValidate="txtNewAppName" SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="labCloneAppName" runat="server" AssociatedControlID="ddlCloneAppName" Text='<%$ Code: InstallerResources.NewApplication_labCloneAppName %>'></asp:Label>
                </p>
                <p>
                    <asp:DropDownList ID="ddlCloneAppName" runat="server"></asp:DropDownList>
                </p>
                <br/>
                <asp:CheckBox ID="chkExistingMembershipProvider" runat="server" Text='<%$ Code: InstallerResources.NewApplication_chkExistingMembershipProvider %>'></asp:CheckBox>
                <br/>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_ft" Runat="Server">
</asp:Content>

