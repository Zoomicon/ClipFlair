<%@ Control Language="C#" 
AutoEventWireup="true" 
CodeBehind="SocialDisconnect.ascx.cs" 
Inherits="MonoSoftware.MonoX.ModuleGallery.SocialDisconnect" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<div class="input-form">
    <div class="social-login clearfix">
        <asp:UpdatePanel runat="server" ID="upSocialLogin" UpdateMode="Always">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnlContainer">
                        <asp:ListView runat="server" ID="lvItems">
                            <LayoutTemplate>
                                <div class="input-form">
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </div>   
                            </LayoutTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <p><%= DefaultResources.SocialDisconnect_EmptyButtonList %></p>
                            </EmptyDataTemplate>
                        </asp:ListView>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>