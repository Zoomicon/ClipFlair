<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Empty.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Message" 
    Codebehind="Message.aspx.cs" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
    <div class="container-fluid-mid">
        <br /><br />
        <div class="message-page">
            <h2><%= Title %></h2>
            <div><%= Description %></div>
        </div>
        <br /><br />
    </div>
</asp:Content>
