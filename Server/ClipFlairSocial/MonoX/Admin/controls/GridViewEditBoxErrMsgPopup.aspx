<%@ Page 
    Title="Error message" 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Main.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.GridViewEditBoxErrMsgPopup" 
    Theme="DefaultAdmin"    
    Codebehind="GridViewEditBoxErrMsgPopup.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" Runat="Server">


    <div class="errorPopupContent">
        <div style="padding: 10px;">
            <img src="<%= ResolveUrl(MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.ErrorMessage_icon_png) %>" style="float: left; margin: 0px 20px 20px 0px;"/>
            <span id="exception">            
            </span>
            <br /><br /><br />
            <span id="innerException" >
            </span>                    
        </div>
    </div>
    <div class="errorPopupButtons">        
        <asp:Button ID="btnClose" runat="server" Text="Close" />
    </div>
    

    <asp:PlaceHolder ID="scriptHolder" runat="server">
        <script type="text/javascript">
            var exception = document.getElementById('exception');
            exception.innerHTML = parent.document.getElementById('<%# GridBoxExceptionControlId %>').getAttribute('value');
            var innerException = document.getElementById('innerException');
            innerException.innerHTML = parent.document.getElementById('<%# GridBoxInnerExceptionControlId %>').getAttribute('value');

            //This code is used to provide a reference to the radwindow 'wrapper'
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            } 
            
        </script>
    </asp:PlaceHolder>

    
</asp:Content>

