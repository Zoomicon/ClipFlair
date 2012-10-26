<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SlideShow.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SlideShow" %>

<asp:Repeater runat="server" ID="rptItems">
<HeaderTemplate>
<div id="slideshow">
    <div id="slidesContainer">
</HeaderTemplate>
<ItemTemplate>
    <div class="slide">        
        <a href='<%# Eval("Url") %>' target="_blank" title='<%# Eval("Title") %>'><%# FormatUrl(Eval("ImageUrl").ToString(), Eval("Title").ToString(), String.Empty) %></a>
    </div>
</ItemTemplate>
<FooterTemplate>
    </div>
</div>
</FooterTemplate>
</asp:Repeater>
