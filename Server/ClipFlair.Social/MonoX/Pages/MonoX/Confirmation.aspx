<%@ Page 
    Title=""
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master" 
    AutoEventWireup="true"
    CodeBehind="Confirmation.aspx.cs" 
    Inherits="MonoSoftware.MonoX.Pages.Confirmation"  %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register TagPrefix="MonoX" TagName="Confirmation" Src="~/MonoX/ModuleGallery/Confirmation.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
   
     <div class="container-highlighter" style="background-color:#38595b">
        <div class="container">
            <p></p>
        </div>              
    </div>
    <div class="container">
        <div class="fancybox-container login-cont">
            <div class="row-fluid">
                <div class="span12 clearfix">       
                    <MonoX:Confirmation ID="ctlConfirmation" runat="server" />
                </div>           
            </div> 
        </div>
    </div>
   
</asp:Content>
