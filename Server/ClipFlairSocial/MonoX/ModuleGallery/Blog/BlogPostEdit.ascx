<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogPostEdit" Codebehind="BlogPostEdit.ascx.cs" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %> 
<%@ Register Src="~/MonoX/controls/CustomRadEditor.ascx" TagPrefix="mono" TagName="CustomRadEditor" %>
<%@ Register TagPrefix="MonoX" TagName="SilverlightUpload" Src="~/MonoX/ModuleGallery/SilverlightUploadModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FileGallery" Src="~/MonoX/ModuleGallery/SocialNetworking/FileGallery.ascx" %>
<%@ Register Src="~/MonoX/Admin/controls/DatePicker.ascx" TagPrefix="mono" TagName="DatePicker" %>
<%@ Register Src="~/MonoX/controls/TagTextBox.ascx" TagPrefix="mono" TagName="TagTextBox" %>
<%@ Register Src="~/MonoX/ModuleGallery/Blog/BlogManageCategories.ascx" TagPrefix="MonoX" TagName="BlogManageCategories" %>
<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
<asp:Panel runat="server" ID="pnlContainer">
    <div class="blog-edit-settings input-form">
        <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List"  ShowSummary="true" />
        <dl>
            <dd>
                <asp:Label ID="lblTitle" runat="server" AssociatedControlID="txtTitle" CssClass="my-label"></asp:Label>
                <asp:TextBox ID="txtTitle" runat="server" class="padding1-after"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requiredTitle" runat="server" CssClass="ValidatorAdapter" ControlToValidate="txtTitle" SetFocusOnError="true" Display="Dynamic" Text="!"></asp:RequiredFieldValidator>
            </dd>        
            <dd>
                
                <asp:Label ID="lblContent" runat="server" AssociatedControlID="radContent" CssClass="my-label"></asp:Label>
                <div style="width: 98%;" class="padding1-after">
                    <mono:CustomRadEditor Width="100%" id="radContent" ToolsFile="~/MonoX/controls/CustomRadEditorBlogToolsFile.xml" runat="server" ToolBarMode="ShowOnFocus"></mono:CustomRadEditor>
                </div>
            </dd>        
            <dd>
                
                <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" CssClass="my-label"></asp:Label>
                <div style="width: 98%;" class="padding1-after">
                    <mono:CustomRadEditor Width="100%" id="txtDescription" ToolsFile="~/MonoX/controls/CustomRadEditorBlogToolsFile.xml" runat="server" ToolBarMode="ShowOnFocus" ></mono:CustomRadEditor>
                </div>                
            </dd>
            <dd>
                <asp:Panel runat="server" ID="pnlCategories" CssClass="padding1-after">
                    <asp:UpdatePanel runat="server" ID="pnlUpdateCategories">
                        <ContentTemplate>
                            <dl>
                                <dd>
                                   
                                    <asp:Label ID="lblCategories" AssociatedControlID="chkCategories" runat="server" CssClass="my-label"></asp:Label>
                                    <asp:CheckBoxList CssClass="check-box-list" ID="chkCategories" runat="server" DataTextField="Name" DataValueField="Id" RepeatColumns="5" RepeatLayout="Table" RepeatDirection="Horizontal" TextAlign="Left"></asp:CheckBoxList>
                                    <div class="small-links">
                                        <asp:Hyperlink ID="lnkCategories" runat="server" CssClass="jq_categoriesAction" NavigateUrl="javascript:void(0);"></asp:Hyperlink>
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
            <dd>
                <asp:Panel runat="server" ID="pnlRoles" class="padding1-after padding1-before">
                    <dl>
                        <dd>
                           
                            <asp:Label ID="lblRoles" AssociatedControlID="chkRoles" runat="server" CssClass="my-label"></asp:Label>
                            <asp:CheckBoxList CssClass="check-box-list" ID="chkRoles" runat="server" DataTextField="RoleName" DataValueField="RoleId" RepeatColumns="5" RepeatLayout="Table" RepeatDirection="Horizontal" TextAlign="Left"></asp:CheckBoxList>
                        </dd>
                    </dl>
                </asp:Panel>
            </dd>
            <dd>
                <mono:TagTextBox id="tags" runat="server"></mono:TagTextBox>
            </dd> 
            <br/>            
            <dd>
                
                <asp:Label ID="lblDatePublished" AssociatedControlID="txtDatePublished" runat="server" CssClass="my-label padding1-before" style="float: left;"></asp:Label>
                <div style="display: inline-block; float: left;"><mono:DatePicker ID="txtDatePublished" Runat="server"></mono:DatePicker></div>
            </dd>
            <dd>
               
                <asp:Label ID="lblIsPublished" AssociatedControlID="chkIsPublished" runat="server" CssClass="my-label padding1-before"></asp:Label>
                <asp:CheckBox BorderStyle="None" BorderWidth="0px" ID="chkIsPublished" runat="server" TextAlign="Right"></asp:CheckBox>
            </dd>
            <dd>
                
                <asp:Label ID="lblIsCommentAllowed" AssociatedControlID="chkIsCommentAllowed" runat="server" CssClass="my-label padding1-before"></asp:Label>
                <asp:CheckBox BorderStyle="None" BorderWidth="0px" ID="chkIsCommentAllowed" runat="server" TextAlign="Right"></asp:CheckBox>
            </dd>
            <dd>
                
                <asp:Label ID="lblAttachments" AssociatedControlID="ctlUpload" runat="server" CssClass="my-label padding1-before"></asp:Label>
                <MonoX:SilverlightUpload runat="server" ID="ctlUpload" EnableFileGallery="false" CssClass="blog-file-upload" />
            </dd>
            <dd>
                <MonoX:FileGallery ID="ctlFileGallery" runat="server" ParentEntityType="BlogPost" />
            </dd>
        </dl>
    </div>
</asp:Panel>
<div class="input-form">
    <div class="button-holder">
        <asp:PlaceHolder id="plhActions" Runat="server">
            <MonoX:StyledButton id="btnSave" runat="server" CausesValidation="true" OnClick="btnSave_Click"  />
            <MonoX:StyledButton id="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click" />                        
        </asp:PlaceHolder>
        <b style="color:Red;"><asp:Literal ID="labMessage" runat="server"></asp:Literal></b>
    </div>
</div>
