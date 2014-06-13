<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Message" 
    Codebehind="Message.aspx.cs" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
     <div class="container-highlighter" style="background-color:#38595b">
        <div class="container">
            <p></p>
        </div>              
    </div>
    <div class="container">
        <div class="fancybox-container login-cont">
            <div class="row-fluid">
                <div class="span12 clearfix">       
                    <div class="span12 clearfix text-center" style="padding:40px 0;">
                        <h2><%= Title %></h2>
                        <div><%= Description %></div>
                    </div>
                </div>           
            </div> 
        </div>
    </div>
   

</asp:Content>
 