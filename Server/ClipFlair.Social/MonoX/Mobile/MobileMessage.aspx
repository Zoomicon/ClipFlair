<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Mobile/Default.master"
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Mobile.MobileMessage" 
    Theme="Mobile" 
    Codebehind="MobileMessage.aspx.cs" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
<div class="main input-form"> <!-- Main Start -->
<div class="message-page">
    <h2><%= Title %></h2>
    <div><%= Description %></div>
</div>    
</div>
</asp:Content>
