//Filename: Label.cs
//Version: 20150313

using System.Windows;

namespace WPF_Compatibility
{
  #if SILVERLIGHT
  [TemplateVisualStateAttribute(Name = "Invalid", GroupName = "ValidationStates")]
  [TemplateVisualStateAttribute(Name = "Disabled", GroupName = "CommonStates")]
  [TemplateVisualStateAttribute(Name = "Normal", GroupName = "CommonStates")]
  [TemplateVisualStateAttribute(Name = "Valid", GroupName = "ValidationStates")]
  [TemplateVisualStateAttribute(Name = "NotRequired", GroupName = "RequiredStates")]
  [TemplateVisualStateAttribute(Name = "Required", GroupName = "RequiredStates")]
  #else
  [LocalizabilityAttribute(LocalizationCategory.Label)]
  #endif
  public class Label : 
    #if WINDOWS_PHONE
    System.Windows.Controls.TextBox
    #else
    System.Windows.Controls.Label
    #endif
  {

    #region --- Constructor ---

    #if !SILVERLIGHT && !WINDOWS_PHONE
    static Label()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Label), new FrameworkPropertyMetadata(typeof(System.Windows.Controls.Label)));
    }
    #endif

    public Label()
    {
      ApplyStyle();
    }

    public virtual void ApplyStyle()
    {
      //note: don't call base.ApplyStyle() at descendent
      DefaultStyleKey = typeof(
        #if WINDOWS_PHONE
        System.Windows.Controls.TextBox
        #else
        System.Windows.Controls.Label
        #endif
      );

      #if WINDOWS_PHONE
      Padding = new Thickness(-12);
      Background = null;
      BorderBrush = null;
      TextAlignment = ConvertAlignment(HorizontalContentAlignment);
      if ((Content == null) && WPF_DesignerProperties.IsInDesignTool) Text = "Label";
      #endif   
    }

    #endregion

    #region --- Properties ---

    #if WINDOWS_PHONE
    
    #region Content

    /// <summary>
    /// Content Dependency Property
    /// </summary>
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register("Content", typeof(object), typeof(WPF_Compatibility.Label),
          new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnContentChanged)));

    /// <summary>
    /// Gets or sets the Content property
    /// </summary>
    public object Content
    {
      get { return GetValue(ContentProperty); }
      set { SetValue(ContentProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Content property
    /// </summary>
    private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      WPF_Compatibility.Label target = (WPF_Compatibility.Label)d;
      object oldContent = (object)e.OldValue;
      object newContent = target.Content;
      target.OnContentChanged(oldContent, newContent);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Content property
    /// </summary>
    protected virtual void OnContentChanged(object oldContent, object newContent)
    {
      Text = ((newContent == null) && WPF_DesignerProperties.IsInDesignTool)? "Label" : (string)newContent;
    }

    #endregion

    #endif

    #endregion

    #region --- Methods ---

    public static TextAlignment ConvertAlignment(HorizontalAlignment h)
    {
      switch (h)
      {
        case HorizontalAlignment.Center: return TextAlignment.Center;
        case HorizontalAlignment.Left: return TextAlignment.Left;
        case HorizontalAlignment.Right: return TextAlignment.Right;
        case HorizontalAlignment.Stretch: return TextAlignment.Justify;
        default:
          return TextAlignment.Left;
      }
    }

    #endregion

  }
}
