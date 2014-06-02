<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridViewEditBox.ascx.cs"
    Inherits="MonoSoftware.MonoX.GridViewEditBox" %>
<%@ Register TagPrefix="mono" Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" %>
<%@ Register TagPrefix="mono" Namespace="MonoSoftware.Web.LiteGrid" Assembly="MonoSoftware.Web.LiteGrid" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<asp:ScriptManagerProxy ID="gridScriptManager" runat="server">
</asp:ScriptManagerProxy>

<div style="padding: 15px; float: left; clear: both;">
<asp:UpdatePanel ID="upGridBox" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="AdminHeaderTop"></div>
        <div class="AdminHeaderBottom">
            <asp:Panel ID="panFilter" runat="server">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 20%">
                            <asp:Literal ID="labFind" runat="server" Text='<%$ Code: AdminResources.Label_Find %>'></asp:Literal>
                        </td>
                        <td style="width: 60%">
                            <asp:TextBox ID="txtFilter" runat="server" CssClass="searchinput" Width="300px"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <asp:Button ID="btnFilter" runat="server" CausesValidation="false" Text='<%$ Code: AdminResources.Button_Filter %>'
                                CssClass="AdminButton" OnClick="btnFilter_Click" />                                                
                            <asp:Button ID="btnFilterReset" runat="server" CausesValidation="false" Text='<%$ Code: AdminResources.Button_FilterReset %>'
                                CssClass="AdminButton" OnClick="btnFilterReset_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="panCustomFilter" runat="server">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:PlaceHolder ID="PlaceHolderCustomFilter" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div class="AdminGridContent">
            <asp:Panel ID="panGrid" runat="server">
            <mono:LiteGrid ID="grdData" runat="server" ShowHeader="true" AutoGenerateColumns="false"
                AutoAdjustDataContainerVSize="false" ShowHeaderWhenEmpty="true" AllowSorting="true">
                <Columns>
                    <asp:TemplateField HeaderText='<%$ Code: AdminResources.Button_Actions %>' ShowHeader="true">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CausesValidation="false" Text='<%$ Code: AdminResources.Button_Delete %>'
                                CommandArgument="<%# ((SD.LLBLGen.Pro.ORMSupportClasses.IEntity2)Container.DataItem).Fields[grdData.DataKeyNames[0]].CurrentValue %>" />
                            <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" CausesValidation="false" Text='<%$ Code: AdminResources.Button_Edit %>'
                                CommandArgument="<%# ((SD.LLBLGen.Pro.ORMSupportClasses.IEntity2)Container.DataItem).Fields[grdData.DataKeyNames[0]].CurrentValue %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                </EmptyDataTemplate>
            </mono:LiteGrid>
            <mono:Pager ID="pager" runat="server" AutoPaging="False" AllowCustomPaging="True" PageSize="10" NumericButtonCount="5">
                <PagerTemplate>
                </PagerTemplate>
            </mono:Pager>       
            </asp:Panel>
        </div> 
        <asp:PlaceHolder ID="plhBottomActions" runat="server">
            <div class="AdminGridFooterTop">
                <div class="Inner">
                    <asp:Button ID="btnAddnew" runat="server" CausesValidation="false" CssClass="AdminButton" Text='<%$ Code: AdminResources.Button_Add %>' style="float: left;" />
                    <div style="display: none;">
                        <asp:Button ID="btnHiddenAddnew" runat="server" Text="Add new" CausesValidation="false"
                            OnClick="btnAddnew_Click" style="float: left;"/></div>
                    <div style="float: right;">
                        <asp:PlaceHolder ID="PlaceHolderCustomActions" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
            </div>
            <div class="AdminGridFooterBottom"></div>
        </asp:PlaceHolder>        
        <asp:PlaceHolder ID="plhTopActions" runat="server">
            <div class="AdminGridFooterHeader">
                <asp:Button runat="server" ID="btnTopSave" CausesValidation="true" UseSubmitBehavior="false" CssClass="AdminButton" Text='<%$ Code: AdminResources.Button_Save %>'>
                </asp:Button>
                <div style="display: none;">
                    <asp:Button runat="server" ID="btnHiddenSave" OnClick="btnSave_Click" CausesValidation="true" UseSubmitBehavior="false" CssClass="AdminButton" Text='<%$ Code: AdminResources.Button_Save %>'></asp:Button>
                </div>
                <asp:Button runat="server" ID="btnTopCancel" OnClick="btnCancel_Click" CssClass="AdminButton" CausesValidation="false"
                    Text='<%$ Code: AdminResources.Button_Cancel %>'></asp:Button>
            </div>
        </asp:PlaceHolder>                
        <asp:PlaceHolder ID="PlaceHolderContent" runat="server"></asp:PlaceHolder>        
        <asp:PlaceHolder ID="plhActions" runat="server">
            <div class="AdminGridFooter">
                <asp:Button runat="server" ID="btnSave" CausesValidation="true" UseSubmitBehavior="false" CssClass="AdminButton" Text='<%$ Code: AdminResources.Button_Save %>'>
                </asp:Button>
                <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" CssClass="AdminButton" CausesValidation="false"
                    Text='<%$ Code: AdminResources.Button_Cancel %>'></asp:Button>
            </div>
        </asp:PlaceHolder>        
        <asp:HiddenField ID="hiddenException" runat="server" />
        <asp:HiddenField ID="hiddenInnerException" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
</div>