<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogManageCategories.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogManageCategories" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
    <ContentTemplate>                
        <dl>
            <dd>
                <!--CLIPFLAIR-->
                <asp:Label ID="labCategory" CssClass="cat_title" runat="server"></asp:Label>
                <asp:TextBox ID="txtCategory" runat="server"></asp:TextBox>
                <div class="add-category-btn small-btn">
                    <MonoX:StyledButton ID="btnAdd" runat="server" CausesValidation="false" CssClass="add-btn-b float-right" />
                </div>
            </dd>
            <dd class="category-lists">  
                <div class="small-btn">
                    <MonoX:StyledButton ID="btnUpdate" runat="server" CausesValidation="false" CssClass="update-btn float-left" />
                    <MonoX:StyledButton ID="btnCancel" runat="server" CausesValidation="false" CssClass="cancel-btn float-left" />
                </div>
                <telerik:RadListBox ID="lstCategories" runat="server" SelectionMode="Single" AllowDelete="true" CausesValidation="false"
                    Width="98%" Height="200px" Skin="Default" AutoPostBackOnDelete="true" AutoPostBack="true">
                </telerik:RadListBox>
            </dd>
        </dl>
    </ContentTemplate>
</asp:UpdatePanel>
