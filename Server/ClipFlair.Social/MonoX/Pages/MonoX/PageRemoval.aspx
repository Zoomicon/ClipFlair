<%@ Page Title="" Language="C#" MasterPageFile="~/MonoX/MasterPages/Default.master"
    AutoEventWireup="true" CodeBehind="PageRemoval.aspx.cs" Inherits="MonoSoftware.MonoX.Pages.PageRemoval"
    %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="main">
        <!-- Main Start -->        
        <table class="error-message-page" cellpadding="0" cellspacing="0" style="width: 800px;
            margin: 0px auto;">
            <asp:PlaceHolder ID="panSuccess" runat="server">
                <tr>
                    <td valign="top">
                        <img src="<%= ResolveUrl(MonoSoftware.MonoX.Paths.App_Themes.All.Common.img.check_icon_png) %>" alt="Success" />
                    </td>
                    <td valign="middle" style="padding: 10px;">
                        <h2><%= AdminResources.PageManagerPartAdmin_DeleteSuccess%></h2>                        
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="panNotFound" runat="server">
                <tr>
                    <td valign="top">
                        <img src="<%= ResolveUrl(MonoSoftware.MonoX.Paths.App_Themes.img.Error_png) %>" alt="Warning" />
                    </td>
                    <td valign="middle" style="padding: 10px;">
                        <h2><%= ErrorMessages.PageNotFound %></h2>                        
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="panWarning" runat="server">
                <tr>
                    <td valign="top">
                        <img src="<%= ResolveUrl(MonoSoftware.MonoX.Paths.App_Themes.img.Error_png) %>" alt="Warning" />
                    </td>
                    <td valign="middle" style="padding: 10px;">
                        <h2><%= ErrorMessages.PageNotFound %>&nbsp;-&nbsp;<%= ErrorMessages.PageNotFoundRemovePersonalization %></h2>
                        <span><%= ErrorMessages.PageNotFoundPersonalizationFound %></span><br />                    
                    </td>
                </tr>
                <tr>
                    <td>
                    </td> 
                    <td>                    
                        <MonoX:StyledButton ID="btnYes" runat="server" Text="<%$ Code:GlobalText.Yes %>" CssClass="float-left" /><MonoX:StyledButton ID="btnNo" runat="server" Text="<%$ Code:GlobalText.No %>" CssClass="float-left" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td></td>
                <td>
                    <%= MonoSoftware.MonoX.Resources.ErrorMessages.ToContinueWorkingWithApplication %><strong>
                        <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/"><%= ErrorMessages.ToContinueWorkingWithApplicationClickHere%></asp:HyperLink></strong>
                </td>
            </tr>
        </table>        
    </div>
</asp:Content>
