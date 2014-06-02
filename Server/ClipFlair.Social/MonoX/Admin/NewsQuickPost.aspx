<%@ Page Title="" Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master"
    Theme="DefaultAdmin" AutoEventWireup="true" CodeBehind="NewsQuickPost.aspx.cs"
    Inherits="MonoSoftware.MonoX.Admin.NewsQuickPost" %>

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.HelperClasses" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/MonoX/controls/CustomRadEditor.ascx" TagPrefix="mono" TagName="CustomRadEditor" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %>
<%@ MasterType VirtualPath="~/MonoX/MasterPages/AdminDefault.master" %>
<asp:Content ID="cp" ContentPlaceHolderID="cp" runat="server">
    <div class="AdminContainer">
        <div style="padding: 15px;">
            <asp:UpdatePanel ID="upGridBox" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="panMain" runat="server">
                        <div class="AdminHeaderTop">
                        </div>
                        <div class="AdminHeaderBottom">
                            <strong>
                                <%= MonoSoftware.MonoX.Resources.NewsAdmin.Global_Title%></strong>
                        </div>
                        <div style="width: 989px;">
                            <div class="AdminGridFooterContent">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <strong><asp:Literal ID="labNoCategories" runat="server"></asp:Literal></strong>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="NewsItems"
                                                ShowSummary="true" />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;">
                                            <asp:Literal ID="labCategories" runat="server"></asp:Literal>
                                        </td>
                                        <td style="width: 80%;">
                                            <rad:RadComboBox ID="ddlCategories" runat="server" Width="100%">
                                            </rad:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr class="input-form">
                                        <td>
                                            <asp:Label ID="labNewsTitle" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNewsTitle" runat="server" Width="99%"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ID="requiredNewsTitle" runat="server" CssClass="ValidatorAdapter"
                                                ValidationGroup="NewsItems" ControlToValidate="txtNewsTitle" SetFocusOnError="true"
                                                Display="Static" Text="!" ErrorMessage='<%$ Code: AdminResources.NewsQuickPost_RequiredTitle %>'></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="labNewsContent" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <mono:CustomRadEditor ID="radNewsContent" EditorHeight="400px" AutoResizeHeight="False"
                                                ToolBarMode="ShowOnFocus" runat="server" EnableEmbeddedSkins="true" Skin="Default" ></mono:CustomRadEditor>                                            
                                        </td>
                                    </tr>
                                </table>
                                <b style="color: Red;">
                                    <asp:Label ID="labMessage" runat="server"></asp:Label></b>
                            </div>
                        </div>
                        <div class="AdminGridFooterTop">
                            <div class="Inner">
                                <asp:Button ID="btnSave" runat="server" CausesValidation="true" UseSubmitBehavior="false"
                                    CssClass="AdminButton" Style="float: left;" ValidationGroup="NewsItems" />
                                <div style="float: right;">
                                    <b style="color: Green;">
                                        <asp:Label ID="labInfoMessage" runat="server" EnableViewState="false"></asp:Label></b>
                                </div>
                            </div>
                        </div>
                        <div class="AdminGridFooterBottom">
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
