<%@ Control
    Language="C#"
    AutoEventWireup="true"
    Inherits="MonoSoftware.MonoX.ModuleGallery.EditProfile"
    CodeBehind="EditProfile.ascx.cs" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="monox" Namespace="MonoSoftware.MonoX.PrivacyManager" Assembly="MonoX" %>
<%@ Register Src="~/MonoX/Controls/TimeZonePicker.ascx" TagPrefix="monox" TagName="TimeZonePicker" %>

<asp:PlaceHolder ID="mainContainer" runat="server">
    <monox:PrivacyManager ID="privacyManager" runat="server" ImageButtonCssClass="privacy-button"></monox:PrivacyManager>
    <h3><asp:Label ID="lblAboutMySelf" AssociatedControlID="AboutMySelf" runat="server"></asp:Label></h3>
    <div id="rowAboutMySelf" runat="server" class="input-form about-me">
        <asp:Panel ID="panAboutMySelfPrivacyEditor" runat="server"></asp:Panel>
        <div class="control-holder">
            <asp:Label ID="prevAboutMySelf" runat="server"></asp:Label>
            <asp:TextBox runat="server" ID="AboutMySelf" TextMode="MultiLine" />
        </div>
    </div>
    <div class="input-form clearfix">
        <dl>
            <dd id="rowValidation" runat="server" CssClass="padding-bottom-20">
                <h3 id="rowTitle" runat="server"><asp:Literal ID="lblTitle" runat="server"></asp:Literal></h3>
                <div id="rowInfo" runat="server">
                    <span id="lblInfo" runat="server" class="edit-profile-subtitle"></span>
                </div>
                <asp:ValidationSummary ID="valSum" runat="server" Font-Bold="true" ShowSummary="true" CssClass="validation-summary" />
            </dd>
            <dd id="rowFirstName" runat="server">
                <asp:Label ID="labFirstName" AssociatedControlID="FirstName" runat="server"></asp:Label>
                <asp:Panel ID="panFristNamePrivacyEditor" runat="server"></asp:Panel>
                <div class="control-holder">
                    <asp:TextBox runat="server" ID="FirstName" />
                    <asp:RequiredFieldValidator runat="server" ID="reqFirstName" CssClass="validator validatorAdapter" SetFocusOnError="true" ControlToValidate="FirstName" Font-Bold="true" />
                </div>
            </dd>
            <dd id="rowLastName" runat="server">
                <asp:Label ID="labLastName" AssociatedControlID="LastName" runat="server"></asp:Label>
                <asp:Panel ID="panLastNamePrivacyEditor" runat="server"></asp:Panel>
                <div class="control-holder">
                    <asp:TextBox runat="server" ID="LastName" />
                    <asp:RequiredFieldValidator runat="server" ID="reqLastName" CssClass="validator validatorAdapter" SetFocusOnError="true" ControlToValidate="LastName" Font-Bold="true" />
                </div>
            </dd>
            <dd id="rowBirthDate" runat="server">
                <asp:Label ID="lblBirthDate" AssociatedControlID="BirthDate" runat="server"></asp:Label>
                <strong><asp:Label ID="prevBirthDate" runat="server"></asp:Label></strong>
                <asp:Panel ID="panBirthDayPrivacyEditor" runat="server"></asp:Panel>
                <div class="control-holder">
                    <telerik:RadDatePicker ID="BirthDate" runat="server"></telerik:RadDatePicker>
                </div>
            </dd>
            <dd id="rowAddress" runat="server">
                <asp:Label ID="lblAddress" AssociatedControlID="Address" runat="server"></asp:Label>
                <strong><asp:Label ID="prevAddress" runat="server"></asp:Label></strong>
                <asp:Panel ID="panAddressPrivacyEditor" runat="server"></asp:Panel>
                <div class="control-holder">
                    <asp:TextBox runat="server" ID="Address" />
                </div>
            </dd>
            <dd id="rowCity" runat="server">
                <asp:Label ID="lblCity" AssociatedControlID="City" runat="server"></asp:Label>
                <strong><asp:Label ID="prevCity" runat="server"></asp:Label></strong>
                <asp:Panel ID="panCityPrivacyEditor" runat="server"></asp:Panel>
                <div class="control-holder">
                    <asp:TextBox runat="server" ID="City" />
                </div>
            </dd>
            <dd id="rowZipCode" runat="server">
                <asp:Label ID="lblZipCode" AssociatedControlID="ZipCode" runat="server"></asp:Label>
                <strong><asp:Label ID="prevZipCode" runat="server"></asp:Label></strong>
                <asp:Panel ID="panZipCodePrivacyEditor" runat="server"></asp:Panel>
                <div class="control-holder">
                    <asp:TextBox runat="server" ID="ZipCode" />
                </div>
            </dd>
            <dd id="rowCountry" runat="server">
                <asp:Label ID="lblCountry" AssociatedControlID="Country" runat="server"></asp:Label>
                <strong><asp:Label ID="prevCountry" runat="server"></asp:Label></strong>
                <asp:Panel ID="panCountryPrivacyEditor" runat="server"></asp:Panel>
                <div class="control-holder">
                    <asp:TextBox runat="server" ID="Country" />
                </div>
            </dd>
            <dd id="rowWebSites" runat="server" class="profile-websites">
                <asp:Label ID="lblWebSites" AssociatedControlID="WebSites" runat="server"></asp:Label>
                <strong><asp:PlaceHolder ID="prevWebSites" runat="server"></asp:PlaceHolder></strong>
                <asp:Panel ID="panWebSitesPrivacyEditor" runat="server"></asp:Panel>
                <div class="control-holder">
                    <asp:TextBox runat="server" ID="WebSites" />
                </div>
            </dd>
            <dd id="rowEMail" runat="server">
                <asp:Label ID="lblEmail" AssociatedControlID="Email" runat="server"></asp:Label>
                <strong><asp:Label ID="prevEMail" runat="server"></asp:Label></strong>
                <asp:Panel ID="panEmailPrivacyEditor" runat="server"></asp:Panel>
                <div class="control-holder">
                    <asp:TextBox runat="server" ID="Email" />
                    <asp:RegularExpressionValidator ID="regexEmail" CssClass="validato ValidatorAdapter" ControlToValidate="Email"
                        Display="Dynamic" Font-Bold="true" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                        runat="server"></asp:RegularExpressionValidator>
                </div>
            </dd>
            <dd id="rowTimeZone" runat="server">
                <asp:Label ID="lblTimeZone" AssociatedControlID="ddlTimeZone" runat="server"></asp:Label>                
                <strong><asp:Label ID="prevTimeZone" runat="server"></asp:Label></strong>
                <asp:Panel ID="panTimeZonePrivacyEditor" runat="server"></asp:Panel>
                <div class="control-holder">
                    <monox:TimeZonePicker id="ddlTimeZone" runat="server"></monox:TimeZonePicker>
                </div>
            </dd>
            <dd id="rowPass" runat="server">
                <asp:Label ID="lblPassword" AssociatedControlID="Password" runat="server"></asp:Label>
                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
            </dd>
            <dd id="rowConfirmPass" runat="server">
                <asp:Label ID="lblConfirmPassword" AssociatedControlID="ConfirmPassword" runat="server"></asp:Label>
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                <asp:CompareValidator ID="PasswordCompare" runat="server" CssClass="validator validatorAdapter" SetFocusOnError="true" 
                ControlToCompare="Password" Font-Bold="true" ControlToValidate="ConfirmPassword"></asp:CompareValidator>
            </dd>
            
            <asp:Repeater ID="rptProfile" runat="server">
                <ItemTemplate>
                    <dd id="row" runat="server">
                        <asp:Label ID="labCaption" runat="server"></asp:Label>
                        <asp:Label ID="labValuePreview" runat="server"></asp:Label>
                        <asp:TextBox ID="txtTextValue" runat="server"></asp:TextBox>
                        <asp:CheckBox ID="txtBoolValue" runat="server" />
                        <telerik:RadDatePicker ID="txtDateValue" runat="server"></telerik:RadDatePicker>
                        <monox:PrivacyEditor id="privacyEditor" runat="server"></monox:PrivacyEditor>
                    </dd>
                </ItemTemplate>
            </asp:Repeater>
        </dl>
    </div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="noUserContainer" runat="server">
    <asp:Literal ID="labNoUser" runat="server"></asp:Literal>
</asp:PlaceHolder>
