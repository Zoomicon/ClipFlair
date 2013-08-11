<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoMetadataPage.aspx.cs" Inherits="ClipFlair.Gallery.VideoMetadataPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!--
Project: ClipFlair (http://ClipFlair.codeplex.com)
Filename: VideoMetadataPage.aspx
Version: 20130805
-->

<html xmlns="http://www.w3.org/1999/xhtml">

  <head runat="server">
    <title>ClipFlair Gallery - Video Metadata</title>

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
    <asp:XmlDataSource ID="xmlGenre" runat="server" DataFile="~/video/metadata/Genre.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="XmlAudiovisualRichness" runat="server" DataFile="~/video/metadata/AudiovisualRichness.xml" XPath="Facet/String" />

    <form id="form1" runat="server">

      <div class="instructions">
      Please fill in the following information for the clip of your choice. Select the clip from the dropdown list.<br />
      Try to fill the metadata as fully and accurately as possible, as they will be used for searching and filtering clips.<br />
      Don't forget to press the SAVE METADATA button. Thank you!
      </div>

      <div class="bar">
        <div class="label">Video stream</div> 
        <asp:DropDownList ID="listItems" runat="server" AutoPostBack="True" 
          DataTextField="Foldername" DataValueField="Foldername" 
          OnSelectedIndexChanged="listItems_SelectedIndexChanged"
          />

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

      <div>
        <div class="label">Audio language</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistAudioLanguage" runat="server" 
            DataSourceID="xmlLanguage" DataTextField="Value" DataValueField="Value" 
            />
         </asp:Panel>
      </div>

      <div>
        <div class="label">Captions language (on the clip)</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistCaptionsLanguage" runat="server" 
            DataSourceID="xmlLanguage" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>

      <div>
        <div class="label">Genre</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistGenre" runat="server" 
            DataSourceID="xmlGenre" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>

      <div class="label">
        <asp:CheckBox ID="cbAgeRestricted" runat="server" Text="Age Restricted (18+)" />
      </div>
      
      <div>
        <div class="label">Duration (hh:mm:ss)</div>
        <asp:TextBox ID="txtDuration" runat="server"></asp:TextBox>
      </div>

      <div>
        <div class="label">Audiovisual richness</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistAudiovisualRichness" runat="server" 
            DataSourceID="xmlAudiovisualRichness" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>

      <div class="label">
        <asp:CheckBox ID="cbPedagogicalAdaptability" runat="server" Text="Pedagogical Adaptability" />
      </div>

      <div>
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
