<%@ Control
    Language="C#"
    AutoEventWireup="true"
    Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogPostEdit"
    Codebehind="BlogPostEdit.ascx.cs" %>

<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %> 
<%@ Register Src="~/MonoX/controls/CustomRadEditor.ascx" TagPrefix="mono" TagName="CustomRadEditor" %>
<%@ Register TagPrefix="MonoX" TagName="SilverlightUpload" Src="~/MonoX/ModuleGallery/SilverlightUploadModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FileGallery" Src="~/MonoX/ModuleGallery/SocialNetworking/FileGallery.ascx" %>
<%@ Register Src="~/MonoX/controls/TagTextBox.ascx" TagPrefix="mono" TagName="TagTextBox" %>
<%@ Register Src="~/MonoX/ModuleGallery/Blog/BlogManageCategories.ascx" TagPrefix="MonoX" TagName="BlogManageCategories" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>

<asp:Panel runat="server" ID="pnlContainer">
    
    <h1>New blog / Edit Blog</h1>
    <div class="blog-edit-settings input-form">
        <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List"  ShowSummary="true" />
        <dl>
            <dd>
                <asp:Label ID="lblTitle" runat="server" AssociatedControlID="txtTitle"></asp:Label>
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requiredTitle" runat="server" CssClass="ValidatorAdapter" ControlToValidate="txtTitle" SetFocusOnError="true" Display="Dynamic" Text="!"></asp:RequiredFieldValidator>
            </dd>        
            <dd class="editor-height">
                <asp:Label ID="lblContent" runat="server" AssociatedControlID="radContent"></asp:Label>
                <mono:CustomRadEditor Width="100%" id="radContent" ContentAreaMode="Div" ToolsFile="~/MonoX/controls/CustomRadEditorBlogToolsFile.xml" runat="server" ToolBarMode="ShowOnFocus" ></mono:CustomRadEditor>
            </dd>        
            <dd class="editor-height">
                <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription"></asp:Label>
                <mono:CustomRadEditor Width="100%" id="txtDescription" ContentAreaMode="Div" ToolsFile="~/MonoX/controls/CustomRadEditorBlogToolsFile.xml" runat="server" ToolBarMode="ShowOnFocus" ></mono:CustomRadEditor>
            </dd>
            <dd>
                <asp:Panel runat="server" ID="pnlCategories">
                    <asp:UpdatePanel runat="server" ID="pnlUpdateCategories">
                        <ContentTemplate>
                            <dl>
                                <dd>
                                    <asp:Label ID="lblCategories" AssociatedControlID="chkCategories" runat="server"></asp:Label>
                                    <asp:CheckBoxList CssClass="check-box-list" ID="chkCategories" runat="server" DataTextField="Name" DataValueField="Id" RepeatColumns="5" RepeatLayout="Table" RepeatDirection="Horizontal" TextAlign="Left"></asp:CheckBoxList>
                                    <div class="small-links float-left">
                                        <span class="edit-link"><asp:Hyperlink ID="lnkCategories" runat="server" CssClass="jq_categoriesAction" NavigateUrl="javascript:void(0);"></asp:Hyperlink></span>
                                    </div>
                                </dd>
                            
                                <asp:Panel runat="server" ID="pnlCategoriesEdit" CssClass="jq_categoriesEdit">
                                    <MonoX:BlogManageCategories id="blogManageCategories" runat="server"></MonoX:BlogManageCategories>    
                                </asp:Panel>
                            </dl>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </dd>
            <dd class="view-roles">
                <asp:Panel runat="server" ID="pnlRoles">
                    <dl>
                        <dd>
                            <asp:Label ID="lblRoles" AssociatedControlID="chkRoles" runat="server"></asp:Label>
                            <asp:CheckBoxList CssClass="check-box-list" ID="chkRoles" runat="server" DataTextField="RoleName" DataValueField="RoleId" RepeatColumns="4" RepeatLayout="Table" RepeatDirection="Horizontal" TextAlign="Left"></asp:CheckBoxList>
                        </dd>
                    </dl>
                </asp:Panel>
            </dd>
            <dd>
                <mono:TagTextBox id="tags" runat="server"></mono:TagTextBox>
            </dd>            
            <dd>
                <asp:Label ID="lblDatePublished" AssociatedControlID="txtDatePublished" runat="server" CssClass="label" style="float: left;"></asp:Label>
                <rad:RadDatePicker id="txtDatePublished" Runat="server" Calendar-Skin="Default2006" CssClass="date_picker">                                                    
                    <datepopupbutton ></datepopupbutton>
                </rad:RadDatePicker>
            </dd>
            <dd>
                <asp:Label ID="lblIsPublished" AssociatedControlID="chkIsPublished" runat="server" CssClass="label"></asp:Label>
                <asp:CheckBox BorderStyle="None" BorderWidth="0px" ID="chkIsPublished" runat="server" TextAlign="Right"></asp:CheckBox>
            </dd>
            <dd>
                <asp:Label ID="lblIsCommentAllowed" AssociatedControlID="chkIsCommentAllowed" runat="server" CssClass="label"></asp:Label>
                <asp:CheckBox BorderStyle="None" BorderWidth="0px" ID="chkIsCommentAllowed" runat="server" TextAlign="Right"></asp:CheckBox>
            </dd>
            <dd>
                <asp:Label ID="lblAttachments" AssociatedControlID="ctlUpload" runat="server"></asp:Label>
                <MonoX:SilverlightUpload width="300" runat="server" ID="ctlUpload" EnableFileGallery="false" CssClass="blog-file-upload" />
            </dd>
            <dd>
                <MonoX:FileGallery ID="ctlFileGallery" runat="server" ParentEntityType="BlogPost" />
            </dd>
        </dl>
    </div>
</asp:Panel>
<br /><br />
<div class="input-form">
    <div class="button-holder">
        <asp:PlaceHolder id="plhActions" Runat="server">
            <!--CLIPFLAIR-->
            <MonoX:StyledButton id="btnSave" runat="server" CausesValidation="true" OnClick="btnSave_Click" CssClass="main-button submit-btn float-left"  />
            <MonoX:StyledButton id="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click" CssClass="cancel-btn float-left" />                        
        </asp:PlaceHolder>
        <b style="color:Red;"><asp:Literal ID="labMessage" runat="server"></asp:Literal></b>
    </div>
</div>
