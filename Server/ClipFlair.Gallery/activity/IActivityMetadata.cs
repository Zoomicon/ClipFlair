//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IActivityMetadata.cs
//Version: 20130718

using System;
using System.Xml.Linq;

namespace ClipFlair.Gallery
{

  public interface IActivityMetadata : IMetadata
  {

    #region --- Properties ---

    string Title { get; set; }
    string Image { get; set; }
    Uri Url { get; set; }
    string Description { get; set; }
    string Filename { get; set; }
    string[] ForLearners { get; set; }
    string[] ForSpeakers { get; set; }
    string[] LanguageCombination { get; set; }
    string[] Level { get; set; }
    string[] Keywords { get; set; }
    string EstimatedTimeMinutes { get; set; }
    string[] Authors { get; set; }
    string License { get; set; }
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