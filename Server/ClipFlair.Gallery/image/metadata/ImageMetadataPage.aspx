<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageMetadataPage.aspx.cs" Inherits="ClipFlair.Gallery.ImageMetadataPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!--
Project: ClipFlair (http://ClipFlair.codeplex.com)
Filename: ImageMetadataPage.aspx
Version: 20140717
-->

<html xmlns="http://www.w3.org/1999/xhtml">

  <head runat="server">
    <title>ClipFlair Gallery - Image Metadata</title>

    <link href="../../css/metadata.css" rel="stylesheet" type="text/css" />
  </head>

  <body>
    <asp:XmlDataSource ID="xmlLanguage" runat="server" DataFile="~/video/metadata/Language.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlAgeGroup" runat="server" DataFile="~/activity/metadata/AgeGroup.xml" XPath="Facet/String" />

    <%-- NAVIGATION MENU --%>
    <div class="navigation">
       <a href="../../activity/metadata/">Activity Metadata</a>
       &nbsp;&nbsp;-&nbsp;&nbsp;
       <a href="../../video/metadata/">Video Metadata</a>
       &nbsp;&nbsp;-&nbsp;&nbsp;
       <a class="selected" href="../../image/metadata/">Image Metadata</a>
    </div>

    <%-- INSTRUCTIONS BOX --%>
    <div class="instructions">
    Please fill in the following information for the image of your choice. Select the image from the dropdown list.<br />
    Try to fill the metadata as fully and accurately as possible, as they will be used for searching and filtering images.<br />
    Don't forget to press the SAVE METADATA button. Thank you!
    </div>

    <form id="form1" runat="server">

      <%-- INFO BOX --%>
      <div class="bar">

        <div class="label">Image file</div> 
        <asp:DropDownList ID="listItems" runat="server" AutoPostBack="True" 
          DataTextField="Filename" DataValueField="Filename" 
          OnSelectedIndexChanged="listItems_SelectedIndexChanged"
          />

        <br />

        <div class="label">Url</div>
        <asp:HyperLink ID="linkUrl" runat="server" Target="_blank"/>

        <div>
          <span class="label">First published: </span>
          <asp:Label ID="lblFirstPublished" runat="server" />
          <span class="label"> - Last updated: </span>
          <asp:Label ID="lblLastUpdated" runat="server" />
        </div>

      </div>

      <%-- METADATA INPUT UI --%>
      <asp:Panel ID="uiMetadata" runat="server" Visible="false">

        <div>
          <div class="label">Title</div>
          <asp:TextBox ID="txtTitle" runat="server" Columns="150"></asp:TextBox>
        </div>

        <div>
          <div class="label">Description</div>
          <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="110" />
        </div>

        <div>
          <div class="label">Captions language (on image)</div>
          <asp:Panel runat="server" 
            Height="450" Width="250"
            ScrollBars="Auto">

            <asp:CheckBoxList ID="clistCaptionsLanguage" runat="server" 
              DataSourceID="xmlLanguage" DataTextField="Value" DataValueField="Value" />
           </asp:Panel>
        </div>

        <div>
          <div class="label">Age group</div>
          <asp:Panel ID="Panel1" runat="server" 
            Height="130" Width="250"
            ScrollBars="Auto">

            <asp:CheckBoxList ID="clistAgeGroup" runat="server" 
              DataSourceID="xmlAgeGroup" DataTextField="Value" DataValueField="Value" />
           </asp:Panel>
        </div>     
     
        <div>
          <div class="label">Keywords (comma-separated)</div>
          <asp:TextBox ID="txtKeywords" runat="server" Columns="150"></asp:TextBox>
        </div>

        <div>
          <div class="label">Authors / Source (comma-separated)</div>
          <asp:TextBox ID="txtAuthorSource" runat="server" Columns="150"></asp:TextBox>
        </div>

        <div>
          <div class="label">License</div>
          <asp:TextBox ID="txtLicense" runat="server" Columns="150" Text="CC BY-SA 3.0"></asp:TextBox>
        </div>

        <%-- SAVE BUTTON --%>   
        <div>
          <asp:Button ID="btnSave" runat="server"
            Text="Save metadata"
            Font-Bold="true"
            height="50"
            OnClick="btnSave_Click"
            />
          &nbsp;
          &nbsp;
          <i>Gallery contents are updated once a day from saved metadata</i>
        </div>

        <%-- EXTRA PADDING AT THE END --%>
        <br />
        <br />

      </asp:Panel>
          
    </form>

  </body>

</html>
