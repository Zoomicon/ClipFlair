<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="PhotoEditView.ascx.cs" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.PhotoEditView" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<strong><%= MonoSoftware.MonoX.Resources.SocialNetworkingResources.PhotoGallery_PhotoEditView_EditPhoto%></strong>
<div class="input-form">
<div class="photo-gallery edit-photos">
<dl>
    <dd>
        <asp:Label ID="lblName" AssociatedControlID="txtName" runat="server" CssClass="my-label" ></asp:Label></span>        
        <asp:TextBox runat="server" ID="txtName" CssClass="TextBox" />
    </dd>
    <dd>
        <asp:Label ID="lblDescription" AssociatedControlID="txtDescription" runat="server" CssClass="my-label" ></asp:Label>
        <asp:TextBox runat="server" ID="txtDescription" CssClass="TextBox" TextMode="MultiLine" Rows="10" />
    </dd>
    <dd>
        <asp:Label ID="lblPrivacy" AssociatedControlID="ddlPrivacy" runat="server" CssClass="my-label" ></asp:Label>
        <asp:DropDownList runat="server" ID="ddlPrivacy" CssClass="TextBox" />
    </dd>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
        <dd>
            <asp:Label ID="labAlbumCover" AssociatedControlID="chkAlbumCover" runat="server" CssClass="my-label" ></asp:Label>
            <asp:CheckBox runat="server" ID="chkAlbumCover" CssClass="TextBox"  />
        </dd>
        </ContentTemplate>
    </asp:UpdatePanel>
</dl>
</div>
</div>
<div class="input-form">
    <div class="button-holder">
        <MonoX:StyledButton ID="btnSave" runat="server" /> 
        <MonoX:StyledButton ID="btnCancel" runat="server" /> 
    </div>        
</div>