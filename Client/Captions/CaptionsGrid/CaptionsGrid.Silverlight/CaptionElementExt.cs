//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionElementExt.cs
//Version: 20121124

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System.IO;
using System.Windows.Browser;

namespace ClipFlair.CaptionsGrid
{

  /// <summary>
  /// Represents an extended closed caption
  /// </summary>
  public class CaptionElementExt : CaptionElement
  {

    const string PROPERTY_ACTOR = "Role";
    const string PROPERTY_AUDIO = "Audio";
    const string PROPERTY_COMMENTS = "Comments";

    private string _role;
    private Stream _audio;
    private string _comments;

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
