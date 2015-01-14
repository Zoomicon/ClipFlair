//Filename: FlipPanel.cs
//Version: 20150108

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace FlipPanel
{
  [TemplatePart(Name=PART_LAYOUTROOT, Type = typeof(Grid))]
  [TemplatePart(Name = PART_FLIPBUTTON, Type = typeof(ToggleButton))]
  [TemplatePart(Name = PART_FLIPBUTTONALTERNATE, Type = typeof(ToggleButton))]
  [TemplateVisualState(Name = STATE_NORMAL, GroupName = STATE_GROUP)]
  [TemplateVisualState(Name = STATE_FLIPPED, GroupName = STATE_GROUP)]
  [ContentProperty("FrontContent")]
  public class FlipPanel : Control
  {

    #region Constants

    public const string PART_LAYOUTROOT = "LayoutRoot";
    public const string PART_FLIPBUTTON = "FlipButton";
    public const string PART_FLIPBUTTONALTERNATE = "FlipButtonAlternate";

    public const string STATE_GROUP = "ViewStates";
    public const string STATE_NORMAL = "Normal";
    public const string STATE_FLIPPED = "Flipped";

    #endregion

    #region --- Initialization ---

    public FlipPanel()
    {
      DefaultStyleKey = typeof(FlipPanel);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      RegisterVSMevent();

      ToggleButton flipButton = GetTemplateChild(PART_FLIPBUTTON) as ToggleButton;
      if (flipButton != null) flipButton.Click += flipButton_Click;

      // Allow for two flip buttons if needed (one for each side of the panel).
      // This is an optional design, as the control consumer may use template
      // that places the flip button outside of the panel sides, like the 
      // default template does.
      ToggleButton flipButtonAlternate = GetTemplateChild(PART_FLIPBUTTONALTERNATE) as ToggleButton;
      if (flipButtonAlternate != null) flipButtonAlternate.Click += flipButton_Click;

      this.ChangeVisualState(false); //do not do any animation at this phase
    }

    private void RegisterVSMevent()
    {
      FrameworkElement layoutRoot = (FrameworkElement)GetTemplateChild(PART_LAYOUTROOT);
      if (layoutRoot == null) return;

      foreach (VisualStateGroup grp in VisualStateManager.GetVisualStateGroups(layoutRoot))
      {
        if (grp.Name == STATE_GROUP)
        {
          //grp.CurrentStateChanged += (s, e) => OnFlipped();

          //this is not the same as above
          Collection<VisualState> states = (Collection<VisualState>)grp.States;
          foreach (VisualState state in states)
          {
            switch (state.Name)
            {
              case STATE_NORMAL:
                state.Storyboard.Completed += (s,e) => OnFlipped();
                break;
              case STATE_FLIPPED:
                state.Storyboard.Completed += (s,e) => OnFlipped();
                break;
            }
          }

          break;
        }
      }
    }

    #endregion

    #region --- Properties ---

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

    #region IsFlipping

    public bool IsFlipping { get; protected set; }

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
        OnFlipping();
        ChangeVisualState(IsAnimated); //storyboard completion event will call "OnFlipped"
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

    #region --- Methods ---

    public void DoFlip()
    {
      if (IsEnabled)
        IsFlipped = !IsFlipped;
    }

    private void ChangeVisualState(bool useTransitions)
    {
      if (!this.IsFlipped)
        VisualStateManager.GoToState(this, "Normal", useTransitions);
      else
        VisualStateManager.GoToState(this, "Flipped", useTransitions);
    }

    #endregion
    
    #region --- Events ---

    public event EventHandler Flipping;
    public event EventHandler Flipped;

    private void flipButton_Click(object sender, RoutedEventArgs e)
    {
      DoFlip();
    }

    protected virtual void OnFlipping()
    {
      IsFlipping = true;
      if (Flipping != null)
        Flipping(this, EventArgs.Empty);
    }

    protected virtual void OnFlipped()
    {
      IsFlipping = false; //note: don't set "IsFlipped" here by mistake
      if (Flipped != null)
        Flipped(this, EventArgs.Empty);
    }

    #endregion

  }

}
