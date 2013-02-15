<%@ Page Language="C#" MasterPageFile="~/MonoX/Installer/Installer.master" AutoEventWireup="true"
    Inherits="MonoX_Installer_SQLInfo" Title="Database Server Info" Theme="Installer"
    CodeBehind="SQLInfo.aspx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>
<%@ MasterType VirtualPath="~/MonoX/Installer/Installer.master" %>
<%@ Register TagPrefix="radcb" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp_hd" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp_cnt" runat="Server">
    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
        <ContentTemplate>
            <div class="installer_sqlinfo">
                <div>
                    <asp:Literal ID="labSubTitle" runat="server" Text='<%$ Code: InstallerResources.SQLInfo_labSubTitle %>'></asp:Literal>
                </div>
                <div class="installer_data">
                    <div class="installer_editform">
                        <p>
                            <asp:Label ID="labSQLServerName" runat="server" AssociatedControlID="ddlSQLServers"
                                Text='<%$ Code: InstallerResources.SQLInfo_labSQLServerName %>'></asp:Label>
                        </p>
                        <p>
                            <radcb:RadComboBox ID="ddlSQLServers" runat="server" EnableItemCaching="false" AllowCustomText="true"
                                EnableLoadOnDemand="True" MarkFirstMatch="true" ShowDropDownOnTextboxClick="false"
                                ShowToggleImage="true" ShowWhileLoading="true" Width="98%" Height="150px" OnClientItemsRequesting="RequestingHandler"
                                OnClientItemsRequested="OnClientItemsRequested">
                            </radcb:RadComboBox>
                            <asp:RequiredFieldValidator ID="requiredServerName" runat="server" CssClass="ValidatorAdapter" ControlToValidate="ddlSQLServers"
                                SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
                        </p>
                    </div>
                    <div class="installer_editform">
                        <asp:RadioButtonList ID="rblAuth" runat="server" RepeatDirection="Vertical" CssClass="installer_option"
                            AutoPostBack="true" OnSelectedIndexChanged="rblAuth_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Text='<%$ Code: InstallerResources.SQLInfo_winAuthTitle %>' Value="0"></asp:ListItem>
                            <asp:ListItem Text='<%$ Code: InstallerResources.SQLInfo_sqlAuthTitle %>' Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div id="panSQLAuthInfo" runat="server" class="installer_editform">
                        <p>
                            <asp:Label ID="labUsername" runat="server" AssociatedControlID="txtUsername" Text='<%$ Code: InstallerResources.SQLInfo_labUsername %>'></asp:Label>
                        </p>
                        <p>
                            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                        </p>
                        <p>
                            <asp:Label ID="labPassword" runat="server" AssociatedControlID="txtPassword" Text='<%$ Code: InstallerResources.SQLInfo_labPassword %>'></asp:Label>
                        </p>
                        <p>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </p>
                    </div>
                </div>
                <div style="padding-top: 40px;" class="installer_note">
                    <asp:Literal ID="labNote" runat="server" Text='<%$ Code: String.Format(InstallerResources.SQLInfo_labNote, System.Security.Principal.WindowsIdentity.GetCurrent().Name) %>'></asp:Literal>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_ft" runat="Server">
</asp:Content>
