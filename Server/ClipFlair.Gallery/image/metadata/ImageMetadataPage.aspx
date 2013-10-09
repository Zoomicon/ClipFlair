<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageMetadataPage.aspx.cs" Inherits="ClipFlair.Gallery.ImageMetadataPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!--
Project: ClipFlair (http://ClipFlair.codeplex.com)
Filename: ImageMetadataPage.aspx
Version: 20131009
-->

<html xmlns="http://www.w3.org/1999/xhtml">

  <head runat="server">
    <title>ClipFlair Gallery - Image Metadata</title>

    <style type="text/css">
    div 
    {
      margin-top: 15px;
      margin-bottom: 5px;
    }

    .instructions
    {
      border-style:dotted;
      background-color:Yellow;
      padding: 5px;
    }

    .bar
    {
      background-color: ButtonFace;
      padding-left: 5px;
      padding-top: 0px;
      padding-bottom: 15px;
    }

    .label  
    {
      font-weight: bold;
    }
    </style>
  </head>

  <body>
    <asp:XmlDataSource ID="xmlLanguage" runat="server" DataFile="~/video/metadata/Language.xml" XPath="Facet/String" />

    <div class="instructions">
    Please fill in the following information for the image of your choice. Select the image from the dropdown list.<br />
    Try to fill the metadata as fully and accurately as possible, as they will be used for searching and filtering images.<br />
    Don't forget to press the SAVE METADATA button. Thank you!
    </div>

    <form id="form1" runat="server">

      <div class="bar">
        <div class="label">Image file</div> 
        <asp:DropDownList ID="listItems" runat="server" AutoPostBack="True" 
          DataTextField="Filename" DataValueField="Filename" 
          OnSelectedIndexChanged="listItems_SelectedIndexChanged"
          />

       <br />

       <div class="label">Url</div>
        <asp:HyperLink ID="linkUrl" runat="server" Target="_blank"/>
      </div>

      <div>
        <div class="label">Title</div>
        <asp:TextBox ID="txtTitle" runat="server" Columns="150"></asp:TextBox>
      </div>

      <div>
        <div class="label">Description</div>
        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="110" />
      </div>

      <div style="float: left">
        <div class="label">Captions language (on image)</div>
        <asp:Panel runat="server" 
          Height="450" Width="250"
          ScrollBars="Auto">

          <asp:CheckBoxList ID="clistCaptionsLanguage" runat="server" 
            DataSourceID="xmlLanguage" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>

      <div style="clear: left">
        <div class="label">Author / Source</div>
        <asp:TextBox ID="txtAuthorSource" runat="server" Columns="150"></asp:TextBox>
      </div>

      <div>
        <div class="label">Keywords (comma-separated)</div>
        <asp:TextBox ID="txtKeywords" runat="server" Columns="150"></asp:TextBox>
      </div>
      
      <div>
        <div class="label">License</div>
        <asp:TextBox ID="txtLicense" runat="server" Columns="150"></asp:TextBox>
      </div>

      <div>
        <asp:Button ID="btnSave" runat="server"
          Text="Save metadata"
          Font-Bold="true" 
          height="50"
          onclick="btnSave_Click"/>
      </div>
    
    </form>

  </body>

</html>
