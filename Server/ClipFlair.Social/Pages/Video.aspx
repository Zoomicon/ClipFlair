<%@ Page 
      Language="C#" 
      MasterPageFile="~/MonoX/MasterPages/Default.master"  
      AutoEventWireup="true" 
      Title="ClipFlair Video"
      Theme="ClipFlair" 
    %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
 
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FileGallery" Src="~/MonoX/ModuleGallery/SocialNetworking/FileGallery.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Repositories" TagPrefix="MonoRepositories" %>
<%@ Register Assembly="CustomXml" Namespace="PAB.WebControls" TagPrefix="cc2" %>

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" Runat="Server">

  <cc2:CustomXml
   DocumentUrl="http://gallery.clipflair.net/collection/video.cxml"
   XslUrl="http://gallery.clipflair.net/collection/video_list.xsl"
   runat="server"
   />			 

</asp:Content>

