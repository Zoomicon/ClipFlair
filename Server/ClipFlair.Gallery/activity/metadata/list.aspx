<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="ClipFlair.Gallery.ActivityMetadataPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!--
Project: ClipFlair (http://ClipFlair.codeplex.com)
Filename: list.aspx
Version: 20130711
-->

<html xmlns="http://www.w3.org/1999/xhtml">

  <head runat="server">
    <title>ClipFlair Gallery - Activity Metadata</title>

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
    <asp:XmlDataSource ID="xmlItems" runat="server" DataFile="~/activity/all_files.xml" XPath="items/item" />
    <asp:XmlDataSource ID="xmlLanguage" runat="server" DataFile="~/activity/metadata/Language.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="XmlLanguageCombination" runat="server" DataFile="~/activity/metadata/LanguageCombination.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="XmlLevel" runat="server" DataFile="~/activity/metadata/Level.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlFromSkills" runat="server" DataFile="~/activity/metadata/Skills.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlToSkills" runat="server" DataFile="~/activity/metadata/Skills.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlAVSkills" runat="server" DataFile="~/activity/metadata/AVSkills.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlResponses" runat="server" DataFile="~/activity/metadata/Responses.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlTasksRevoicing" runat="server" DataFile="~/activity/metadata/TasksRevoicing.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlTasksCaptioning" runat="server" DataFile="~/activity/metadata/TasksCaptioning.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlLearnerType" runat="server" DataFile="~/activity/metadata/LearnerType.xml" XPath="Facet/String" />
    <asp:XmlDataSource ID="xmlAgeGroup" runat="server" DataFile="~/activity/metadata/AgeGroup.xml" XPath="Facet/String" />
    
    <form id="form1" runat="server">
     
      <div class="instructions">
      Please fill in the following information for the activity of your choice. Select the activity from the dropdown list.<br />
      Try to fill the metadata as fully and accurately as possible, as they will be used for searching and filtering activities.<br />
      Don't forget to press the SAVE METADATA button. Thank you!
      </div>

      <div class="bar">
        <div class="label">Activity file</div> 
        <asp:DropDownList ID="listItems" runat="server" AutoPostBack="True" 
          DataSourceID="xmlItems" DataTextField="filename" DataValueField="filename" 
          OnSelectedIndexChanged="listItems_SelectedIndexChanged"
          />
      </div>

      <div>
        <div class="label">Title</div>
        <asp:TextBox ID="txtTitle" runat="server" Columns="150"></asp:TextBox>
      </div>

      <div>
        <div class="label">Url</div>
        <asp:TextBox ID="txtUrl" runat="server" Columns="150" ReadOnly="True"></asp:TextBox>
      </div>

      <div>
        <div class="label">Description</div>
        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="100">
        What:
        ...

        Why:
        ...

        How:
        ...
        </asp:TextBox>
      </div>

      <hr />

      <div>
        <div class="label">For learners of</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistForLearners" runat="server" 
            DataSourceID="xmlLanguage" DataTextField="Value" DataValueField="Value"
            />
         </asp:Panel>
      </div>

      <hr />

      <div>
        <div class="label">For speakers of</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistForSpeakers" runat="server" 
            DataSourceID="xmlLanguage" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>

      <hr />

      <div>
        <div class="label">Language combination</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistLanguageCombination" runat="server" 
            DataSourceID="xmlLanguageCombination" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>

      <hr />

      <div>
        <div class="label">Level</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistLevel" runat="server" 
            DataSourceID="xmlLevel" DataTextField="Value" DataValueField="Value"
            RepeatLayout="Table" RepeatColumns="3"
            />
         </asp:Panel>
      </div>
      
      <hr />
      
      <div>
        <div class="label">Keywords (comma-separated)</div>
        <asp:TextBox ID="txtKeywords" runat="server" Columns="150"></asp:TextBox>
      </div>
      
      <hr />

      <div>
        <div class="label">Estimated time (minutes)</div>
        <asp:TextBox ID="txtEstimatedTime" runat="server"></asp:TextBox>
      </div> <!-- TODO: add validator for integer -->

      <hr />

      <div>
        <div class="label">Authors (comma-separated)</div>
        <asp:TextBox ID="txtAuthors" runat="server" Columns="150"></asp:TextBox>
      </div>

      <hr />
      

      <div>
        <div class="label">License</div>
        <asp:TextBox ID="txtLicense" runat="server" Columns="150" Text="CC BY-SA 3.0"></asp:TextBox>
      </div>

      <hr />

      <div>
        <div class="label">From skills</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistFromSkills" runat="server" 
            DataSourceID="xmlFromSkills" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>

      <hr />

      <div>
        <div class="label">To skills</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistToSkills" runat="server" 
            DataSourceID="xmlToSkills" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>

      <hr />

      <div>
        <div class="label">AV skills</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistAVSkills" runat="server" 
            DataSourceID="xmlAVSkills" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>

      <hr />
      
      <div>
        <div class="label">Responses</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistResponses" runat="server" 
            DataSourceID="xmlResponses" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>

      <hr />
      
      <div>
        <div class="label">Tasks - Revoicing</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistTasksRevoicing" runat="server" 
            DataSourceID="xmlTasksRevoicing" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>
      
      <hr />
      
      <div>
        <div class="label">Tasks - Captioning</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistTasksCaptioning" runat="server" 
            DataSourceID="xmlTasksCaptioning" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>
      
      <hr />
      
      <div>
        <div class="label">Learner type</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistLearnerType" runat="server" 
            DataSourceID="xmlLearnerType" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>
      
      <hr />
      
      <div>
        <div class="label">Age group</div>
        <asp:Panel runat="server" 
          Height="100" Width="250"
          ScrollBars="Vertical">

          <asp:CheckBoxList ID="clistAgeGroup" runat="server" 
            DataSourceID="xmlAgeGroup" DataTextField="Value" DataValueField="Value" />
         </asp:Panel>
      </div>
      
      <hr />
      
      <div>
        <div class="label">Feedback mode to learner</div>
        <asp:TextBox ID="txtFeedbackModeToLearner" runat="server" Columns="150"></asp:TextBox>
      </div>
      
      <hr /> 
           
      <div>
        <asp:Button ID="btnSave" runat="server"
          Text="Save metadata"
          Font-Bold="true"
          height="50"
          OnClick="btnSave_Click"/>
      </div>
    
    </form>

  </body>

</html>
