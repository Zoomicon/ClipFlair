<%@ Page Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Admin.PageManagerPageCrawl" EnableTheming="true" Codebehind="PageManagerPageCrawl.aspx.cs" Theme="DefaultAdmin" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Check page status</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="AjaxScriptManager" EnablePageMethods="True" runat="server">
        <Scripts>
        </Scripts>
    </asp:ScriptManager>

    <asp:Timer ID="radTick" runat="server" Interval="1000" />
        <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
            <ContentTemplate>
            <div class="CssForm">
            <div class="popupBox">
                <div class="header">
                    <div class="headerContent">
                        <h2 class="toolIcon"><asp:Literal runat="server" ID="ltlTitle" Text='<%$ Code: AdminResources.PageManagerPageCrawl_ltlTitle %>'></asp:Literal>
                        </h2>
                        <p>
                            <asp:Label ID="lblHeader" runat="server" Text='<%$ Code: AdminResources.PageManagerPageCrawl_lblHeader %>'></asp:Label>        
                        </p>
                        <br />
                        <asp:CheckBox ID="chkShowAllMessages" runat="server" Text='<%$ Code: AdminResources.PageManagerPageCrawl_chkShowAllMessages %>' Checked="true" />

                    </div>
                </div>
                <div class="content"> 
                    <div id="logContainer" class="installer_log">
                        <asp:Panel ID="panStatus" runat="server">
                            <asp:Literal ID="labStatus" runat="server">
                            </asp:Literal>
                        </asp:Panel>
                        <div style="width: 95%;">
                            <div id="imgStatusHolder" runat="server" class="invisibleLoader">
                                <img id="imgStatus" runat="server" src="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.Common.img.ajaxLoader_gif %>" />
                            </div>
                        </div>
                    </div>
                
                </div>
                <div class="footer">
                    <asp:Button ID="btnStart" runat="server" AccessKey="O" OnClick="btnStart_Click" CssClass="AdminButton" Text='<%$ Code: AdminResources.PageManagerPageCrawl_btnStart %>' /> <asp:button id="btnClose" OnClientClick="CloseWindow()" Text='<%$ Code: AdminResources.Button_Close %>' CssClass="AdminButton" runat="server"></asp:button>
                </div>
            
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

                //statusScroll();
            </script>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="radTick" />
        </Triggers>
    </asp:UpdatePanel>
            
            
    </form>
</body>
</html>
