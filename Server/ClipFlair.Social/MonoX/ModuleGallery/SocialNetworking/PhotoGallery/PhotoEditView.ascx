<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="PhotoEditView.ascx.cs" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.PhotoEditView" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div class="row-fluid">
    <div class="span12">       
        <h2><%= MonoSoftware.MonoX.Resources.SocialNetworkingResources.PhotoGallery_PhotoEditView_EditPhoto%></h2>
        <div class="input-form photos-form">
            <dl>
                <dd>
                    <asp:Label ID="lblName" AssociatedControlID="txtName" runat="server" ></asp:Label>        
                    <asp:TextBox runat="server" ID="txtName" CssClass="TextBox" />
                </dd>
                <dd>
                    <asp:Label ID="lblDescription" AssociatedControlID="txtDescription" runat="server" ></asp:Label>
                    <asp:TextBox runat="server" ID="txtDescription" CssClass="TextBox" TextMode="MultiLine" Rows="10" />
                </dd>
                <dd>
                    <asp:Label ID="lblPrivacy" AssociatedControlID="ddlPrivacy" runat="server" ></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlPrivacy" CssClass="TextBox" />
                </dd>
                <asp:UpdatePanel ID="up" runat="server">
                    <ContentTemplate>
                    <!--CLIPFLAIR-->
                    <dd class="my_checkbox">
                        <asp:CheckBox runat="server" ID="chkAlbumCover" />
                        <asp:Label ID="labCheck" AssociatedControlID="chkAlbumCover" runat="server" ></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="labAlbumCover" CssClass="my_label" runat="server"></asp:Label>
                    </dd>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!--CLIPFLAIR-->
                <dd class="button-holder">
                    <MonoX:StyledButton ID="btnSave" CausesValidation="false" runat="server" CssClass="main-button submit-btn float-left" /> 
                    <MonoX:StyledButton ID="btnCancel" CausesValidation="false" runat="server" CssClass="cancel-btn float-left" />
                </dd>
            </dl>
        </div>
    </div>
</div>