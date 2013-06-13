//Filename: FlipPanel.cs
//Version: 20130612

using WPFCompatibility;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace FlipPanel
{
  [TemplatePart(Name = "FlipButton", Type = typeof(ToggleButton))]
  [TemplatePart(Name = "FlipButtonAlternate", Type = typeof(ToggleButton))]
  [TemplateVisualState(Name = "Normal", GroupName = "ViewStates")]
  [TemplateVisualState(Name = "Flipped", GroupName = "ViewStates")]
  [ContentProperty("FrontContent")]
  public class FlipPanel : Control
  {

    #region Constructor

    public FlipPanel()
    {
      DefaultStyleKey = typeof(FlipPanel);
    }

    #endregion

    #region Properties

    #region FrontContent

    public static readonly DependencyProperty FrontContentProperty = DependencyProperty.Register("FrontContent", typeof(object), typeof(FlipPanel), null);
       
    public object FrontContent
    {
      get { return GetValue(FrontContentProperty); }
      set { SetValue(FrontContentProperty, value); }
    }

    #endregion

    #region BackContent

    public static readonly DependencyProperty BackContentProperty = DependencyProperty.Register("BackContent", typeof(object), typeof(FlipPanel), null);

    public object BackContent
    {
      get { return GetValue(BackContentProperty); }
      set { SetValue(BackContentProperty, value); }
    }

    #endregion

    #region CornerRadius

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlipPanel), null);
    
    public CornerRadius CornerRadius
    {
      get { return (CornerRadius)GetValue(CornerRadiusProperty); }
      set { SetValue(CornerRadiusProperty, value); }
    }

    #endregion

    #region IsFlipped

    public static readonly DependencyProperty IsFlippedProperty = 
      DependencyProperty.Register(
        "IsFlipped",
        typeof(bool),
        typeof(FlipPanel),
        new FrameworkPropertyMetadata(false, OnIsFlippedChanged));

    private static void OnIsFlippedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      FlipPanel target = (FlipPanel)d;
      bool oldIsFlipped = (bool)e.OldValue;
      bool newIsFlipped = target.IsFlipped;
      target.OnIsFlippedChanged(oldIsFlipped, newIsFlipped);
    }
    
    protected void OnIsFlippedChanged(bool oldIsFlipped, bool newIsFlipped)
    {
      if (oldIsFlipped != newIsFlipped) {
        ChangeVisualState(IsAnimated);
        OnFlipped();
      }
    }

    public bool IsFlipped
    {
      get { return (bool)GetValue(IsFlippedProperty); }
      set { SetValue(IsFlippedProperty, value); } //property setters may not be called when a property is set via XAML databinding, so making sure we do any updating at "OnIsFlippedchanged"
    }

    #endregion

    #region IsAnimated

    public static readonly DependencyProperty IsAnimatedProperty = DependencyProperty.Register("IsAnimated", typeof(bool), typeof(FlipPanel), new FrameworkPropertyMetadata(true));

    public bool IsAnimated
    {
      get { return (bool)GetValue(IsAnimatedProperty); }
      set { SetValue(IsAnimatedProperty, value); }
    }

    #endregion

    #endregion

    #region Methods

    public void DoFlip()
    {
      if (IsEnabled)
        this.IsFlipped = !this.IsFlipped;
    }

    private void ChangeVisualState(bool useTransitions)
    {
      if (!this.IsFlipped)
        VisualStateManager.GoToState(this, "Normal", useTransitions);
      else
        VisualStateManager.GoToState(this, "Flipped", useTransitions);
    }

    #endregion
    
    #region Events

    public event EventHandler Flipped;

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      ToggleButton flipButton = base.GetTemplateChild("FlipButton") as ToggleButton;
      if (flipButton != null) flipButton.Click += flipButton_Click;

      // Allow for two flip buttons if needed (one for each side of the panel).
      // This is an optional design, as the control consumer may use template
      // that places the flip button outside of the panel sides, like the 
      // default template does.
      ToggleButton flipButtonAlternate = base.GetTemplateChild("FlipButtonAlternate") as ToggleButton;
      if (flipButtonAlternate != null) flipButtonAlternate.Click += flipButton_Click;

      this.ChangeVisualState(false); //do not do any animation at this phase
    }

    private void flipButton_Click(object sender, RoutedEventArgs e)
    {
      DoFlip();
    }

    protected void OnFlipped()
    {
      if (Flipped != null)
        Flipped(this, EventArgs.Empty);
    }

    #endregion

  }

}
