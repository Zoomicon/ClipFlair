<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageFriendListAdmin.aspx.cs" EnableTheming="true" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.MessageFriendListAdmin" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %> 
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoX" TagName="AddressEntry" Src="~/MonoX/controls/UserPicker.ascx" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="popupBox">
            <asp:ScriptManager ID="AjaxScriptManager" runat="server"></asp:ScriptManager>
            <asp:Panel CssClass="CssForm" runat="server" ID="pnlContainer">     
            <div class="header">
                <div style="padding: 10px;">
                    <h2 class="friendListAdminHeader">
                        <asp:Literal ID="ltlHeader" runat="server"></asp:Literal>
                    </h2>
                    <br />
                    <p>
                        <asp:Literal ID="ltlSubHeader" runat="server"></asp:Literal>
                    </p>
                </div>
            </div>
            <div class="content"> 
                <asp:UpdatePanel ID="updatePanel" ChildrenAsTriggers="true" runat="server" UpdateMode="Always">
                <ContentTemplate>
                <asp:Button id="btnAdd" runat="server" CausesValidation="true" CssClass="AdminButton" OnClick="lnkAdd_Click"/>     
                <p class="separator"></p>                      
                <asp:DataList CssSelectorClass="dlItems" ID="dlItems" runat="server" DataKeyField="Id" OnEditCommand="dlItems_Edit" OnCancelCommand="dlItems_Cancel" OnDeleteCommand="dlItems_Delete" OnUpdateCommand="dlItems_Update" OnItemDataBound="dlItems_ItemDataBound" Width="100%">
                <ItemTemplate>
                <p>
                    <asp:Label runat="server" Text='<%# SocialNetworkingResources.Messaging_ListAdmin_Name  %>' ID="lblNameCaption" CssClass="leftLabel"></asp:Label>
                    <asp:Label runat="server" Text='<%# Eval("Name") %>' ID="lblName" CssClass="rightLabel"></asp:Label>
                </p>
                <p>
                    <asp:Label runat="server" Text='<%# SocialNetworkingResources.Messaging_ListAdmin_ListMembers  %>' ID="lblMembersCaption" CssClass="leftLabel"></asp:Label>
                    <asp:Label runat="server" Text='<%# Eval("MembersString") %>' ID="lblMembers" CssClass="rightLabel"></asp:Label>
                </p>    
                <p>
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="Delete" Text='<%# DefaultResources.Button_Delete %>' CssClass="blue-button"></asp:LinkButton>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="Edit" Text='<%# DefaultResources.Button_Edit %>' CssClass="blue-button"></asp:LinkButton>                 
                </p>
                <p class="separator"></p>           
                </ItemTemplate>
                <EditItemTemplate>
                <p>
                    <asp:Label ID="lblNameCaption" runat="server" Text='<%# SocialNetworkingResources.Messaging_ListAdmin_Name  %>' AssociatedControlID="txtName" CssClass="leftLabel"></asp:Label>
                    <asp:TextBox ID="txtName" Text='<%# Eval("Name") %>' runat="server" Width="68%" ></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="lblMembersCaption" runat="server" Text='<%# SocialNetworkingResources.Messaging_ListAdmin_ListMembers  %>' AssociatedControlID="ddlMembers" CssClass="leftLabel"></asp:Label>
                    <MonoX:AddressEntry runat="server" Height="200" Width="68%" ID="ddlMembers" Text='<%# Eval("MembersString") %>' UserFilterMode="ShowFriends" />
                </p>
                <p>
                    <asp:LinkButton ID="lnkCancel" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="Cancel" Text='<%# DefaultResources.Button_Cancel %>' CssClass="blue-button"></asp:LinkButton>
                    <asp:LinkButton ID="lnkDelete2" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="Delete" Text='<%# DefaultResources.Button_Delete %>' CssClass="blue-button"></asp:LinkButton> 
                    <asp:LinkButton ID="lnkUpdate" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="Update" Text='<%# DefaultResources.Button_Update %>' CssClass="blue-button"></asp:LinkButton>                                
                </p>
                <p class="separator"></p>           
                </EditItemTemplate>
                </asp:DataList>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            </asp:Panel>
        <div class="footer">
            <asp:PlaceHolder id="plhActions" Runat="server">
                <asp:Button id="btnSave" runat="server" CausesValidation="true" ValidationGroup="Modification" CssClass="AdminButton" OnClientClick="javascript:CloseWindow()" />
            </asp:PlaceHolder>
            <b style="color:Red;"><asp:Literal ID="labMessage" runat="server"></asp:Literal></b>
        </div>
    </div> 
    </form>
</body>
</html>
