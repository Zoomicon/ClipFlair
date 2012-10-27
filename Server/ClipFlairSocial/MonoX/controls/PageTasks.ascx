<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Controls.PageTasks" Codebehind="PageTasks.ascx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="LocaleChanger" Src="~/MonoX/controls/LocaleChanger.ascx" %>

<div class="dropdownSelections">
    <table class="tasksLayout" width="1000" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td style="width:375px; padding-top: 20px;" align="right" valign="top">
        	<%= AdminResources.PageTasks_CurrentLanguageText %> <MonoX:LocaleChanger ID="localeChanger" runat="server" />
        </td>
        <td style="width:25px;" align="left" valign="bottom">&nbsp;</td>
        <td style="width:600px; padding-top: 20px;" align="left" valign="bottom">
            <span class="topView"><asp:LinkButton ID="lnkPerUserScope" runat="server" OnClick="btnPerUserScope_Click" CausesValidation="false"><%= AdminResources.PageTasks_UserViewText %></asp:LinkButton>&nbsp;&nbsp;<asp:ImageButton ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.userView_gif %>" runat="server" ID="btnPerUserScope" ImageAlign="Middle" BorderStyle="None" OnClick="btnPerUserScope_Click"  CausesValidation="false"/></span>
            <span class="topView"><asp:LinkButton ID="lnkSharedScope" runat="server" OnClick="btnSharedScope_Click" CausesValidation="false"><%= AdminResources.PageTasks_SharedViewText %></asp:LinkButton>&nbsp;&nbsp;<asp:ImageButton ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.sharedView_gif %>" runat="server" ID="btnSharedScope" ImageAlign="Middle" BorderStyle="None" OnClick="btnSharedScope_Click"  CausesValidation="false"/></span>

        </td>
      </tr>
          <tr>
            <td style="width:375px;" align="center" valign="top">
            <h1 class="titleTwo"><%= AdminResources.PageTasks_ShortDescriptionTitle %></h1>
            <p class="moduleDescription"><asp:Label runat="server" ID="lblShortDescription"></asp:Label></p>
            </td>
            <td style="width:25px;" align="left">&nbsp;</td>
            <td style="width:600px;" align="left" valign="top">
            
           	  <h1 class="titleOne"><%= AdminResources.PageTasks_PageTasksTitle %></h1>
                <asp:Panel runat="server" ID="pnlTasks">
                    <div class="pageTasksBtn"><asp:LinkButton ID="lnkBrowse" runat="server" OnClick="lbtBrowse_Click" CausesValidation="false"><%= AdminResources.PageTasks_BrowseModeText %></asp:LinkButton><br /><asp:ImageButton ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.browseModeIcon_gif %>" runat="server" CommandArgument="browse" ID="btnBrowse" ImageAlign="AbsBottom" BorderStyle="None" OnClick="lbtBrowse_Click" CssClass="pageTasksImg"  CausesValidation="false"/></div>
                    <div class="pageTasksBtn"><asp:LinkButton ID="lnkDesign" runat="server" OnClick="lbtDesign_Click" CausesValidation="false"><%= AdminResources.PageTasks_DesignModeText %></asp:LinkButton><br /><asp:ImageButton ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.designModeIcon_gif %>" runat="server" CommandArgument="design" ID="btnDesign" ImageAlign="AbsBottom" BorderStyle="None" OnClick="lbtDesign_Click" CssClass="pageTasksImg" CausesValidation="false"/></div>
                    <div class="pageTasksBtn"><asp:LinkButton ID="lnkRevision" runat="server" OnClick="lnkCreateRevision_Click" CausesValidation="false"><asp:Label ID="lblRevision" runat="server"></asp:Label></asp:LinkButton><br /><asp:ImageButton ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.reviseContentIcon_gif %>" runat="server" ID="btnRevision" ImageAlign="AbsBottom" BorderStyle="None" OnClick="lnkCreateRevision_Click" CssClass="pageTasksImg" CausesValidation="false" /></div>
                    <div class="pageTasksBtn"><asp:LinkButton ID="lnkCatalog" runat="server" OnClick="lbtCatalog_Click" CausesValidation="false"><%= AdminResources.PageTasks_CatalogText %></asp:LinkButton><br /><asp:ImageButton ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.partCatalogIcon_gif %>" runat="server" ID="btnCatalog" ImageAlign="AbsBottom" BorderStyle="None" OnClick="lbtCatalog_Click" CssClass="pageTasksImg"  CausesValidation="false"/></div>
                    <div class="pageTasksBtn"><asp:LinkButton ID="lnkConnect" runat="server" OnClick="lnkConnect_Click" CausesValidation="false"><%= AdminResources.PageTasks_ConnectText %></asp:LinkButton><br /><asp:ImageButton ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.connectPartsIcon_gif %>" runat="server" ID="btnConnect" ImageAlign="AbsBottom" BorderStyle="None" OnClick="lnkConnect_Click" CssClass="pageTasksImg" CausesValidation="false" /></div>
                    <asp:PlaceHolder ID="pnlApprove" runat="server" Visible="False">
                    <div class="pageTasksBtn"><asp:LinkButton ID="lnkApproveChanges" runat="server" OnClick="lnkApproveChanges_Click" CausesValidation="false"><%= AdminResources.PageTasks_ApproveChangesText %></asp:LinkButton><br /><asp:ImageButton ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.aproveContentActiveIcon_gif %>" runat="server" ID="btnApproveChanges" ImageAlign="AbsBottom" BorderStyle="None" OnClick="lnkApproveChanges_Click" CssClass="pageTasksImg"  CausesValidation="false"/></div>
                    </asp:PlaceHolder>
                </asp:Panel>
             </td>
          </tr>
        </table>
    <input type="button" style="display:none" id="btnAjaxClick" onclick="return false;" />
</div>
