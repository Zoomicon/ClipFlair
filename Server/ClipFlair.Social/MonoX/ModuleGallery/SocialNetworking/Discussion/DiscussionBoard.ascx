<%@ Control
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="DiscussionBoard.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.DiscussionBoard" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Src="~/MonoX/controls/CustomRadEditor.ascx" TagPrefix="mono" TagName="CustomRadEditor" %>
<%@ Register TagPrefix="MonoX" TagName="UserEntry" Src="~/MonoX/controls/UserPicker.ascx" %>

<asp:UpdatePanel ID="upNewBoard" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <!--CLIPFLAIR-->
        <div class="discussion-top-section clearfix">
            <h1><%= MonoSoftware.MonoX.Resources.SocialNetworkingResources.Discussion_DiscussionBoardList %></h1>
            <div class="main-options">
                <div class="inner padding-bottom-10">
                    <asp:HyperLink ID="lnkLastActiveTopics" CssClass="discussion-link styled-button list-btn float-left" runat="server" ></asp:HyperLink>
                    <asp:HyperLink ID="lnkMyTopics" CssClass="discussion-link styled-button list-btn float-left" runat="server" ></asp:HyperLink>                                      
                    <MonoX:StyledButton ID="btnNewBoard" runat="server" CssClass="styled-button main-button add-btn float-left" />
                    <div class="float-right">
                        <asp:UpdateProgress ID="upTop" runat="server" DisplayAfter="0" DynamicLayout="true">
                            <ProgressTemplate>
                                <img src='<%= BaseSiteFullUrl %>/App_Themes/<%= Page.Theme %>/img/loading.gif' alt="Progress ..."
                                    width="40" class="progress" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
            </div>
        </div>
        <div class="discussion board-container">
            <asp:PlaceHolder ID="plhNewBoard" runat="server" Visible="false">
            <div class="input-form discussion-form">
                <dl>
                    <dd>
                        <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ShowSummary="true" />
                    </dd>
                    <dd>
                        <asp:Label ID="lblBoardName" runat="server" AssociatedControlID="txtBoardName"></asp:Label>
                        <asp:TextBox ID="txtBoardName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="vldRequiredBoardName" runat="server" ControlToValidate="txtBoardName" Text="!" CssClass="validator ValidatorAdapter" Display="Dynamic" />
                    </dd>
                    <dd class="editor-height">
                        <asp:Label ID="lblBoardDescription" runat="server" AssociatedControlID="txtBoardDescription"></asp:Label>
                        <asp:TextBox ID="txtBoardDescription" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        <mono:CustomRadEditor Width="100%" Height="" ID="radBoardDescription" EditModes="Design"
                            runat="server" ToolBarMode="ShowOnFocus" StripFormattingOptions="AllExceptNewLines" ContentAreaMode="Div" ></mono:CustomRadEditor>
                    </dd>
                    <dd>
                        <asp:Label ID="lblEditors" runat="server" AssociatedControlID="ddlEditors"></asp:Label>
                        <MonoX:UserEntry runat="server" Height="200" ID="ddlEditors" UserFilterMode="ShowAllUsers" ExcludeMySelf="false" />
                    </dd>
                    <dd id="rowAutoSubscribe" runat="server" class="my_checkbox">                        
                        <asp:CheckBox ID="chkSubscribe" runat="server" Checked="true" />
                        <asp:Label ID="labCheck" runat="server" AssociatedControlID="chkSubscribe"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="labSubscribe" runat="server" class="my_label"></asp:Label>
                    </dd>
                    <dd id="rowRoles" runat="server">
                        <asp:Label ID="labRoles" runat="server" AssociatedControlID="chkRoles"></asp:Label> 
                        <asp:CheckBoxList ID="chkRoles" runat="server" CellSpacing="10" CssClass="check-box-list" Width="100%"
                            RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table">
                        </asp:CheckBoxList>                       
                    </dd>
                    <dd class="button-holder">
                        <!--CLIPFLAIR-->
                        <MonoX:StyledButton ID="btnSaveNewBoard" runat="server" CommandName="Save" CssClass="main-button submit-btn float-left" />
                        <MonoX:StyledButton ID="btnCancelNewBoard" runat="server" CausesValidation="false" CssClass="cancel-btn float-left" />
                    </dd>
                </dl>
            </div>
            </asp:PlaceHolder>
            <asp:ListView ID="lvDB" runat="server">
                <LayoutTemplate>
                    <div>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>                        
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="empty-discussion"><%= MonoSoftware.MonoX.Resources.SocialNetworkingResources.Discussion_Board_Empty %></div>
                </EmptyDataTemplate>
            </asp:ListView>            
        </div>
        <div style="clear: both">
            <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true"
                AutoPaging="false">
                <PagerTemplate>
                </PagerTemplate>
            </mono:Pager>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
