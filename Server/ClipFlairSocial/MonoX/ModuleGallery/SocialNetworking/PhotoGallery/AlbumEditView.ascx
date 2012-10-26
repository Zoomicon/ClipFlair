<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="AlbumEditView.ascx.cs" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.AlbumEditView" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<span class="comments-title padding0-after"><strong><%= MonoSoftware.MonoX.Resources.SocialNetworkingResources.PhotoGallery_AlbumEditView_CreateAlbum %></strong></span>
<div class="input-form">
<div class="photo-gallery edit-photos">
<dl>
    <dd>
        <asp:Label ID="lblName" AssociatedControlID="txtName" runat="server" CssClass="my-label" ></asp:Label>
        <asp:TextBox runat="server" ID="txtName" CssClass="TextBox" />
    </dd>
    <dd>
        <asp:Label ID="lblDescription" AssociatedControlID="txtDescription" runat="server" CssClass="my-label" ></asp:Label>
        <asp:TextBox runat="server" ID="txtDescription" CssClass="TextBox" TextMode="MultiLine" Rows="10" />
    </dd>
    <dd id="rowPrivacy" runat="server">
        <asp:Label ID="lblPrivacy" AssociatedControlID="ddlPrivacy" runat="server" CssClass="my-label" ></asp:Label>
        <asp:DropDownList runat="server" ID="ddlPrivacy" CssClass="TextBox" />
    </dd>
</dl>
</div>
</div>
<div class="input-form">
    <div class="button-holder">
        <MonoX:StyledButton ID="btnSave" runat="server" /> 
        <MonoX:StyledButton ID="btnCancel" runat="server" />
    </div>
</div>