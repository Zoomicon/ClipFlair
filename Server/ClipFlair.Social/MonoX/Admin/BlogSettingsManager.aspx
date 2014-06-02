<%@ Assembly Name="MonoSoftware.MonoX.DAL" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.EntityClasses" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlogSettingsManager.aspx.cs" Inherits="MonoSoftware.MonoX.Admin.BlogSettingsManager"
MasterPageFile="~/MonoX/MasterPages/AdminDefault.master" Theme="DefaultAdmin" Title="Blog management" %>

<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %> 
<%@ Register TagPrefix="mono" Namespace="MonoSoftware.Web.LiteGrid" Assembly="MonoSoftware.Web.LiteGrid" %>
<%@ Register Src="~/MonoX/Admin/controls/GridViewEditBox.ascx" TagPrefix="monox"
    TagName="GridViewEditBox" %>
<%@ Register Src="~/MonoX/ModuleGallery/Blog/BlogSettings.ascx" TagPrefix="mono" TagName="BlogSettings" %>
<%@ MasterType VirtualPath="~/MonoX/MasterPages/AdminDefault.master" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">

<asp:ScriptManagerProxy ID="gridScriptManager" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/MonoX/Admin/controls/scripts/JSHelper.js" ScriptMode="Auto" />
    </Scripts>
</asp:ScriptManagerProxy>

<div class="AdminContainer">
    <monox:GridViewEditBox ID="gridViewBox" runat="server" ShowTopActions="true" ValidationGroup="Blog">
        <CustomFilterTemplate>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="display: none;">
                        <%-- Note: Hidden but left for possible future use --%>
                        <asp:Literal ID="labAppName" runat="server" Text='<%$ Code: AdminResources.Label_Application %>'></asp:Literal>&nbsp;
                        <asp:DropDownList ID="ddlApps" runat="server" AutoPostBack="true" CssClass="searchselect"
                            OnSelectedIndexChanged="ddlApps_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>                    
                </tr>                
            </table>
        </CustomFilterTemplate>
        <Columns>
            <mono:LiteGridBoundField DataField="Id" Visible="false" />
            <mono:LiteGridBoundField DataField="Name" HeaderText='<%$ Code: AdminResources.BlogSettingsManager_colName %>' SortExpression="Name" />
            <mono:LiteGridBoundField DataField="DateCreated" HeaderText='<%$ Code: AdminResources.BlogSettingsManager_colDateCreated %>' SortExpression="DateCreated" />
            <mono:LiteGridBoundField DataField="AspnetUser.UserName" HeaderText='<%$ Code: AdminResources.BlogSettingsManager_colCreatedByUserName %>' SortExpression="UserName" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="labNoData" runat="server" Text='<%$ Code: AdminResources.BlogSettingsManager_labNoData %>'></asp:Label>
        </EmptyDataTemplate>
        <CustomActionsTemplate>            
        </CustomActionsTemplate>
        <ContentTemplate>
            <asp:PlaceHolder ID="plhModification" runat="server">
            <div  class="AdminGridFooterContent">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <mono:BlogSettings runat="server" ID="ctlBlogSettings" ShowActions="false"></mono:BlogSettings>
                        </td>
                    </tr>
                </table>
            </div>
            </asp:PlaceHolder>
        </ContentTemplate>
    </monox:GridViewEditBox>
</div>
</asp:Content>
