<%@ Control Language="C#" 
    AutoEventWireup="True" 
    CodeBehind="EventEditor.ascx.cs" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.EventEditor" %>

<%@ Import Namespace="MonoSoftware.MonoX.Resources"%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
    
<div class="event-module-form input-form">
    <h2><%= EventModuleResources.EventEditorTitle %></h2>
    <div id="Div1" runat="server">
        <asp:ValidationSummary ID="validationSummary" CssClass="validation-summary" runat="server" />
    </div>
    <dl>
        <dd>
            <label for="<%= txtTitle.ClientID %>" class="short-label"><%= EventModuleResources.Title %></label>
            <asp:Label ID="lblTitle" runat="server"></asp:Label>
            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            <asp:Label ID="prevTitle" runat="server" Visible="false"></asp:Label>
            <asp:RequiredFieldValidator ID="vldRequiredSubject" runat="server" ControlToValidate="txtTitle" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
        </dd>
        <dd>
            <label for="<%= dateFrom.ClientID %>"><%= EventModuleResources.StartTime %>:</label>
            <telerik:RadDateTimePicker ID="dateFrom" runat="server"></telerik:RadDateTimePicker>
            <asp:RequiredFieldValidator ID="vldRequiredDateFrom" runat="server" ControlToValidate="dateFrom" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
            <asp:CompareValidator ID="vldCompareRange" runat="server" ControlToValidate="dateFrom" ControlToCompare="dateTo" Operator="LessThanEqual" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic"></asp:CompareValidator>
            
            <div ID="pnlDateTo" runat="server">
                <label for="<%= dateTo.ClientID %>"><%= EventModuleResources.EndTime %>:</label>
                <telerik:RadDateTimePicker ID="dateTo" runat="server"></telerik:RadDateTimePicker>
                <asp:RequiredFieldValidator ID="vldRequiredDateTo" runat="server" ControlToValidate="dateTo" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
            </div>
                
            <asp:CheckBox ID="chkAllDay" runat="server" />
            <label for="<%= chkAllDay.ClientID %>"><%= EventModuleResources.AllDay %></label>
        </dd>
        <dd>
            <label for="<%= txtDescription.ClientID %>"><%= EventModuleResources.Description %></label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
            <asp:Label ID="prevDescription" runat="server" Visible="false"></asp:Label>
        </dd>
        <dd>
            <label for="<%= txtPlace.ClientID %>" class="short-label"><%= EventModuleResources.Place %></label>
            <asp:TextBox ID="txtPlace" runat="server"></asp:TextBox>
            <asp:Label ID="prevPlace" runat="server" Visible="false"></asp:Label>
        </dd>
    </dl>
    <div class="input-form">
        <div class="button-holder">
            <MonoX:StyledButton ID="btnSave" runat="server" CssClass="CssFormButton"></MonoX:StyledButton>
            <MonoX:StyledButton ID="btnCancel" runat="server" CausesValidation="false" CssClass="CssFormButton"></MonoX:StyledButton>
        </div>
    </div>
</div>
