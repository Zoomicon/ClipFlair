<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogInfo.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogInfo" %>

<asp:Panel runat="server" ID="pnlContainer">
    <div class="input-form blog-info">
        <dl>
            <dd>
                <asp:Literal ID="lblTitle" runat="server"></asp:Literal>
                <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
            </dd> 
            <dd>
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                <asp:Literal ID="ltlDescription" runat="server"></asp:Literal>
            </dd>
        </dl>
    </div>
</asp:Panel>

