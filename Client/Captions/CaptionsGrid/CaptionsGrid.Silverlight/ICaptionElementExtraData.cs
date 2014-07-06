//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ICaptionElementExtraData.cs
//Version: 20140706

namespace ClipFlair.CaptionsGrid
{

  /// <summary>
  /// Extra serializable fields contained by CaptionElementExt (that are not possible to store in SRT captions file)
  /// </summary>
  public interface ICaptionElementExtraData
  {
    string Comments { get; set; }

    bool RTL { get; set; }
  }

}
