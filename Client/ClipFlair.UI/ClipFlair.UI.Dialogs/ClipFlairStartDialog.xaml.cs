//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: StartDialog.xaml.cs
//Version: 20140415

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ClipFlair.UI.Dialogs
{
  public partial class StartDialog : ChildWindow
  {
    public StartDialog()
    {
      InitializeComponent();
      Hide2ndLevelButtons();
    }

    #region --- Properties ---

    public IClipFlairStartActions Actions { get; set; }

    #endregion
    
    #region --- Methods ---

    public static void Show(IClipFlairStartActions actions, EventHandler<CancelEventArgs> closingHandler = null)
    {
      StartDialog prompt = new StartDialog();
      prompt.Title = "";
      prompt.Actions = actions;
      if (closingHandler != null) 
        prompt.Closing += closingHandler;
      prompt.Show();
    }

    private static Visibility ToggleVisibility(Visibility visibility)
    {
      return (visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
    }

    private void Hide2ndLevelButtons()
    {
      btnOpenActivityFile.Visibility =
      btnOpenActivityURL.Visibility =
      btnOpenActivityGallery.Visibility =
      btnOpenVideoFile.Visibility =
      btnOpenVideoURL.Visibility =
      btnOpenVideoGallery.Visibility =
      btnOpenImageFile.Visibility =
      btnOpenImageURL.Visibility =
      btnOpenImageGallery.Visibility =
      btnHelpTutorials.Visibility =
      btnHelpFAQ.Visibility =
      btnHelpContact.Visibility = Visibility.Collapsed;
    }

    private void Nop()
    {
      //do nothing
    }
    
    #endregion

    #region --- Events ---

    //Project Home//

    private void btnProjectHome_Click(object sender, RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null)
        Actions.ProjectHome();

      //Close();
    }

    //NewActivity//

    private void btnNewActivity_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null)
        Actions.NewActivity();

      Close();
    }

    //OpenActivity//

    private void btnOpenActivity_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      btnOpenActivityFile.Visibility = 
      btnOpenActivityURL.Visibility = 
      btnOpenActivityGallery.Visibility = Visibility.Visible;
    }

    private void btnOpenActivityFile_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.OpenActivityFile())
        Close();
    }

    private void btnOpenActivityURL_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.OpenActivityURL())
        Close();
    }

    private void btnOpenActivityGallery_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.OpenActivityGallery())
        Close();
    }

    //OpenVideo//

    private void btnOpenVideo_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      //btnOpenVideoFile.Visibility = 
      btnOpenVideoURL.Visibility = 
      btnOpenVideoGallery.Visibility = Visibility.Visible;
    }

    private void btnOpenVideoFile_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.OpenVideoFile())
        Close();
    }

    private void btnOpenVideoURL_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.OpenVideoURL())
        Close();
    }

    private void btnOpenVideoGallery_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.OpenVideoGallery())
        Close();
    }

    //OpenImage//
    
    private void btnOpenImage_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      //btnOpenImageFile.Visibility =
      btnOpenImageURL.Visibility = 
      btnOpenImageGallery.Visibility = Visibility.Visible;
    }

    private void btnOpenImageFile_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.OpenImageFile())
        Close();
    }

    private void btnOpenImageURL_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.OpenImageURL())
        Close();
    }

    private void btnOpenImageGallery_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();
      
      if (Actions != null && Actions.OpenImageGallery())
        Close();
    }

    //Help//

    private void btnHelp_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      btnHelpTutorials.Visibility = 
      btnHelpFAQ.Visibility = 
      btnHelpContact.Visibility = Visibility.Visible;
    }
    
    private void btnHelpTutorials_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      //Hide2ndLevelButtons();

      if (Actions != null && Actions.HelpTutorials())
        Nop(); //Close();
    }

    private void btnHelpFAQ_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      //Hide2ndLevelButtons();

      if (Actions != null && Actions.HelpFAQ())
        Nop(); //Close();
    }

    private void btnHelpContact_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      //Hide2ndLevelButtons();

      if (Actions != null && Actions.HelpContact())
        Nop(); //Close();    
    }

    //Social//

    private void btnSocial_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      //Hide2ndLevelButtons();

      if (Actions != null && Actions.Social())
        Nop(); //Close();
    }

    #endregion

  }

}
