//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IVideoMetadata.cs
//Version: 20130711

using System;

namespace ClipFlair.Gallery
{

  public interface IVideoMetadata
  {
    string Title { get; set; }
    string Image { get; set; }
    Uri Url { get; set; }
    string Description { get; set; }
    string Filename { get; set; }
    string[] AudioLanguage { get; set; }
    string[] CaptionsLanguage { get; set; }
    string[] Genre { get; set; }
    bool AgeRestricted { get; set; }
    string Duration { get; set; }
    string[] AudiovisualRichness { get; set; }
    bool PedagogicalAdaptability { get; set; }
    string AuthorSource { get; set; }
    string License { get; set; }
  }

}