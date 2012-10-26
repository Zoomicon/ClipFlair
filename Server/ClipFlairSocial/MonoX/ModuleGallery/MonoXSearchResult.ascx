<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.MonoXSearchResult" Codebehind="MonoXSearchResult.ascx.cs" %>
<%@ Register TagPrefix="mono" Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" %>
 
<div id="plhSearchResults" runat="server" visible="false">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <asp:Literal ID="labNoSearchProviderSelected" runat="server"></asp:Literal>
                <asp:Literal ID="labResults" runat="server"></asp:Literal>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <mono:pager id="pager" runat="server" AutoPaging="False" AllowCustomPaging="True"  NumericButtonCount="5" PageSize="10">
				    <PAGERTEMPLATE>
				        <%-- NOTE: Pager template is loaded dynamically via templating system --%>					    
				    </PAGERTEMPLATE>
			    </mono:pager>        
            </td>
        </tr>
    </table>
    <br />
</div>

