<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Message" 
    Theme="Default" 
    Codebehind="Message.aspx.cs" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
<div class="main input-form"> <!-- Main Start -->
<div class="message-page">
    <h2><%= Title %></h2>
    <div><%= Description %></div>
</div>    
</div>
</asp:Content>
