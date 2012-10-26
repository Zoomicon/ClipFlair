<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.ModuleGallery.MonoXPollModule"
    CodeBehind="PollModule.ascx.cs" %>
<asp:Panel runat="server" ID="pnlContainer">
<div class="poll">
    <div class="poll-question"><asp:Label runat="server" ID="lblQuestion"></asp:Label></div>
    
    <asp:Panel runat="server" ID="pnlInput">
        <asp:RadioButtonList runat="server" ID="rblAnswers">
        </asp:RadioButtonList>
        
        <div style="margin-top: 10px;">
            <asp:LinkButton runat="server" ID="btnVote" OnClick="btnVote_Click" CssClass="separatedList" /> 
            <asp:LinkButton runat="server" ID="btnResults" OnClick="btnResults_Click" />
        </div>
    </asp:Panel>
    
    <asp:Panel runat="server" ID="pnlGraph" Width="100%">
        <asp:Repeater runat="server" ID="rptGraph">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblAnswer" Text='<%# GetAnswerText(Eval("Answer").ToString(), int.Parse(Eval("Votes").ToString())) %>'></asp:Label><br />
                <asp:Panel runat="server" ID="pnlWidth" BackColor='<%# GetColor(Eval("Color").ToString()) %>' Width='<%# GetWidth(Eval("Votes").ToString()) %>' Height="15px" CssClass='<%# GetColor(Eval("Color").ToString()).Equals(System.Drawing.Color.Empty) ? "poll-bg-color" : String.Empty  %>'></asp:Panel>
            </ItemTemplate>
            <SeparatorTemplate>
                <div class="separator">
                </div>
            </SeparatorTemplate>
        </asp:Repeater>        
        <div style="margin-top: 10px; font-weight: bold;"><asp:Label runat="server" ID="lblTotal"></asp:Label></div>
        
        <asp:LinkButton runat="server" ID="btnShowVotePanel" OnClick="btnShowVotePanel_Click" />&nbsp;
    </asp:Panel>
</div>
</asp:Panel>
