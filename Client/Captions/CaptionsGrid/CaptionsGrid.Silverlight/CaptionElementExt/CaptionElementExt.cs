//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionElementExt.cs
//Version: 20140724

//TODO: listen PROPERTY_CONTENT property change event and somehow notify players to render the caption again if it's currently visible

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System;
using System.IO;
using System.Windows.Browser;
using Utils.Extensions;

namespace ClipFlair.CaptionsGrid
{

  /// <summary>
  /// Represents an extended closed caption
  /// </summary>
  public class CaptionElementExt : CaptionElement, ICaptionElementExtraData
  {

    #region --- Constants ---

    public const string PROPERTY_BEGIN = "Begin";
    public const string PROPERTY_END = "End";
    public const string PROPERTY_DURATION = "Duration";
    public const string PROPERTY_CONTENT = "Content";
    public const string PROPERTY_ROLESEPARATOR = "RoleSeparator";
    public const string PROPERTY_ROLE = "Role";
    public const string PROPERTY_CAPTION = "Caption";
    public const string PROPERTY_RTL = "RTL";
    public const string PROPERTY_CPL = "CPL";
    public const string PROPERTY_CPS = "CPS";
    public const string PROPERTY_WPM = "WPM";
    public const string PROPERTY_AUDIO = "Audio";
    public const string PROPERTY_COMMENTS = "Comments";
    public const string PROPERTY_COMMENTS_AUDIO = "CommentsAudio";

    public const string DEFAULT_ROLE_SEPARATOR = ": "; //note: has a space at the end
    private static readonly string[] LineSeparators = new string[] { "\r\n", "\r", "\n"};

    #endregion

    #region --- Fields ---

    private string _roleSeparator = DEFAULT_ROLE_SEPARATOR;
    private string _role; //=null
    private string _caption; //=null
    private Stream _audio; //=null
    private Stream _commentsAudio; //=null
    private CaptionElementExtraData _extraData = new CaptionElementExtraData();

    #endregion

    #region --- Initialization ---

    public CaptionElementExt()
    {
      PropertyChanged += (s, e) =>
      {
        switch (e.PropertyName) {
          case PROPERTY_DURATION:
            HandleDurationChanged(); //if duration changed also notify that CPS and WPM changed
            break;
          case PROPERTY_BEGIN: //fix for MediaMarker ancestor: calling NotifyPositionChanged to remove/add again the CaptionElement at the correct place at MediaMarkerCollection
            NotifyPositionChanged();
            break;
        };
      };
    }

    #endregion

    #region --- Properties ---

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
      set { End = Begin + value; } //setting End will fire PropertyChanged event for both "End" and "Duration" properties (and will also set the ancestor's Duration property appropriately)
    }

