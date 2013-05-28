//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionElementExt.cs
//Version: 20130528

using Utils.Extensions;

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
    public const string PROPERTY_ROLESEPARATOR = "RoleSeparator";
    public const string PROPERTY_ROLE = "Role";
    public const string PROPERTY_CAPTION = "Caption";
    public const string PROPERTY_WPM = "WPM";
    public const string PROPERTY_AUDIO = "Audio";
    public const string PROPERTY_COMMENTS = "Comments";

    public const string DEFAULT_ROLE_SEPARATOR = ": "; //note: has a space at the end

    private string _roleSeparator = DEFAULT_ROLE_SEPARATOR;
    private string _role;
    private string _caption;
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
          NotifyPropertyChanged(PROPERTY_CAPTION);
          NotifyPropertyChanged(PROPERTY_WPM); //also notify that WPM changed
        }
      }
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
        _caption = newCaption;
        NotifyPropertyChanged(PROPERTY_CAPTION);
        NotifyPropertyChanged(PROPERTY_WPM); //also notify that WPM changed
      }

    }

    /// <summary>
    /// Gets or sets the word count for this marker item.
    /// </summary>
    [ScriptableMember]
    public int WordCount
    {
      get
      { return (_caption != null)? _caption.WordCount() : 0; }
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

  }
}
