//Version: 20120730

using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using SilverFlow.Controls;
                                        
namespace ClipFlair.Components
{
    [ContentProperty("FrontContent")]
    public partial class FlipWindow : FloatingWindow
    {
        public FlipWindow()
        {
            InitializeComponent();
            ShowMaximizeButton = false; //!!! (till we fix it to resize to current visible view area and to allow moving the window in that case only [when it's not same size as parent])
        }

        public object FrontContent
        {
            get { return FlipPanel.FrontContent; }
            set { FlipPanel.FrontContent = value; }
        }

        public object BackContent //if one wants to replace the default backcontent that hosts the PropertiesPanel etc.
        {
          get { return FlipPanel.BackContent; }
          set { FlipPanel.BackContent = value; }
        }

        public UIElementCollection PropertyItems
        {
            get { return PropertiesPanel.Children; }
            set { 
              PropertiesPanel.Children.Clear();
              foreach (UIElement item in value) { PropertiesPanel.Children.Add(item); }
            }
        }


    }

}
