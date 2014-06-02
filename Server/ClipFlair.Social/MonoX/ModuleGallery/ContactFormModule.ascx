<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.ContactFormModule" 
    Codebehind="ContactFormModule.ascx.cs" %>
<%@ Register Src="~/MonoX/ModuleGallery/CaptchaModule.ascx" TagPrefix="monox" TagName="CaptchaModule" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>

<!--CLIPFLAIR-->
<div class="contact-us input-form">
    <div class="span8">
        <h1><asp:Literal ID="lblTitle" runat="server"></asp:Literal></h1> 
        <dl>
            <dd>
                <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="Modification" CssClass="validation-summary" ShowSummary="true" EnableClientScript="true" />
            </dd>
            <dd>
                <asp:Label ID="lblName" AssociatedControlID="txtName" runat="server" CssClass="label-width"></asp:Label>
                <asp:TextBox ID="txtName" runat="server" class="short-textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqName" runat="server" CssClass="validator ValidatorAdapter" ControlToValidate="txtName" SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
            </dd>
            <dd>
                <asp:Label ID="lblEMail" AssociatedControlID="txtEMail" runat="server" CssClass="label-width"></asp:Label>
                <asp:TextBox ID="txtEMail" runat="server" class="short-textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqEMail" runat="server" CssClass="validator ValidatorAdapter" ControlToValidate="txtEMail" SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regexEmail" CssClass="validator ValidatorAdapter" ControlToValidate="txtEMail"
                    Display="Dynamic" Font-Bold="true" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                    runat="server"></asp:RegularExpressionValidator>
            </dd>
            <dd>
                <asp:Label ID="lblSubject" AssociatedControlID="txtSubject" runat="server" CssClass="label-width"></asp:Label>
                <asp:TextBox ID="txtSubject" runat="server" class="short-textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqSubject" runat="server" CssClass="validator ValidatorAdapter" ControlToValidate="txtSubject" SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
            </dd>
            <dd>
                <asp:Label ID="lblMessage" AssociatedControlID="txtMessage" runat="server"></asp:Label>
                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </dd>
            <dd>
                <monox:CaptchaModule ID="captchaModule" runat="server" />
            </dd>
            <dd>
                <strong><asp:Label ID="lblInfo" runat="server" ></asp:Label></strong>
            </dd>
            <dd>
                <!--CLIPFLAIR-->
                <asp:LinkButton ID="btnSend" runat="server" CausesValidation="true" OnClick="btnSend_Click" CssClass="styled-button main-button send-btn float-left" />
            </dd>
        </dl>
    </div>
    <div ID="panInfo"  runat="server" class="span4">
        <div class="contact-info">
            <div class="inner">
                <div class="header-title"><%# DefaultResources.ContactForm_ContactInfoTitle %></div>
                <div class="contact-info-line"><asp:PlaceHolder ID="panWebSiteName" runat="server"><strong><%# A_WebSiteName%></strong><span></span></asp:PlaceHolder></div>
                <div class="contact-info-line"><asp:PlaceHolder ID="panPhone" runat="server"><strong><%# DefaultResources.ContactForm_PhoneLabel %></strong><span><%# A_Phone%></span></asp:PlaceHolder></div>
                <div class="contact-info-line"><asp:PlaceHolder ID="panFax" runat="server"><strong><%# DefaultResources.ContactForm_FaxLabel %></strong><span><%# A_Fax%></span></asp:PlaceHolder></div>
                <div class="contact-info-line"><asp:PlaceHolder ID="panAddress" runat="server"><strong><%# DefaultResources.ContactForm_AddressLabel %></strong><span><%# A_Address%></span></asp:PlaceHolder>
                <asp:PlaceHolder ID="panZipCode" runat="server"><strong></strong><span><%# A_ZipCode%><%# A_City%></span></asp:PlaceHolder>
                <asp:PlaceHolder ID="panCountry" runat="server"><strong></strong><span><%# A_Country%></span></asp:PlaceHolder></div>
            </div>
        </div>
    </div>
</div>