    /// <summary>
    /// Gets or sets the role separator used to extract role from caption) for this marker item.
    /// </summary>
    [ScriptableMember]
    public string RoleSeparator
    {
      get { return _roleSeparator; }
      set
      {
        if (_roleSeparator != value)
        {
          _roleSeparator = value;
          NotifyPropertyChanged(PROPERTY_ROLESEPARATOR);
        }
      }
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
        if (_role != value)
        {
          _role = value;
          UpdateContent();
          NotifyPropertyChanged(PROPERTY_ROLE);
        }
      }
    }

    [ScriptableMember]
    protected string RoleWithSeparator
    {
      get
      { 
        string role = Role;
        return (String.IsNullOrWhiteSpace(role))? "" : role + RoleSeparator; //never return null
      }
    }

    /// <summary>
    /// Gets or sets the caption associated with this marker.
    /// </summary>
    [ScriptableMember]
    public string Caption
    {
      get { return _caption; }
      set
      {
        if (_caption != value)
        {
          _caption = value;
          UpdateContent();
          OnCaptionChanged();
        }
      }
    }

    /// <summary>
    /// Gets the caption lines.
    /// </summary>
    [ScriptableMember]
    public string[] CaptionLines
    {
      get { return (_caption != null)? _caption.Split(LineSeparators, StringSplitOptions.None) : new string[]{}; }
      set { Caption = value.Concatenate(); }
    }

    /// <summary>
    /// Gets or sets the content associated with this marker.
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
          UpdateRoleAndCaption();
        }
      }
    }

    /// <summary>
    /// Sets Content property from Role and Caption ones
    /// </summary>
    private void UpdateContent()
    {
      object newContent = RoleWithSeparator + ((_caption != null) ? _caption : ""); //even for null captions we need to set Content to non-null since Role is persisted via the Content property
      if (newContent != Content)
        base.Content = newContent;
    }
    /// <summary>
    /// Sets _role and _caption fields from Content property
    /// </summary>
    private void UpdateRoleAndCaption()
    {
      string content = Content as string;
      int roleSeparatorPos = (content != null) ? content.IndexOf(RoleSeparator) : -1;
      bool foundRoleSeparator = (roleSeparatorPos >= 0);
      
      string newRole = (foundRoleSeparator) ? content.Substring(0, roleSeparatorPos) : null;
      string newCaption = (foundRoleSeparator) ? content.Substring(roleSeparatorPos + RoleSeparator.Length) : content; //if Content doesn't contain a string, then _caption will get a null value

      if (_role != newRole)
      {
        _role = newRole;
        NotifyPropertyChanged(PROPERTY_ROLE);
      }

      if (_caption != newCaption)
      {
        _caption = newCaption; //since we set _caption and not Caption (must do so), we need to raise NotifyPropertyChanged ourselves here
        OnCaptionChanged();
      }

    }

    /// <summary>
    /// Gets the line count for this marker item.
    /// </summary>
    [ScriptableMember]
    public int LineCount
    {
      get { return CaptionLines.Length; }
    }

    /// <summary>
    /// Gets the character count for this marker item.
    /// </summary>
    [ScriptableMember]
    public int CharCount
    {
      get { return (_caption != null) ? _caption.Length : 0; }
    }
    
    /// <summary>
    /// Gets the word count for this marker item.
    /// </summary>
    [ScriptableMember]
    public int WordCount
    {
      get { return (_caption != null)? _caption.WordCount() : 0; }
    }

    /// <summary>
    /// Gets the characters-per-line (CPL) metric for this marker item.
    /// </summary>
    [ScriptableMember]
    public int[] CPL
    {
      get
      {
        string[] lines = CaptionLines;
        int lineCount = lines.Length;
        int[] result = new int[lineCount];
        for (int i = 0; i < lineCount; i++)
          result[i] = lines[i].Trim().Length; //not counting whitespace at start or end of caption

        return result;
      }
    }

    /// <summary>
    /// Gets the characters-per-second (CPS) metric for this marker item.
    /// </summary>
    [ScriptableMember]
    public double CPS
    {
      get
      {
        double seconds = Duration.TotalSeconds;
        return (seconds != 0) ? CharCount / seconds : 0;
      }
    }
    
    /// <summary>
    /// Gets the words-per-minute (WPM) metric for this marker item.
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
        if (_audio != value)
        {
          _audio = value;
          NotifyPropertyChanged(PROPERTY_AUDIO);
        }
      }
    }

    /// <summary>
    /// Gets or sets the comments audio for this marker item.
    /// </summary>
    [ScriptableMember]
    public Stream CommentsAudio
    {
      get { return _commentsAudio; }
      set
      {
        if (_commentsAudio != value)
        {
          _commentsAudio = value;
          NotifyPropertyChanged(PROPERTY_COMMENTS_AUDIO);
        }
      }
    }
    
    #region Extra Data
    
    /// <summary>
    /// Gets or sets the extra data for this marker item.
    /// </summary>
    [ScriptableMember]
    public CaptionElementExtraData ExtraData
    {
      get { return _extraData; }
      set
      {
        if (_extraData != value)
        {
          _extraData = value;
          NotifyPropertyChanged(PROPERTY_COMMENTS);
          NotifyPropertyChanged(PROPERTY_RTL);
        }
      }
    }
    
    /// <summary>
    /// Gets or sets the comments for this marker item.
    /// </summary>
    [ScriptableMember]
    public string Comments
    {
      get { return _extraData.Comments; }
      set
      {
        if (_extraData.Comments != value)
        {
          _extraData.Comments = value;
          NotifyPropertyChanged(PROPERTY_COMMENTS);
        }
      }
    }

    /// <summary>
    /// Gets or sets the RTL (right to left) direction for this marker item.
    /// </summary>
    [ScriptableMember]
    public bool RTL
    {
      get { return _extraData.RTL; }
      set
      {
        if (_extraData.RTL != value)
        {
          _extraData.RTL = value;
          NotifyPropertyChanged(PROPERTY_RTL);
        }
      }
    }

    #endregion

    #endregion

    #region --- Events ---

    protected void HandleDurationChanged()
    {
      //if duration changed also notify that CPS and WPM changed
      NotifyPropertyChanged(PROPERTY_CPS);
      NotifyPropertyChanged(PROPERTY_WPM);
    }

    protected void OnCaptionChanged()
    {
      NotifyPropertyChanged(PROPERTY_CAPTION); //notify that caption changed...
      NotifyPropertyChanged(PROPERTY_CPL); //...also notify that CPL changed
      NotifyPropertyChanged(PROPERTY_CPS); //...also notify that CPS changed
      NotifyPropertyChanged(PROPERTY_WPM); //...also notify that WPM changed
    }

    #endregion

  }
}
