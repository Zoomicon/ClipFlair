//Prroject: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IClipFlairStartActions.cs
//Version: 20140623

namespace ClipFlair.UI.Dialogs
{
  public interface IClipFlairStartActions
  {
    //Project Home//
    bool ProjectHome();

    //NewActivity//
    bool NewActivity();

    //OpenActivity//
     bool OpenActivityFile();
     bool OpenActivityURL();
     bool OpenActivityGallery();

    //OpenVideo//
     bool OpenVideoFile();
     bool OpenVideoURL();
     bool OpenVideoGallery();

    //OpenImage//
     bool OpenImageFile();
     bool OpenImageURL();
     bool OpenImageGallery();

    //Help//
     bool HelpTutorialActivity();
     bool HelpTutorialVideos();
     bool HelpManual();
     bool HelpFAQ();
     bool HelpContact();

    //Social//
     bool Social();
 
  }
}
