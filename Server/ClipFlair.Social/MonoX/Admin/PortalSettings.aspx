<%@ Assembly Name="MonoSoftware.MonoX.DAL" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.EntityClasses" %>

<%@ Page Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master" AutoEventWireup="true"
    Inherits="MonoSoftware.MonoX.Admin.PortalSettingsManager" EnableTheming="true" Theme="DefaultAdmin"
    Title="Portal settings management" CodeBehind="PortalSettings.aspx.cs" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Register Src="~/MonoX/Admin/controls/PortalSettings.ascx" TagPrefix="MonoX" TagName="PortalSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="Server">
<div class="AdminContainer">
    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <MonoX:PortalSettings ID="portalSettings" runat="server" />            
        </ContentTemplate>
    </asp:UpdatePanel>
</div>    
</asp:Content>
