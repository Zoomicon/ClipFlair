<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Default.aspx.cs"
    Inherits="MonoSoftware.MonoX.Mobile.Default"
    MasterPageFile="~/MonoX/MasterPages/Mobile/Default.master"
    Theme="Mobile"
    HashListeningEnabled="true"
%>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>  

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
      <div class="home-logo">
          <img runat="server" src="~/App_Themes/Mobile/img/logo.png" alt="Logo" width="180" /><br /><br />
          A demonstration of touch-optimized features of MonoX content management system and social networking framework for mobile devices.
      </div>
      <ul data-role="listview" data-inset="true" data-theme="c" data-dividertheme="b"> 
		<li data-role="list-divider">Overview</li> 
		<li><a href="~/Mobile/Content/About/" runat="server">About MonoX Mobile</a></li> 
		<li>
		    Features
		    <ul>
		        <li><a href="~/Mobile/Content/Technical-Features/" runat="server">Technical features</a></li>
		        <li><a href="~/Mobile/Content/Content-Management-Features/" runat="server">Content management features</a></li>
		        <li><a href="~/Mobile/Content/Social-Networking-Features/" runat="server">Social networking features</a></li>
		        <li><a href="~/Mobile/Content/Other-Web-Parts/" runat="server">Other Web parts</a></li>
		        <li><a href="~/Mobile/Content/Performance/" runat="server">Performance</a></li>
		        <li><a href="~/Mobile/Content/Interoperability/" runat="server">Interoperability</a></li>
		        <li><a href="~/Mobile/Content/Licensing-And-Support/" runat="server">Licensing and support</a></li>
		    </ul>
		</li>
		<li><a href="~/Mobile/Content/Supported-Mobile-Platforms/" runat="server">Supported mobile platforms</a></li> 
	  </ul> 
      <ul data-role="listview" data-inset="true" data-theme="c" data-dividertheme="b"> 
		<li data-role="list-divider">Sample Mobile Pages</li> 
	    <li><a href="~/MonoX/Mobile/blog/posts/MonoX/" runat="server">Blog</a></li>
	    <li><a href="~/MonoX/Mobile/Friends.aspx" runat="server">Friends</a></li> 
	    <li><a href="~/MonoX/Mobile/Wall.aspx" runat="server">Wall</a></li> 
      </ul>
</asp:Content>