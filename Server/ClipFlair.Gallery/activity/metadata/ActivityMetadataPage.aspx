﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityMetadataPage.aspx.cs" Inherits="ClipFlair.Gallery.ActivityMetadataPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!--
Project: ClipFlair (http://ClipFlair.codeplex.com)
Filename: ActivityMetadataPage.aspx
Version: 20160616
-->

<html xmlns="http://www.w3.org/1999/xhtml">

  <head runat="server">
    <title>ClipFlair Gallery - Activity Metadata</title>
    
    <link href="../../css/metadata.css" rel="stylesheet" type="text/css" />
  </head>

  <body>
        
    <%-- DATA SOURCES --%>
    <asp:XmlDataSource ID="xmlLanguage" runat="server" DataFile="~/metadata/Language.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="XmlLanguageCombination" runat="server" DataFile="~/metadata/LanguageCombination.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="XmlLevel" runat="server" DataFile="~/metadata/Level.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlFromSkills" runat="server" DataFile="~/metadata/Skills.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlToSkills" runat="server" DataFile="~/metadata/Skills.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlAVSkills" runat="server" DataFile="~/metadata/AVSkills.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlResponses" runat="server" DataFile="~/metadata/Responses.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlTasksRevoicing" runat="server" DataFile="~/metadata/TasksRevoicing.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlTasksCaptioning" runat="server" DataFile="~/metadata/TasksCaptioning.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlLearnerType" runat="server" DataFile="~/metadata/LearnerType.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlAgeGroup" runat="server" DataFile="~/metadata/AgeGroup.xml" XPath="Facet/String" />
    
    <%-- NAVIGATION MENU --%>

    <div class="navigation">
       <a class="selected" href="../../activity/metadata/">Activity Metadata</a>
       &nbsp;&nbsp;-&nbsp;&nbsp;
       <a href="../../video/metadata/">Video Metadata</a>
       &nbsp;&nbsp;-&nbsp;&nbsp;
       <a href="../../image/metadata/">Image Metadata</a>
    </div>

    <%-- INSTRUCTION BOX --%>

    <div class="instructions">
      Please fill in the following information for the activity of your choice. Select the activity from the dropdown list.<br />
      Try to fill the metadata as fully and accurately as possible, as they will be used for searching and filtering activities.<br />
      Don't forget to press the SAVE METADATA button. Thank you!
    </div>

    <form id="form1" runat="server">

      <%-- LOGIN STATUS --%>

      <asp:LoginName ID="loginName" runat="server" FormatString="Welcome {0}!" /> [<asp:LoginStatus ID="loginStatus" runat="server"/>]

      <%-- INFO BOX --%>

      <div class="bar">

        <div class="label">Activity file</div> 
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
          <div class="label">Description (What, How, Why)</div>
          <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="110"/>
        </div>
     
        <div style="float: left">
          <div class="label">For learners of</div>
          <asp:Panel runat="server" 
            Height="450" Width="250"
            ScrollBars="Auto">

            <asp:CheckBoxList ID="clistForLearners" runat="server" 
              DataSourceID="xmlLanguage" DataTextField="Value" DataValueField="Value"
              />
           </asp:Panel>
        </div>

        <div style="float: left">
          <div class="label">For speakers of</div>
          <asp:Panel runat="server" 
            Height="450" Width="250"
            ScrollBars="Auto">

            <asp:CheckBoxList ID="clistForSpeakers" runat="server" 
              DataSourceID="xmlLanguage" DataTextField="Value" DataValueField="Value" />
           </asp:Panel>
        </div>

        <div style="float: left">
          <div class="label">Language combination</div>
          <asp:Panel runat="server" 
            Height="100" Width="250"
            ScrollBars="Auto">

            <asp:CheckBoxList ID="clistLanguageCombination" runat="server" 
              DataSourceID="xmlLanguageCombination" DataTextField="Value" DataValueField="Value" />
           </asp:Panel>
        </div>

        <div style="float: left">
          <div class="label">Level</div>
          <asp:Panel runat="server" 
            Height="50" Width="250"
            ScrollBars="Auto">

            <asp:CheckBoxList ID="clistLevel" runat="server" 
              DataSourceID="xmlLevel" DataTextField="Value" DataValueField="Value"
              RepeatLayout="Table" RepeatColumns="3"
              />
           </asp:Panel>
        </div>
     
        <div style="clear: left">
          <div class="label">Estimated time (minutes)</div>
          <asp:TextBox ID="txtEstimatedTime" runat="server"></asp:TextBox>
        </div> <!-- TODO: add validator for integer -->
   
        <div style="float: left">
          <div class="label">From skills</div>
          <asp:Panel runat="server" 
            Height="100" Width="250"
            ScrollBars="Auto">

            <asp:CheckBoxList ID="clistFromSkills" runat="server" 
              DataSourceID="xmlFromSkills" DataTextField="Value" DataValueField="Value" />
           </asp:Panel>
        </div>

        <div style="float: left">
          <div class="label">To skills</div>
          <asp:Panel runat="server" 
            Height="100" Width="250"
            ScrollBars="Auto">

            <asp:CheckBoxList ID="clistToSkills" runat="server" 
              DataSourceID="xmlToSkills" DataTextField="Value" DataValueField="Value" />
           </asp:Panel>
        </div>

        <div style="float: left">
          <div class="label">AV skills</div>
          <asp:Panel runat="server" 
            Height="150" Width="250"
            ScrollBars="Auto">

            <asp:CheckBoxList ID="clistAVSkills" runat="server" 
              DataSourceID="xmlAVSkills" DataTextField="Value" DataValueField="Value" />
           </asp:Panel>
        </div>

        <div style="clear:left; height:0"></div>

        <div style="float: left">
          <div class="label">Responses</div>
          <asp:Panel runat="server" 
            Height="100" Width="250"
            ScrollBars="Auto">

            <asp:CheckBoxList ID="clistResponses" runat="server" 
              DataSourceID="xmlResponses" DataTextField="Value" DataValueField="Value" />
           </asp:Panel>
        </div>
    
        <div style="float: left">
          <div class="label">Tasks - Revoicing</div>
          <asp:Panel runat="server" 
            Height="130" Width="250"
            ScrollBars="Auto">

            <asp:CheckBoxList ID="clistTasksRevoicing" runat="server" 
              DataSourceID="xmlTasksRevoicing" DataTextField="Value" DataValueField="Value" />
           </asp:Panel>
        </div>
     
        <div style="float: left">
          <div class="label">Tasks - Captioning</div>
          <asp:Panel runat="server" 
            Height="100" Width="250"
            ScrollBars="Auto">

            <asp:CheckBoxList ID="clistTasksCaptioning" runat="server" 
              DataSourceID="xmlTasksCaptioning" DataTextField="Value" DataValueField="Value" />
           </asp:Panel>
        </div>

        <div style="clear: left; height: 0" />
     
        <div style="float: left">
          <div class="label">Learner type</div>
          <asp:Panel runat="server" 
            Height="100" Width="250"
            ScrollBars="Auto">

            <asp:CheckBoxList ID="clistLearnerType" runat="server" 
              DataSourceID="xmlLearnerType" DataTextField="Value" DataValueField="Value" />
           </asp:Panel>
        </div>

        <div style="clear: left">
          <div class="label">Feedback mode to learner</div>
          <asp:TextBox ID="txtFeedbackModeToLearner" runat="server" Columns="150"></asp:TextBox>
        </div>

        <div>
          <div class="label">Age group</div>
          <asp:Panel runat="server" 
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
