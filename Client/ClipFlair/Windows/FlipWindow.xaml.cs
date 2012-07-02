//Version: 20120628

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

        public object BackContent
        {
            get { return FlipPanel.BackContent; }
            set { FlipPanel.BackContent = value; }
        }


    }

}
