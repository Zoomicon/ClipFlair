<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogInfo.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogInfo" %>

<asp:Panel runat="server" ID="pnlContainer">
    <!--!!!CLIPFLAIR-->
    <div class="input-form blog-info">
        <dl>
            <dd>
                <span><asp:Literal ID="lblTitle" runat="server"></asp:Literal></span>
                <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
            </dd> 
            <dd>               
                <span><asp:Label ID="lblDescription" runat="server"></asp:Label></span>
                <asp:Literal ID="ltlDescription" runat="server"></asp:Literal>                 
            </dd>
        </dl>
    </div>
</asp:Panel>

