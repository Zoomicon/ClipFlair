//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ICaptionElementExtraData.cs
//Version: 20140707

namespace ClipFlair.CaptionsGrid
{

  /// <summary>
  /// Extra serializable fields contained by CaptionElementExt (that are not possible to store in SRT captions file)
  /// </summary>
  public interface ICaptionElementExtraData
  {
    string Comments { get; set; } //20140706
    bool RTL { get; set; } //20140706

    //bool Intertitle { get; set; }
  }

}
