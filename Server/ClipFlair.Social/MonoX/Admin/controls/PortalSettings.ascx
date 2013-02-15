<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Admin.PortalSettings"
    CodeBehind="PortalSettings.ascx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Import Namespace="MonoSoftware.MonoX.Admin" %>
<%@ Register Src="~/MonoX/controls/LightBox.ascx" TagPrefix="MonoX" TagName="LightBox" %>
<div class="portal-settings input-form">
    <asp:Repeater ID="rpt" runat="server">
        <ItemTemplate>
            <h2><strong>
                <%# ((PortalSettingGroup)Container.DataItem).Name %></strong></h2>
            <MonoX:LightBox ID="lightBox" runat="server">
                <ContentTemplate>
                    <dl>
                        <asp:Repeater ID="rptInner" runat="server">
                            <ItemTemplate>
                                <dd>
                                    <asp:Label ID="labName" runat="server" AssociatedControlID="txtValue"></asp:Label>
                                    <asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
                                    <asp:CheckBox ID="chkValue" runat="server"></asp:CheckBox>
                                    <asp:RequiredFieldValidator ID="reqValue" runat="server" CssClass="ValidatorAdapter" SetFocusOnError="true" ControlToValidate="txtValue"
                                        Text="*"></asp:RequiredFieldValidator>
                                </dd>
                            </ItemTemplate>
                        </asp:Repeater>
                    </dl>
                </ContentTemplate>
            </MonoX:LightBox>
        </ItemTemplate>
    </asp:Repeater>    
    <br />
    <br />
    <div class="button-holder">
        <b style="color: Green;">
            <asp:Literal ID="labSuccess" runat="server"></asp:Literal></b>        
        <b style="color: Red;">
            <asp:Literal ID="labMessage" runat="server"></asp:Literal></b>
        <asp:PlaceHolder ID="plhActions" runat="server">
            <asp:Button ID="btnSave" runat="server" CausesValidation="true" CssClass="CssFormButton" />
            <asp:Button ID="btnSkip" runat="server" CausesValidation="false" CssClass="CssFormButton" Visible="false" />
        </asp:PlaceHolder>
    </div>
</div>
