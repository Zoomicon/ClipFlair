<%@ Control
    Language="C#"
    AutoEventWireup="true"
    Inherits="MonoSoftware.MonoX.ModuleGallery.MonoXPollModule"
    CodeBehind="PollModule.ascx.cs" %>

<asp:Panel runat="server" ID="pnlContainer">
    <div class="poll">
        <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <h2><asp:Label runat="server" ID="lblQuestion"></asp:Label></h2>
                <asp:Panel runat="server" ID="pnlInput" Width="100%">
                    <asp:RadioButtonList runat="server" ID="rblAnswers" CellPadding="5">
                    </asp:RadioButtonList>
                    <div class="button-holder">
                        <asp:LinkButton runat="server" ID="btnVote" OnClick="btnVote_Click" CssClass="styled-button" /> 
                        <asp:LinkButton runat="server" ID="btnResults" OnClick="btnResults_Click" CssClass="styled-button" />
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlGraph" Width="100%">
                    <asp:Repeater runat="server" ID="rptGraph">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAnswer" CssClass="answer" Text='<%# GetAnswerText(Eval("Answer").ToString(), int.Parse(Eval("Votes").ToString())) %>'></asp:Label>
                            <div class="poll-holder">
                                <asp:Panel runat="server" ID="pnlWidth" BackColor='<%# GetColor(Eval("Color").ToString()) %>' Width='<%# GetWidth(Eval("Votes").ToString()) %>' CssClass='<%# GetColor(Eval("Color").ToString()).Equals(System.Drawing.Color.Empty) ? "poll-bg-color" : String.Empty  %>'></asp:Panel>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="button-holder">
                        <asp:LinkButton runat="server" ID="btnShowVotePanel" OnClick="btnShowVotePanel_Click" CssClass="styled-button" />
                        <strong class="total-votes"><asp:Label runat="server" ID="lblTotal"></asp:Label></strong>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Panel>