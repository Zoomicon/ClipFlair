<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Samples.HelloWorld" Codebehind="HelloWorld.ascx.cs" %>

<p>
Please enter your name: <asp:TextBox runat="server" ID="txtName" CssClass="borderlessInput"></asp:TextBox> <asp:Button runat="server" ID="btnOk" Text="OK" CssClass="SilverButton" OnClick="btnOk_Click" />
</p>
<p>
<asp:Label ID="lblGreeting" runat="server"></asp:Label>
</p>
