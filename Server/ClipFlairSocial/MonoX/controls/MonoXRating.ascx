<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonoXRating.ascx.cs"
    Inherits="MonoSoftware.MonoX.Controls.MonoXRating" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:ScriptManagerProxy ID="proxySM" runat="server">
    <Scripts>
    </Scripts>
</asp:ScriptManagerProxy>
<asp:PlaceHolder ID="panMain" runat="server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:PlaceHolder ID="panHolder" runat="server">
            </asp:PlaceHolder>
            <telerik:RadToolTip ID="wndUpload" runat="server" RelativeTo="Element" 
                Skin="Default" Modal="true" Position="MiddleRight" ShowEvent="FromCode" ManualClose="true" RenderInPageRoot="true">
                <div style="padding: 10px;">                    
                    <strong><asp:Label ID="labRatingHistoryTitle" runat="server"></asp:Label></strong><br />
                    <asp:Label ID="labRatingHistorySubTitle" runat="server"></asp:Label>
                    <div style="max-height: 350px; overflow-y: auto; margin-top: 10px;">
                        <asp:Repeater ID="rpt" runat="server">
                            <ItemTemplate>
                                <asp:PlaceHolder ID="panHistoryItem" runat="server"></asp:PlaceHolder>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </telerik:RadToolTip>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:PlaceHolder>
