//Version: 20120711

using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using SilverFlow.Controls;
                                        
namespace ClipFlair
{
    [ContentProperty("FrontContent")]
    public partial class FlipWindow : FloatingWindow
    {
        public FlipWindow()
        {
            InitializeComponent();
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
