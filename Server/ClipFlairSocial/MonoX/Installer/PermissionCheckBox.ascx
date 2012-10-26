<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PermissionCheckBox.ascx.cs"
    Inherits="MonoSoftware.MonoX.Installer.PermissionCheckBox" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<asp:UpdatePanel ID="up" runat="server">
    <ContentTemplate>
        <div>            
            <asp:Panel ID="pnOk" runat="server">
                <img runat="server" src="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.Installer.img.okay_48_png %>" alt="Okay image" />                
                <p>
                    <asp:Label ID="labOk" runat="server"></asp:Label>
                </p>
            </asp:Panel>
            <asp:Panel ID="panWarning" runat="server">
                <img runat="server" src="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.Installer.img.warning_48_png %>" alt="Warning image" />
                <p>
                    <asp:Label ID="labWarning" runat="server"></asp:Label>
                </p>
                <div class="button-holder">
                    <asp:Button ID="btnTestAgain" runat="server" CausesValidation="false" CssClass="installer_button_big"
                        Text='<%$ Code: InstallerResources.PermissionCheckBox_TestAgain %>' />
                    <asp:Button ID="btnSkip" runat="server" CausesValidation="false" CssClass="installer_button"
                        Text='<%$ Code: InstallerResources.PermissionCheckBox_Skip %>' />
                </div>
            </asp:Panel>
            <asp:UpdateProgress ID="upTop" runat="server" AssociatedUpdatePanelID="up" DisplayAfter="0" 
                    DynamicLayout="true">
                    <ProgressTemplate>
                        <div style="padding-left: 5px;">
                            <strong>Loading ...</strong>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
