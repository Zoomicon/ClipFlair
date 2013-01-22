//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionElementExt.cs
//Version: 20130122

using ClipFlair.Utils.Extensions;

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.IO;
using System.Windows.Browser;

namespace ClipFlair.CaptionsGrid
{

  /// <summary>
  /// Represents an extended closed caption
  /// </summary>
  public class CaptionElementExt : CaptionElement
  {

    public const string PROPERTY_DURATION = "Duration";
    public const string PROPERTY_ACTOR = "Role";
    public const string PROPERTY_WPM = "WPM";
    public const string PROPERTY_AUDIO = "Audio";
    public const string PROPERTY_COMMENTS = "Comments";

    private string _role;
    private Stream _audio;
    private string _comments;

    public CaptionElementExt()
    {
      PropertyChanged += (s, e) => { if (e.PropertyName == PROPERTY_DURATION) NotifyPropertyChanged(PROPERTY_WPM); }; //if duration changed also notify that WPM changed
    }

    /// <summary>
    /// Gets or sets the duration of this marker (calculated from start time to end time).
    /// </summary>
    /// <remarks>
    /// The property value is calculated (when set it updates end time) and is provided as a convenience.
    /// </remarks>
    [ScriptableMember]
    public new TimeSpan Duration //hides ancestor's "Duration" property (if it was "virtual" we'd "override" it) to add a "set" accessor method
    {
      get { return base.Duration; }
      set { End = Begin + value; } //setting End will fire PropertyChanged event for "End" and "Duration" properties
    }

    /// <summary>
    /// Gets or sets the role for this marker item.
    /// </summary>
    [ScriptableMember]
    public string Role
    {
      get { return _role; }
      set
      {
        _role = value;
        NotifyPropertyChanged(PROPERTY_ACTOR);
      }
    }

    /// <summary>
    /// Gets or sets the text associated with this marker.
    /// </summary>
    [ScriptableMember]
    public override object Content
    {
      get { return base.Content; }
      set
      {
        if (Content != value)
        {
          base.Content = value;
          NotifyPropertyChanged(PROPERTY_WPM); //if content changed also notify that WPM changed
        }
      }
    }

    /// <summary>
    /// Gets or sets the word count for this marker item.
    /// </summary>
    [ScriptableMember]
    public int WordCount
    {
      get
      {
        if (Content == null) return 0;
        return ((string)Content).WordCount();
      }
    }

    /// <summary>
    /// Gets or sets the words-per-minute (WPM) number for this marker item.
    /// </summary>
    [ScriptableMember]
    public double WPM
    {
      get
      {
        double minutes = Duration.TotalMinutes;
        return (minutes != 0)? WordCount / minutes : 0;
      }
    }

    /// <summary>
    /// Gets or sets the audio for this marker item.
    /// </summary>
    [ScriptableMember]
    public Stream Audio
    {
      get { return _audio; }
      set
      {
        _audio = value;
        NotifyPropertyChanged(PROPERTY_AUDIO);
      }
    }

    /// <summary>
    /// Gets or sets the comments for this marker item.
    /// </summary>
    [ScriptableMember]
    public string Comments
    {
      get { return _comments; }
      set
      {
        _comments = value;
        NotifyPropertyChanged(PROPERTY_COMMENTS);
      }
    }

    #region static methods

    public static bool HasAudio(CaptionElement caption)
    {
      CaptionElementExt c = caption as CaptionElementExt;
      return (c != null) ? (c.Audio != null) : false;
    }

    #endregion

  }
}
