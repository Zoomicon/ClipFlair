<%@ Page
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="MessageFriendListAdmin.aspx.cs"
    EnableTheming="true"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.InMail.MessageFriendListAdmin" %>
    MasterPageFile="~/App_MasterPages/ClipFlair/Default.master"

<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %> 
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoX" TagName="AddressEntry" Src="~/MonoX/controls/UserPicker.ascx" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="popup-window">
        <asp:ScriptManager ID="AjaxScriptManager" runat="server"></asp:ScriptManager>
        <asp:Panel runat="server" ID="pnlContainer">     
        <div class="top-section">
            <div class="inner">
                <h2><asp:Literal ID="ltlHeader" runat="server"></asp:Literal></h2>
                <p><asp:Literal ID="ltlSubHeader" runat="server"></asp:Literal></p>
            </div>
        </div>
        <div class="content">
            <div class="inner">
                <asp:UpdatePanel ID="updatePanel" ChildrenAsTriggers="true" runat="server" UpdateMode="Always">
                <ContentTemplate>
                <asp:DataList CssSelectorClass="dlItems" ID="dlItems" runat="server" DataKeyField="Id" OnEditCommand="dlItems_Edit" OnCancelCommand="dlItems_Cancel" OnDeleteCommand="dlItems_Delete" OnUpdateCommand="dlItems_Update" OnItemDataBound="dlItems_ItemDataBound" Width="100%">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# SocialNetworkingResources.Messaging_ListAdmin_Name  %>' ID="lblNameCaption"></asp:Label>
                        <h3>
                            <asp:Label runat="server" Text='<%# Eval("Name") %>' ID="lblName"></asp:Label>
                        </h3>
                        <p>
                            <asp:Label runat="server" Text='<%# SocialNetworkingResources.Messaging_ListAdmin_ListMembers  %>' ID="lblMembersCaption"></asp:Label>
                            <asp:Label runat="server" Text='<%# Eval("MembersString") %>' ID="lblMembers" CssClass="members"></asp:Label>
                        </p>    
                        <div class="button-holder small-btn float-left padding-bottom">
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="Delete" Text='<%# DefaultResources.Button_Delete %>' CssClass="styled-button_ delete-btn float-left"></asp:LinkButton>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="Edit" Text='<%# DefaultResources.Button_Edit %>' CssClass="styled-button_ edit-btn float-left"></asp:LinkButton>                 
                        </div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div class="input-form padding-bottom">
                            <dl>
                                <dd>
                                    <asp:Label ID="lblNameCaption" runat="server" Text='<%# SocialNetworkingResources.Messaging_ListAdmin_Name  %>' AssociatedControlID="txtName"></asp:Label>
                                    <asp:TextBox ID="txtName" Text='<%# Eval("Name") %>' runat="server"></asp:TextBox>
                                </dd>
                                <dd>
                                    <asp:Label ID="lblMembersCaption" runat="server" Text='<%# SocialNetworkingResources.Messaging_ListAdmin_ListMembers  %>' AssociatedControlID="ddlMembers"></asp:Label>
                                    <MonoX:AddressEntry runat="server" Height="200" ID="ddlMembers" Text='<%# Eval("MembersString") %>' UserFilterMode="ShowFriends" />
                                </dd>
                                <dd class="button-holder small-btn">
                                    <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="false" CommandArgument='<%# Eval("Id") %>' CommandName="Update" Text='<%# DefaultResources.Button_Update %>' CssClass="styled-button_ main-button update-btn-b float-left"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" CommandArgument='<%# Eval("Id") %>' CommandName="Cancel" Text='<%# DefaultResources.Button_Cancel %>' CssClass="styled-button_ cancel-btn float-left"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete2" runat="server" CausesValidation="false" CommandArgument='<%# Eval("Id") %>' CommandName="Delete" Text='<%# DefaultResources.Button_Delete %>' CssClass="styled-button_ delete-btn float-left"></asp:LinkButton>
                                </dd>
                                <br /><br />
                            </dl>
                        </div>                      
                    </EditItemTemplate>
                </asp:DataList>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        </asp:Panel>
        <div class="bottom-section">
            <div class="inner clearfix">
                <asp:PlaceHolder id="plhActions" Runat="server">
                    <!--CLIPFLAIR-->                       
                    <asp:Button id="btnSave" runat="server" CausesValidation="true" ValidationGroup="Modification" CssClass="styled-button_ main-button submit-btn float-right" OnClientClick="javascript:CloseWindow()" />
                    <asp:Button id="btnAdd" runat="server" CausesValidation="true" CssClass="styled-button_ add-btn float-right" OnClick="lnkAdd_Click"/>                
                </asp:PlaceHolder>
                <b><asp:Literal ID="labMessage" runat="server"></asp:Literal></b>
            </div>
        </div>
    </div> 
    </form>
</body>
</html>
