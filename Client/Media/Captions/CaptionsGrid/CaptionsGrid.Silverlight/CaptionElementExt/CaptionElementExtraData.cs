//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionElementExtraData.cs
//Version: 20140706

using System.ComponentModel;
using System.Runtime.Serialization;

namespace ClipFlair.CaptionsGrid
{

  /// <summary>
  /// Extra serializable fields contained by CaptionElementExt (that are not possible to store in SRT captions file)
  /// </summary>
  [DataContract(Namespace = "http://clipflair.net/Contracts/View")]
  public class CaptionElementExtraData : ICaptionElementExtraData
  {

    #region --- Constants ---

    public const string DEFAULT_COMMENTS = null;
    public const bool DEFAULT_RTL = false;

    #endregion

    #region --- Initialization ---

    public CaptionElementExtraData() //since we have a custom constructor, C# doesn't add a public default constructor
    {
      //NOP
    }

    public CaptionElementExtraData(ICaptionElementExtraData captionExtraData) //initialize from CaptionElementExt or CaptionElementExtraData instance (or any instance of class implementing ICaptionElementExtraData)
    {
      Comments = captionExtraData.Comments;
      RTL = captionExtraData.RTL;
    }

    #endregion

    #region --- Properties ---

    [DataMember]
    [DefaultValue(DEFAULT_COMMENTS)]
    public string Comments { get; set; }

    [DataMember]
    [DefaultValue(DEFAULT_RTL)]
    public bool RTL { get; set; }

    #endregion

  }

}
