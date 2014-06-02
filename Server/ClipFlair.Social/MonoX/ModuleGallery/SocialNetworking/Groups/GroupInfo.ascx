<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="GroupInfo.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.GroupInfo" %>

<!--!!!CLIPFLAIR-->
<div class="group-info">
    <div class="group-image">
        <asp:Image runat="server" ID="imgLogo" CssClass="scale-with-grid" />
    </div>
    <asp:Panel runat="server" ID="pnlContainer">
    <div class="input-form">
        <dl>
            <dd>
                <span><asp:Literal ID="lblName" runat="server"></asp:Literal></span>
                <asp:Literal ID="ltlName" runat="server"></asp:Literal>
            </dd> 
            <dd>
                <span><asp:Label ID="lblDescription" runat="server"></asp:Label></span>
                <asp:Literal ID="ltlDescription" runat="server"></asp:Literal>
            </dd>
            <dd>
                <span><asp:Label ID="lblCategory" runat="server"></asp:Label></span>
                <asp:Hyperlink ID="lnkCategory" runat="server"></asp:Hyperlink>
            </dd>
            <dd>
                <span><asp:Label ID="lblPrivacy" runat="server"></asp:Label></span>
                <asp:Literal ID="ltlPrivacy" runat="server"></asp:Literal>
            </dd>
            <dd class="button-holder small-btn">
                <asp:Panel CssClass="options" ID="pnlLeave" runat="server">
                    <asp:LinkButton ID="lnkLeave" runat="server" CssClass="styled-button padding-left"></asp:LinkButton>
                </asp:Panel>
                <asp:Panel ID="pnlAdmin" runat="server">
                    <asp:HyperLink ID="lnkEdit" runat="server" CssClass="styled-button main-button edit-btn-b float-left"></asp:HyperLink>
                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="styled-button delete-btn float-left"></asp:LinkButton>
                </asp:Panel>
                <asp:Literal runat="server" ID="ltlRequestWaiting"></asp:Literal>
            </dd>
        </dl>
    </div>
    </asp:Panel>
</div>