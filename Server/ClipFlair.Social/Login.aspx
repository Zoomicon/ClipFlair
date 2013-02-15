<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Login.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.Default" 
    Theme="Default" 
    Codebehind="Default.aspx.cs" %>
  
<%@ Import Namespace="MonoSoftware.MonoX.Resources"%>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>    
<%@ Register TagPrefix="MonoX" TagName="Login" Src="~/MonoX/ModuleGallery/LoginModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="LoginRpx" Src="~/MonoX/ModuleGallery/LoginRpx.ascx" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
<div class="current-login"> <!-- Main Start -->
    <asp:PlaceHolder ID="plhAuthorizationMessage" runat="server" Visible="false">
        <table class="error-message-page" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top"><img src="<%= ResolveUrl(MonoSoftware.MonoX.Paths.App_Themes.img.Error_png) %>" alt="Error" /></td>
                <td valign="middle">
                    <div style="font-size: 18px; border-bottom: solid 1px #eee; padding-bottom: 10px; margin-bottom: 10px;">
                        <span class="title"><%= ErrorMessages.Authorization_Login %></span>
                    </div>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="login-left-section">
                <MonoX:Login runat="server" ID="ctlLogin" Width="100%" />
            </td>
        </tr>        
    </table>                    
</div>    
</asp:Content>


