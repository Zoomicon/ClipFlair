<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Empty.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.Error" 
    Codebehind="Error.aspx.cs" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
    <div class="top-copyright">
        Copyright &#169;2013
        <a href="http://clipflair.net" title="Learn more about the Project">ClipFlair.net</a>
    </div>
    <div class="error-top-section">
        <div class="container-fluid-large">
            <h1><%= MonoSoftware.MonoX.Resources.ErrorMessages.ErrorPage_Title %></h1>
            <p><%= MonoSoftware.MonoX.Resources.ErrorMessages.ErrorPage_SubTitle %></p>
        </div>
    </div>
    <div class="error-bottom-section">
        <div class="container-fluid-large">
            <p><%= Description %></p>
            <h3><%= MonoSoftware.MonoX.Resources.ErrorMessages.ToContinueWorkingWithApplication %> <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/"></asp:HyperLink></h3>
        </div>
    </div>
</asp:Content>
