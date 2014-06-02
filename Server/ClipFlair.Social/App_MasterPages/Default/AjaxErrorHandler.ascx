<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AjaxErrorHandler.ascx.cs"
    Inherits="MonoSoftware.MonoX.MasterPages.AjaxErrorHandler" %>

<asp:ScriptManagerProxy ID="AjaxScriptManager" runat="server" >
</asp:ScriptManagerProxy> 
        
<asp:PlaceHolder ID="errorHandlerScript" runat="server">

    <script type="text/javascript">            
                //<![CDATA[
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                function EndRequestHandler(sender, args)
                {
                   if (args.get_error() != undefined && args.get_error().httpStatusCode == '500')
                   {
                       var errorMessage = args.get_error().message
                       args.set_errorHandled(true);                   
                       if (<%# (MonoSoftware.MonoX.Utilities.SecurityUtility.IsAdmin() || IsErrorAlertEnabled()).ToString().ToLower() %>)
                            alert(errorMessage.replace("Sys.WebForms.PageRequestManagerServerErrorException: ", ""));                   
                       else 
                            window.location.href = '<%# GetErrorRedirectionUrl() %>';                   
                   }
                }
                //]]>            
    </script>

</asp:PlaceHolder>
