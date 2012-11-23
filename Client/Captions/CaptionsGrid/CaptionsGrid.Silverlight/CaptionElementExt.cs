//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionElementExt.cs
//Version: 20121123

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

    const string PROPERTY_ACTOR = "Actor";
    const string PROPERTY_AUDIO = "Audio";

    private string _actor;
    private Stream _audio;

    /// <summary>
    /// Gets or sets the actor for this marker item.
    /// </summary>
    [ScriptableMember]
    public string Actor
    {
      get { return _actor; }
      set
      {
        _actor = value;
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

    #region static methods

    public static bool HasAudio(CaptionElement caption)
    {
      CaptionElementExt c = caption as CaptionElementExt;
      return (c != null) ? (c.Audio != null) : false;
    }

    #endregion

  }
}
