<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="DiscussionBoard.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.DiscussionBoard" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager"
    TagPrefix="mono" %>
<%@ Register Src="~/MonoX/controls/CustomRadEditor.ascx" TagPrefix="mono" TagName="CustomRadEditor" %>
<%@ Register TagPrefix="MonoX" TagName="UserEntry" Src="~/MonoX/controls/UserPicker.ascx" %>

<asp:UpdatePanel ID="upNewBoard" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <div style="overflow: hidden;">            
            <asp:HyperLink ID="lnkLastActiveTopics" runat="server" CssClass="discussion-link" ></asp:HyperLink>
            <asp:HyperLink ID="lnkMyTopics" runat="server" CssClass="discussion-link" ></asp:HyperLink>                       
            <asp:UpdateProgress ID="upTop" runat="server" DisplayAfter="0" DynamicLayout="true">
                <ProgressTemplate>
                    <img src='<%= BaseSiteFullUrl %>/App_Themes/<%= Page.Theme %>/img/loading.gif' alt="Progress ..."
                        width="20px" class="progress" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <MonoX:StyledButton ID="btnNewBoard" runat="server" CssClass="discussion-button" />
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
                        <dd>
                            <asp:Label ID="lblBoardDescription" runat="server" AssociatedControlID="txtBoardDescription"></asp:Label>
                            <asp:TextBox ID="txtBoardDescription" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox>
                            <div style="width: 95%;">
                                <mono:CustomRadEditor Width="100%" ID="radBoardDescription"  EditModes="Design"
                                    runat="server" ToolBarMode="ShowOnFocus" StripFormattingOptions="AllExceptNewLines" ContentAreaMode="Iframe" ></mono:CustomRadEditor>
                            </div>
                        </dd>
                        <dd>
                            <asp:Label ID="lblEditors" runat="server" AssociatedControlID="ddlEditors"></asp:Label>
                            <div>
                                <MonoX:UserEntry runat="server" Height="200" Width="98%" ID="ddlEditors" UserFilterMode="ShowAllUsers" ExcludeMySelf="false" />
                            </div>
                        </dd>
                        <dd id="rowAutoSubscribe" runat="server">                        
                            <asp:CheckBox ID="chkSubscribe" runat="server" Checked="true" />
                            <asp:Label ID="labSubscribe" runat="server" AssociatedControlID="chkSubscribe"></asp:Label>
                        </dd>
                        <dd id="rowRoles" runat="server">
                            <asp:Label ID="labRoles" runat="server" AssociatedControlID="chkRoles"></asp:Label>
                            <asp:CheckBoxList ID="chkRoles" runat="server" CellSpacing="10"  
                                RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Table">
                            </asp:CheckBoxList>
                        </dd>
                        <dd>
                            <MonoX:StyledButton ID="btnSaveNewBoard" runat="server" CommandName="Save" CssClass="discussion-styled-button" />
                            <MonoX:StyledButton ID="btnCancelNewBoard" runat="server" CausesValidation="false" CssClass="discussion-styled-button" />
                        </dd>
                    </dl>
                </div>
            </asp:PlaceHolder>
            <asp:ListView ID="lvDB" runat="server">
                <LayoutTemplate>                    
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>                        
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
