<%@ Assembly Name="MonoSoftware.MonoX.DAL" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.EntityClasses" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>

<%@ Page Title="User management" Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master"
    AutoEventWireup="True" CodeBehind="AdManager.aspx.cs" Inherits="MonoSoftware.MonoX.Admin.AdManager"
    Theme="DefaultAdmin" %>

<%@ Register Src="~/MonoX/controls/DualListBox.ascx" TagPrefix="mono" TagName="DualListBox" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %>
<%@ Register Assembly="MonoSoftware.Web" Namespace="MonoSoftware.Web.Controls" TagPrefix="mono" %>
<%@ Register TagPrefix="mono" Namespace="MonoSoftware.Web.LiteGrid" Assembly="MonoSoftware.Web.LiteGrid" %>
<%@ Register Src="~/MonoX/Admin/controls/GridViewEditBox.ascx" TagPrefix="monox"
    TagName="GridViewEditBox" %>
<%@ Register Src="~/MonoX/ModuleGallery/UploadModule.ascx" TagPrefix="mono" TagName="UploadModule" %>
<%@ MasterType VirtualPath="~/MonoX/MasterPages/AdminDefault.master" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <asp:ScriptManagerProxy ID="gridScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/MonoX/Admin/controls/scripts/JSHelper.js" ScriptMode="Auto" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <div class="AdminContainer">
        <monox:GridViewEditBox ID="gridViewBox" runat="server" ShowTopActions="true" ValidationGroup="Modification">
            <CustomFilterTemplate>
            </CustomFilterTemplate>
            <Columns>
                <mono:LiteGridBoundField DataField="Id" Visible="false" />
                <mono:LiteGridBoundField DataField="Name" HeaderText='<%$ Code: AdminResources.AdAdmin_sortName %>' SortExpression="Name" />
                <mono:LiteGridBoundField DataField="Start" HeaderText='<%$ Code: AdminResources.AdAdmin_sortStart %>' SortExpression="Start" />
                <mono:LiteGridBoundField DataField="End" HeaderText='<%$ Code: AdminResources.AdAdmin_sortEnd %>' SortExpression="End" />
                <mono:LiteGridBoundField DataField="Active" HeaderText='<%$ Code: AdminResources.AdAdmin_sortActive %>' SortExpression="Active" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="labNoDataCamp" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labNoDataCamp %>'></asp:Label>
            </EmptyDataTemplate>
            <CustomActionsTemplate>
                <asp:Button ID="btnToggleActive" runat="server" UseSubmitBehavior="false" Text='<%$ Code: AdminResources.AdAdmin_btnToggleActive %>'
                    CssClass="AdminLargeButton" OnClick="btnToggleActive_Click" />
            </CustomActionsTemplate>
            <ContentTemplate>
            <div class="AdminGridFooterContent input-form">
                <table width="100%">
                    <tr>
                        <td>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="Modification"
                                        ShowSummary="true" />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" width="15%">
                                    <asp:Literal ID="labName" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labName %>'></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="input" Width="100%" ValidationGroup="Modification"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ValidatorAdapter" SetFocusOnError="true"
                                        ControlToValidate="txtName" ValidationGroup="Modification" ErrorMessage='<%$ Code: AdminResources.AdAdmin_requiredName %>' Text="!"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="labValidFrom" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labValidFrom %>'></asp:Literal>
                                </td>
                                <td>
                                    <rad:RadDatePicker ID="radValidFrom" runat="server" Calendar-Skin="Default2006">
                                    </rad:RadDatePicker>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="labValidTo" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labValidTo %>'></asp:Literal>
                                </td>
                                <td>
                                    <rad:RadDatePicker ID="radValidTo" runat="server" Calendar-Skin="Default2006">
                                    </rad:RadDatePicker>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Literal ID="labActive" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labActive %>'></asp:Literal>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkActive" runat="server" Width="100%" ValidationGroup="Modification">
                                    </asp:CheckBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                </table>        
            </div>                            
            </ContentTemplate>
        </monox:GridViewEditBox>
    </div>
    <div class="AdminContainer">
        <div style="padding-left: 20px;padding-right: 20px;">
            <asp:Label ID="labUploadTitle" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labUploadTitle %>' style="float: left;"></asp:Label>
            <div class="RadUploadContainer">
                <mono:UploadModule ID="uploadModule" runat="server"></mono:UploadModule>
            </div>
        </div>
    </div>
    <div class="AdminContainer">
        <monox:GridViewEditBox ID="gridViewBoxAd" runat="server" ShowTopActions="true" ValidationGroup="ModificationAd"
            UpdateMode="Always">
            <CustomFilterTemplate>
            </CustomFilterTemplate>
            <Columns>
                <mono:LiteGridBoundField DataField="Id" Visible="false" />
                <mono:LiteGridBoundField DataField="Caption" HeaderText='<%$ Code: AdminResources.AdAdmin_sortCaptionButton %>' SortExpression="Caption" />
                <mono:LiteGridBoundField DataField="Weight" HeaderText='<%$ Code: AdminResources.AdAdmin_sortWeightButton %>' SortExpression="Weight" />
                <mono:LiteGridBoundField DataField="ValidFrom" HeaderText='<%$ Code: AdminResources.AdAdmin_sortValidFromButton %>' SortExpression="ValidFrom" />
                <mono:LiteGridBoundField DataField="ValidTo" HeaderText='<%$ Code: AdminResources.AdAdmin_sortValidToButton %>' SortExpression="ValidTo" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="labNoData" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labNoData %>'></asp:Label>
            </EmptyDataTemplate>
            <CustomActionsTemplate>
            </CustomActionsTemplate>
            <ContentTemplate>
            <div class="AdminGridFooterContent input-form">
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="validationSummaryAd" runat="server" DisplayMode="List"
                                ValidationGroup="ModificationAd" ShowSummary="true" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width: 20%;">
                                        <asp:Literal ID="labCampaignEdit" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labCampaignEdit %>'></asp:Literal>
                                    </td>
                                    <td style="width: 80%;">
                                        <asp:DropDownList ID="ddlCampaignEdit" runat="server" CssClass="select" Width="100%"
                                            ValidationGroup="ModificationAd">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Literal ID="labCaption" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labCaption %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCaption" runat="server" CssClass="input" Width="100%" ValidationGroup="ModificationAd"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="requiredCaption" runat="server" SetFocusOnError="true" CssClass="ValidatorAdapter"
                                            ControlToValidate="txtCaption" ValidationGroup="ModificationAd" Text="!" ErrorMessage='<%$ Code: AdminResources.AdAdmin_requiredCaption %>'></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="labImageUrl" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labImageUrl %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMedia" runat="server" CssClass="select" Width="100%" ValidationGroup="ModificationAd">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Literal ID="labAdContent" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labAdContent %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdContent" runat="server" CssClass="input" TextMode="multiline"
                                            ValidationGroup="ModificationAd" Width="100%" Height="100px"></asp:TextBox>
                                        <br />
                                        <asp:Literal ID="labAdContentNote" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labAdContentNote %>'></asp:Literal>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Literal ID="labNavigateUrl" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labNavigateUrl %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNavigateUrl" runat="server" CssClass="input" Width="100%" ValidationGroup="ModificationAd"></asp:TextBox>
                                    </td>
                                    <td>
                                        <mono:RegExValidator ID="regExNavigateUrl" runat="server" SetFocusOnError="true" CssClass="ValidatorAdapter"
                                            ControlToValidate="txtNavigateUrl" ValidationType="uRL" ValidationGroup="ModificationAd" Text="!" ErrorMessage='<%$ Code: AdminResources.AdAdmin_regExNavigateUrl %>'></mono:RegExValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Literal ID="labKeyword" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labKeyword %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="input" Width="100%" ValidationGroup="ModificationAd"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Literal ID="labAlternateText" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labAlternateText %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAlternateText" runat="server" CssClass="input" Width="100%" ValidationGroup="ModificationAd"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Literal ID="labWeight" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labWeight %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWeight" runat="server" CssClass="input" Width="100%" ValidationGroup="ModificationAd"></asp:TextBox>
                                    </td>
                                    <td>
                                        <mono:RegExValidator ID="regExWeight" runat="server" SetFocusOnError="true" ControlToValidate="txtWeight" CssClass="ValidatorAdapter"
                                            ValidationGroup="ModificationAd" ValidationType="numericint32" Text="!" ErrorMessage='<%$ Code: AdminResources.AdAdmin_regExWeight %>'></mono:RegExValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="labValidFromAd" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labValidFromAd %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <rad:RadDatePicker ID="radValidFromAd" runat="server" Calendar-Skin="Default2006">
                                        </rad:RadDatePicker>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="labValidToAd" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labValidToAd %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <rad:RadDatePicker ID="radValidToAd" runat="server" Calendar-Skin="Default2006">
                                        </rad:RadDatePicker>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Literal ID="labLanguage" runat="server" Text='<%$ Code: AdminResources.AdAdmin_labLanguage %>'></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="left">
                                        <asp:DropDownList ID="ddlLanguages" runat="server" AutoPostBack="true" CssClass="select"
                                            Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <mono:DualListBox ID="lstPages" runat="server" LeftWidth="350" LeftRows="10" RightWidth="350"
                                            RightRows="10" DualListTextCssClass="select" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>      
            </div>                              
            </ContentTemplate>
        </monox:GridViewEditBox>
    </div>
</asp:Content>
