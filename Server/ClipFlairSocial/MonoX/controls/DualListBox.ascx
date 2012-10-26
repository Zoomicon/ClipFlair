<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Controls.DualListBox" Codebehind="DualListBox.ascx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<table>
  <tr>
    <td>
      <asp:Label ID="labLeftHeaderText" runat="server" ></asp:Label>
    </td>
    <td style="vertical-align:middle;" colspan="<%= rowLeftReorder.Visible ? "2" : "1" %>">
    </td>
    <td>
      <asp:Label ID="labRightHeaderText" runat="server" ></asp:Label>
    </td>
  </tr>
  <tr>
    <td>
      <asp:ListBox runat="server" ID="lbLeft" ></asp:ListBox>
    </td>
    <td id="rowLeftReorder" runat="server">
      <asp:Image runat="server" ID="reorderLeftUp" AlternateText="Reorder - Up" ToolTip="Reorder - Up" />
      <br />
      <br />
      <br />
      <br />
      <br />
      <asp:Image runat="server" ID="reorderLeftDown" AlternateText="Reorder - Down" ToolTip="Reorder - Down"/>
    </td>
    <td style="vertical-align:middle;">
      <asp:Image runat="server" ID="addImg" AlternateText="Add" ToolTip="Add" />
      <br />
      <asp:Image runat="server" ID="removeImg" AlternateText="Remove" ToolTip="Remove"/>
    </td>
    <td>
      <asp:ListBox runat="server" ID="lbRight" ></asp:ListBox>
    </td>
    <td id="rowRightReorder" runat="server">
      <asp:Image runat="server" ID="reorderRightUp" AlternateText="Reorder - Up" ToolTip="Reorder - Up" />
      <br />
      <br />
      <br />
      <br />
      <br />
      <asp:Image runat="server" ID="reorderRightDown" AlternateText="Reorder - Down" ToolTip="Reorder - Down"/>
    </td>
  </tr>
  <tr>
    <td>
      <asp:Label ID="labLeftFooterText" runat="server" ></asp:Label>
    </td>
    <td style="vertical-align:middle;" colspan="<%= rowRightReorder.Visible ? "2" : "1" %>">
    </td>
    <td>
      <asp:Label ID="labRightFooterText" runat="server" ></asp:Label>
    </td>
  </tr>
  <tr colspan="5">
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td colspan="5">
      <asp:Label ID="labMaxErrorMessage" runat="server" style="display:none;"><%= DefaultResources.DualListBox_MaxErrorMessage %></asp:Label>
      <asp:Label ID="labMinErrorMessage" runat="server" style="display:none;"><%= DefaultResources.DualListBox_MinErrorMessage %></asp:Label>
      <asp:Label ID="labMinItemsRequired" runat="server" style="display:none;" ></asp:Label>
    </td>
  </tr>
</table>    
