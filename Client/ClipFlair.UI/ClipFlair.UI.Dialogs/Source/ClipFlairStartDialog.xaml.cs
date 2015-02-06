﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: StartDialog.xaml.cs
//Version: 20150206

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WPF_Compatibility;

namespace ClipFlair.UI.Dialogs
{
  public partial class StartDialog : ChildWindowExt
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

    /*
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      //ChildWindow template part names can be found at: http://msdn.microsoft.com/en-us/library/dd833070(v=vs.95).aspx
      FrameworkElement contentRoot = GetTemplateChild("ContentRoot") as FrameworkElement;

      //make the ChildWindow content take up the whole ChildWindow area
      contentRoot.HorizontalAlignment = HorizontalAlignment.Stretch;
      contentRoot.VerticalAlignment = VerticalAlignment.Stretch;
    }
    */

    public static void Show(IClipFlairStartActions actions, 
      #if SILVERLIGHT
      EventHandler<CancelEventArgs> closingHandler
      #else
      CancelEventHandler closingHandler
      #endif
      = null)
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
      btnHelpTutorialActivity.Visibility =
      btnHelpTutorialVideos.Visibility =
      btnHelpManual.Visibility =
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

      btnOpenVideoFile.Visibility = 
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

      btnOpenImageFile.Visibility =
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

      btnHelpTutorialActivity.Visibility =
      btnHelpTutorialVideos.Visibility = 
      btnHelpManual.Visibility =
      btnHelpFAQ.Visibility = 
      btnHelpContact.Visibility = Visibility.Visible;
    }

    private void btnHelpTutorialActivity_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.HelpTutorialActivity())
        Close();
    }
    
    private void btnHelpTutorialVideos_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.HelpTutorialVideos())
        Nop(); //Close();
    }

    private void btnHelpManual_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.HelpManual())
        Nop(); //Close();
    }
    
    private void btnHelpFAQ_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.HelpFAQ())
        Nop(); //Close();
    }

    private void btnHelpContact_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.HelpContact())
        Nop(); //Close();    
    }

    //Social//

    private void btnSocial_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Hide2ndLevelButtons();

      if (Actions != null && Actions.Social())
        Nop(); //Close();
    }

    #endregion

  }

}
