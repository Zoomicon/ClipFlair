﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IActivityMetadata.cs
//Version: 20130725

using System;
using System.Xml.Linq;

namespace ClipFlair.Gallery
{

  public interface IActivityMetadata : IMetadata
  {

    #region --- Properties ---

    string[] ForLearners { get; set; }
    string[] ForSpeakers { get; set; }
    string[] LanguageCombination { get; set; }
    string[] Level { get; set; }
    string EstimatedTimeMinutes { get; set; }
    string[] Authors { get; set; }
    string[] FromSkills { get; set; }
    string[] ToSkills { get; set; }
    string[] AVSkills { get; set; }
    string[] Responses { get; set; }
    string[] TasksRevoicing { get; set; }
    string[] TasksCaptioning { get; set; }
    string[] LearnerType { get; set; }
    string[] AgeGroup { get; set; }
    string[] FeedbackModeToLearner { get; set; }

    #endregion
  
  }

}