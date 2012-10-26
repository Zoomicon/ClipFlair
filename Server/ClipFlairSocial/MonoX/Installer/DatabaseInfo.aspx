<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/Installer/Installer.master" 
    AutoEventWireup="true" 
    Inherits="MonoX_Installer_DatabaseInfo" 
    Title="Database Info" 
    Theme="Installer"
    Codebehind="DatabaseInfo.aspx.cs" %>
<%@ MasterType VirtualPath="~/MonoX/Installer/Installer.master" %>    
<%@ Register TagPrefix="radcb" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>        
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>   
    
<asp:Content ID="Content1" ContentPlaceHolderID="cp_hd" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp_cnt" Runat="Server">

    <div class="installer_dbinfo">
        <div>
            <asp:Literal ID="labSubTitle" runat="server" Text='<%$ Code: InstallerResources.DatabaseInfo_labSubTitle %>'></asp:Literal>
        </div>
        <div class="installer_data">
            <div class="installer_editform">
                <p>
                    <radcb:RadComboBox                
                    ID="ddlDBs"
                    Runat="server"
                    EnableItemCaching="false"                    
                    AllowCustomText="false"                                                     
                    EnableLoadOnDemand="True"
                    MarkFirstMatch="false"      
                    ShowDropDownOnTextboxClick="false"
                    ShowToggleImage="true"      
                    ShowWhileLoading="true"                                                              
                    Width="98%"
                    Height="150px"
                    OnClientItemsRequesting="RequestingHandler"
                    OnClientItemsRequested="OnClientItemsRequested"
                    >
                    </radcb:RadComboBox>
                    <asp:RequiredFieldValidator ID="requireDBName" runat="server" CssClass="ValidatorAdapter" ControlToValidate="ddlDBs" SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
                </p>
            </div>
            <div class="installer_editform">
                <asp:RadioButton ID="btnCreateNew" runat="server" CssClass="installer_option" GroupName="ActionType" Text='<%$ Code: InstallerResources.DatabaseInfo_btnCreateNew %>'></asp:RadioButton><br />
                <p class="installer_note">
                    <asp:Literal ID="labNote1" runat="server" Text='<%$ Code: InstallerResources.DatabaseInfo_labNote1 %>'></asp:Literal>
                </p>
                <asp:RadioButton ID="btnExisting" runat="server"  CssClass="installer_option" GroupName="ActionType" Text='<%$ Code: InstallerResources.DatabaseInfo_btnExisting %>'></asp:RadioButton>
                <p class="installer_note">
                    <asp:Literal ID="labNote2" runat="server" Text='<%$ Code: InstallerResources.DatabaseInfo_labNote2 %>'></asp:Literal>
                </p>
                <br/>
                <asp:RadioButton ID="btnMultiApp" runat="server"  CssClass="installer_option" GroupName="ActionType" Text='<%$ Code: InstallerResources.DatabaseInfo_btnMultiApp %>' Visible="false"></asp:RadioButton>
                <p class="installer_note">
                    <asp:Literal ID="labNote3" runat="server" Text='<%$ Code: InstallerResources.DatabaseInfo_labNote3 %>' Visible="false"></asp:Literal>
                </p>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_ft" Runat="Server">
</asp:Content>

