<%@ Control 
  Language="C#" 
  AutoEventWireup="true" 
  Inherits="MonoSoftware.MonoX.Admin.DatePicker" Codebehind="DatePicker.ascx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

    <table cellspacing="1px" cellpadding="0" border="0">
      <tr>
        <td>
            <rad:RadDatePicker id="picker" Runat="server" Calendar-Skin="Default2006">                                                    
                <datepopupbutton ></datepopupbutton>
            </rad:RadDatePicker>
            <asp:RequiredFieldValidator ID="requiredPicker" runat="server" Enabled="false" CssClass="ValidatorAdapter" SetFocusOnError="true" ControlToValidate="picker" Text="!" ErrorMessage='<%$ Code: AdminResources.DatePicker_RequiredPicker_ErrorMessage %>'></asp:RequiredFieldValidator>
        </td>
      </tr>
    </table>

