<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditProfile.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Mobile.ProfileModule.EditProfile" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="monox" Namespace="MonoSoftware.MonoX.PrivacyManager" Assembly="MonoX" %>
<%@ Register Src="~/MonoX/Controls/TimeZonePicker.ascx" TagPrefix="monox" TagName="TimeZonePicker" %>

<asp:PlaceHolder ID="mainContainer" runat="server">
    <monox:PrivacyManager Enabled="false" ID="privacyManager" runat="server" ImageButtonCssClass="privacy-button"
        CssClass="privacy-content"></monox:PrivacyManager>
    <div class="input-form">
        <ul data-role="listview">
            <li id="rowValidation" runat="server">
                <h3 id="rowTitle" runat="server">
                    <asp:Literal ID="lblTitle" runat="server"></asp:Literal></h3>
                <div id="rowInfo" runat="server">
                    <span id="lblInfo" runat="server"></span>
                </div>
                <asp:ValidationSummary ID="valSum" runat="server" Font-Bold="true" CssClass="validation-summary"  ShowSummary="true"  />
            </li>
            <li id="rowFirstName" runat="server">
                <asp:Label ID="labFirstName" AssociatedControlID="FirstName" runat="server" CssClass="label-bold"></asp:Label>
                <asp:TextBox runat="server" ID="FirstName" />
                <asp:RequiredFieldValidator runat="server" ID="reqFirstName" CssClass="ValidatorAdapter" SetFocusOnError="true"
                    ControlToValidate="FirstName" Font-Bold="true" />
            </li>
            <li id="rowLastName" runat="server">
                <asp:Label ID="labLastName" AssociatedControlID="LastName" runat="server" CssClass="label-bold"></asp:Label>
                <asp:TextBox runat="server" ID="LastName" />
                <asp:RequiredFieldValidator runat="server" ID="reqLastName" CssClass="ValidatorAdapter" SetFocusOnError="true"
                    ControlToValidate="LastName" Font-Bold="true" />
            </li>
            <asp:PlaceHolder ID="plhDob" runat="server" Visible="false">
            <li id="rowBirthDate" runat="server">
                <asp:Label ID="lblBirthDate" AssociatedControlID="BirthDate" runat="server" CssClass="label-bold"></asp:Label>
                <strong><asp:Label ID="prevBirthDate" runat="server"></asp:Label></strong>
                <div style="display: block;">
                    <telerik:RadDatePicker ID="BirthDate" runat="server">
                    </telerik:RadDatePicker>
                </div>
            </li>
            </asp:PlaceHolder>
            <li id="rowAddress" runat="server">
                <asp:Label ID="lblAddress" AssociatedControlID="Address" runat="server" CssClass="label-bold"></asp:Label>
                <strong><asp:Label ID="prevAddress" runat="server"></asp:Label></strong>
                <asp:TextBox runat="server" ID="Address" />
            </li>
            <li id="rowCity" runat="server">
                <asp:Label ID="lblCity" AssociatedControlID="City" runat="server" CssClass="label-bold"></asp:Label>
                <strong><asp:Label ID="prevCity" runat="server"></asp:Label></strong>
                <asp:TextBox runat="server" ID="City" />
            </li>
            <li id="rowZipCode" runat="server">
                <asp:Label ID="lblZipCode" AssociatedControlID="ZipCode" runat="server" CssClass="label-bold"></asp:Label>
                <strong><asp:Label ID="prevZipCode" runat="server"></asp:Label></strong>
                <asp:TextBox runat="server" ID="ZipCode" />
            </li>
            <li id="rowCountry" runat="server">
                <asp:Label ID="lblCountry" AssociatedControlID="Country" runat="server" CssClass="label-bold"></asp:Label>
                <strong><asp:Label ID="prevCountry" runat="server"></asp:Label></strong>
                <asp:TextBox runat="server" ID="Country" />
            </li>
            <li id="rowWebSites" runat="server">
                <asp:Label ID="lblWebSites" AssociatedControlID="WebSites" runat="server" CssClass="label-bold"></asp:Label>
                <strong><asp:PlaceHolder ID="prevWebSites" runat="server"></asp:PlaceHolder></strong>
                <asp:TextBox runat="server" ID="WebSites" />
            </li>
            <li id="rowEMail" runat="server">
                <asp:Label ID="lblEmail" AssociatedControlID="Email" runat="server" CssClass="label-bold"></asp:Label>
                <strong><asp:Label ID="prevEMail" runat="server"></asp:Label></strong>
                <asp:TextBox runat="server" ID="Email" />
                <asp:RegularExpressionValidator ID="regexEmail" CssClass="ValidatorAdapter" ControlToValidate="Email"
                    Display="Dynamic" Font-Bold="true" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                    runat="server"></asp:RegularExpressionValidator>
            </li>
            <asp:PlaceHolder ID="plhTimeZone" runat="server" Visible="false">
                <asp:Label ID="lblTimeZone" AssociatedControlID="ddlTimeZone" runat="server" CssClass="label-bold"></asp:Label>                
                <strong><asp:Label ID="prevTimeZone" runat="server"></asp:Label></strong>
                <monox:TimeZonePicker id="ddlTimeZone" runat="server"></monox:TimeZonePicker>
            </asp:PlaceHolder>
            <li id="rowPass" runat="server">
                <asp:Label ID="lblPassword" AssociatedControlID="Password" runat="server" CssClass="label-bold"
                    Width="85%"></asp:Label>
                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
            </li>
            <li id="rowConfirmPass" runat="server">
                <asp:Label ID="lblConfirmPassword" AssociatedControlID="ConfirmPassword" runat="server"
                    CssClass="label-bold" Width="85%"></asp:Label>
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
            </li>
            <asp:CompareValidator Display="Dynamic" ID="PasswordCompare" runat="server" CssClass="ValidatorAdapter" SetFocusOnError="true"
                ControlToCompare="Password" Font-Bold="true" ControlToValidate="ConfirmPassword"></asp:CompareValidator>             
            <asp:Repeater ID="rptProfile" runat="server">
                <ItemTemplate>
                    <li id="row" runat="server">                        
                        <asp:Label ID="labCaption" runat="server"  CssClass="label-bold"></asp:Label>
                        <asp:Label ID="labValuePreview" runat="server"></asp:Label>
                        <asp:TextBox ID="txtTextValue" runat="server"></asp:TextBox>
                        <asp:CheckBox ID="txtBoolValue" runat="server" />
                        <telerik:RadDatePicker ID="txtDateValue"  runat="server">
                        </telerik:RadDatePicker>
                        <monox:PrivacyEditor id="privacyEditor" runat="server" CssClass="privacy-content"></monox:PrivacyEditor>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <h3>
    <asp:Label ID="lblAboutMySelf" AssociatedControlID="AboutMySelf" runat="server"></asp:Label></h3>
    <div class="input-form">
        <dl>
            <div id="rowAboutMySelf" runat="server">
                <asp:Label ID="prevAboutMySelf" runat="server"></asp:Label>
                <asp:TextBox runat="server" ID="AboutMySelf" TextMode="MultiLine" />
            </div>
        </dl>
    </div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="noUserContainer" runat="server">
    <asp:Literal ID="labNoUser" runat="server"></asp:Literal>
</asp:PlaceHolder>
