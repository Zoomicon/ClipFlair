<%@ Control
    Language="C#"
    AutoEventWireup="true"
    Inherits="MonoSoftware.MonoX.Samples.HelloWorld" Codebehind="HelloWorld.ascx.cs" %>

<div class="input-form">
    <dl>
        <dd>
            <label>Please enter your name:</label>
            <asp:TextBox runat="server" ID="txtName" CssClass="borderlessInput"></asp:TextBox>
        </dd>
        <dd>
            <asp:Button runat="server" ID="btnOk" Text="OK" CssClass="styled-button" OnClick="btnOk_Click" />
        </dd>
        <dd>
            <asp:Label ID="lblGreeting" runat="server"></asp:Label>
        </dd>
    </dl>
</div>