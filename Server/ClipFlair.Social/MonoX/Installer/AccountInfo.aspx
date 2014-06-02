<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/Installer/Installer.master" 
    AutoEventWireup="true" 
    Inherits="MonoX_Installer_AccountInfo" 
    Title="Database Info" 
    Theme="Installer"
    Codebehind="AccountInfo.aspx.cs" %>
<%@ MasterType VirtualPath="~/MonoX/Installer/Installer.master" %>    
<%@ Register Assembly="MonoSoftware.Web" Namespace="MonoSoftware.Web.Controls" TagPrefix="mono" %>
<%@ Register TagPrefix="radcb" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>        
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %> 
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>   
    
<asp:Content ID="Content1" ContentPlaceHolderID="cp_hd" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp_cnt" Runat="Server">

    <div class="installer_dbinfo">
        <div>
            <asp:Literal ID="labSubTitle" runat="server" Text='<%$ Code: InstallerResources.AccountInfo_labSubTitle %>'></asp:Literal>
        </div>
        <div class="installer_data">
            <div class="installer_editform">
                <p>
                    <strong><%= InstallerResources.AccountInfo_UsernameNote %></strong>
                </p>
                <p>
                    <asp:Label ID="labUsername" runat="server" AssociatedControlID="txtUsername" Text='<%$ Code: InstallerResources.AccountInfo_labUsername %>'></asp:Label>                    
                </p>
                <p>
                    <asp:TextBox ID="txtUsername" runat="server" Text="admin"></asp:TextBox>                    
                    <asp:RequiredFieldValidator ID="requiredUserName" runat="server" CssClass="ValidatorAdapter"
                        SetFocusOnError="true" ControlToValidate="txtUsername" Text="*" ErrorMessage='<%$ Code: AdminResources.UserManager_requiredUserName %>'></asp:RequiredFieldValidator>
                    <mono:regexvalidator id="validateUserName" runat="server" cssclass="ValidatorAdapter"
                        setfocusonerror="true" controltovalidate="txtUsername" text="!" errormessage='<%$ Code: AdminResources.UserManager_validateUserName %>'>
                    </mono:regexvalidator>
                </p>
                <p>
                    <asp:Label ID="labPassword" runat="server" AssociatedControlID="txtPassword" Text='<%$ Code: InstallerResources.AccountInfo_labPassword %>'></asp:Label>
                </p>
                <p>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredPassword" runat="server" SetFocusOnError="true"
                        CssClass="ValidatorAdapter" ControlToValidate="txtPassword" Text="*" ErrorMessage='<%$ Code: AdminResources.UserManager_requiredPassword %>'></asp:RequiredFieldValidator>
                    <mono:regexvalidator id="validatePassword" runat="server" setfocusonerror="true"
                        cssclass="ValidatorAdapter" controltovalidate="txtPassword" validationtype="alphaNumericWithSymbolsAllowedSpecialChr" text="!" errormessage='<%$ Code: AdminResources.UserManager_validatePassword %>'>
                    </mono:regexvalidator>
                </p>
                <p>
                    <asp:Label ID="labEmail" runat="server" AssociatedControlID="txtEMail" Text='<%$ Code: InstallerResources.AccountInfo_labEmail %>'></asp:Label>
                </p>
                <p>
                    <asp:TextBox ID="txtEMail" runat="server"></asp:TextBox>
                    <mono:RegExValidator id="validateEmail" runat="server" setfocusonerror="true" cssclass="ValidatorAdapter"
                        controltovalidate="txtEMail" validationtype="eMail"
                        text="!" errormessage='<%$ Code: InstallerResources.AccountInfo_validateEmail %>'></mono:RegExValidator>
                </p>
            </div>            
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_ft" Runat="Server">
</asp:Content>

