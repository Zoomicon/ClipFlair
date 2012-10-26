<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SilverlightUploadModule.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SilverlightUploadModule" %>
<%@ Register TagPrefix="MonoX" TagName="FileGallery" Src="~/MonoX/ModuleGallery/SocialNetworking/FileGallery.ascx" %>
<div class="jq_monoxUploadContainer silverlight-upload">
    <asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server">
    </asp:ScriptManagerProxy>
    <asp:HyperLink runat="server" NavigateUrl="javascript:void(0);" CssClass="upload-button jq_uploadAction"
        ID="lnkUpload" ></asp:HyperLink>
    <asp:Panel ID="pnlUpload" runat="server" CssClass="jq_silverlightUploadModuleBox">
        <object id="SilverlightUpload" data="data:application/x-silverlight," type="application/x-silverlight-2"
            width="<%= Width %>" height="<%= Height %>" >
            <param name="source" value='<%= ResolveUrl("~/MonoX/Silverlight/Upload.xap") %>' />
            <param name="background" value="white" />
            <param name="minRuntimeVersion" value="2.0.31005.0" />
            <param name="autoUpgrade" value="true" />
            <param name="windowless" value="true" />
            <param name="InitParams" value='<%= InitParameters %>' />            
            <a href="http://go.microsoft.com/fwlink/?LinkID=124807" style="text-decoration: none;">
                <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                    style="border-style: none" />
            </a>
        </object>
    </asp:Panel>
    <asp:Button runat="server" ID="btnPostback" CssClass="silverlightBtnPostback" CausesValidation="false" />
    <asp:UpdatePanel id="up" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="handlerResponse" runat="server"  />            
            <MonoX:FileGallery ID="fileGallery" runat="server"></MonoX:FileGallery>
        </ContentTemplate>
    </asp:UpdatePanel>    
</div>
