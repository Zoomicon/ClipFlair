<%@ Page Language="C#" MasterPageFile="~/MonoX/Installer/Installer.master" AutoEventWireup="true"
    Inherits="MonoX_Installer_InstallComplete" Title="Install Options" Theme="Installer"
    CodeBehind="InstallComplete.aspx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>   

<%@ MasterType VirtualPath="~/MonoX/Installer/Installer.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cp_hd" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp_cnt" runat="Server">
    <asp:Timer ID="radTick" runat="server" Interval="1000" />
    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
        <ContentTemplate>
            <div class="installer_complete">
                <div>
                    <asp:Literal ID="labSubTitle" runat="server" Text='<%$ Code: InstallerResources.InstallComplete_labSubTitle %>'></asp:Literal>
                </div>
                <div class="installer_data">
                    <div id="logContainer" class="installer_log">
                        <asp:Panel ID="panStatus" runat="server">
                            <asp:Literal ID="labStatus" runat="server">
                            </asp:Literal>
                        </asp:Panel>
                        <div style="width: 95%;">
                            <div id="imgStatusHolder" runat="server" class="active">
                                <img id="imgStatus" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="panNote" runat="server" class="installer_data">
                    <span>
                        <%= String.Format(InstallerResources.InstallComplete_Note, System.Security.Principal.WindowsIdentity.GetCurrent().Name) %></span>
                    <span>
                        <%= InstallerResources.InstallComplete_Note2 %><b><asp:LinkButton ID="lnkHere"
                            runat="server" OnClick="lnkHere_Click" Text='<%$ Code: InstallerResources.InstallComplete_lnkHere %>'></asp:LinkButton></b>
                        <%= InstallerResources.InstallComplete_Note3 %></span>
                </div>
            </div>
            <asp:HiddenField ID="isLoading" runat="server" Value="true" />

            <script type="text/javascript">

                function statusScroll() {
                    var isLoading = document.getElementById("<%= isLoading.ClientID %>");

                    var statusDiv = document.getElementById("logContainer");
                    var h = statusDiv.scrollHeight;
                    statusDiv.scrollTop = h;
                    if (isLoading.value == "False") return;
                    setTimeout(function() { statusScroll(); }, 10);
                }

                statusScroll();
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="radTick" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_ft" runat="Server">
</asp:Content>
