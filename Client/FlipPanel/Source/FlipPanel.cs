﻿//Filename: FlipPanel.cs
//Version: 20130326

using WPFCompatibility;

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
        public static readonly DependencyProperty FrontContentProperty = DependencyProperty.Register("FrontContent", typeof(object), typeof(FlipPanel), null);
        public static readonly DependencyProperty BackContentProperty = DependencyProperty.Register("BackContent", typeof(object), typeof(FlipPanel), null);
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlipPanel), null);
        public static readonly DependencyProperty IsFlippedProperty = DependencyProperty.Register("IsFlipped", typeof(bool), typeof(FlipPanel), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty IsAnimatedProperty = DependencyProperty.Register("IsAnimated", typeof(bool), typeof(FlipPanel), new FrameworkPropertyMetadata(true));

        public object FrontContent
        {
            get { return GetValue(FrontContentProperty); }
            set { SetValue(FrontContentProperty, value); }
        }

        public object BackContent
        {
            get { return GetValue(BackContentProperty); }
            set { SetValue(BackContentProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public bool IsFlipped
        {
            get { return (bool)GetValue(IsFlippedProperty); }

            set //TODO: property setters may not be called when a property is set via XAML databinding, should use a callback handler for the dependency property instead to do ChangeVisualState
            {
                SetValue(IsFlippedProperty, value);
                ChangeVisualState(IsAnimated);
            }
        }

        public bool IsAnimated
        {
          get { return (bool)GetValue(IsAnimatedProperty); }
          set { SetValue(IsAnimatedProperty, value); }
        }
      
        public FlipPanel()
        {
            DefaultStyleKey = typeof(FlipPanel);
        }

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
            this.IsFlipped = !this.IsFlipped;
        }

        private void ChangeVisualState(bool useTransitions)
        {
            if (!this.IsFlipped) { VisualStateManager.GoToState(this, "Normal", useTransitions); }
            else { VisualStateManager.GoToState(this, "Flipped", useTransitions); }
        }                             
                
    }

}
