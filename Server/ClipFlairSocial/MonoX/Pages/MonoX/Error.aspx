<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.Error" 
    Theme="Default" 
    Codebehind="Error.aspx.cs" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
<div class="main"> <!-- Main Start -->
    <table class="error-message-page" cellpadding="0" cellspacing="0" style="width: 700px; margin: 0px auto;">
        <tr>
            <td valign="top"><img src="<%= ResolveUrl(MonoSoftware.MonoX.Paths.App_Themes.img.Error_png) %>" alt="Error" /></td>
            <td valign="middle" style="padding: 10px;">
                <h2><%= Title %></h2>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <span class="description"><%= Description %></span>
                <br /><br />
                <span>
                    <%= MonoSoftware.MonoX.Resources.ErrorMessages.ToContinueWorkingWithApplication %><strong>
                    <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/"></asp:HyperLink></strong>
                </span>
            </td>
        </tr>
    </table>
</div>    
</asp:Content>
