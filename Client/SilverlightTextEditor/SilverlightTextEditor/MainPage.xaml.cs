﻿//Version: 20120626
//Editor: George Birbilis <birbilis@kagi.com>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Printing;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace SilverlightTextEditor
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        //Initialize the RichTextBox. The intial text is saved as XAML in the sample.sav file.
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
           rtb.Xaml = XElement.Load("/SilverlightTextEditor;component/sample.sav").ToString();
        }

        #region Bold, Italics & Underline

        //Set Bold formatting to the selected content 
        private void btnBold_Click(object sender, RoutedEventArgs e)
        {
            if (rtb != null && rtb.Selection.Text.Length > 0)
            {
                if (rtb.Selection.GetPropertyValue(Run.FontWeightProperty) is FontWeight && ((FontWeight)rtb.Selection.GetPropertyValue(Run.FontWeightProperty)) == FontWeights.Normal)
                    rtb.Selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold);
                else
                    rtb.Selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
            }
            ReturnFocus();
        }

        //Set Italic formatting to the selected content 
        private void btnItalic_Click(object sender, RoutedEventArgs e)
        {
            if (rtb != null && rtb.Selection.Text.Length > 0)
            {
                if (rtb.Selection.GetPropertyValue(Run.FontStyleProperty) is FontStyle && ((FontStyle)rtb.Selection.GetPropertyValue(Run.FontStyleProperty)) == FontStyles.Normal)
                    rtb.Selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Italic);
                else
                    rtb.Selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
            }
            ReturnFocus();
        }

        //Set Underline formatting to the selected content 
        private void btnUnderline_Click(object sender, RoutedEventArgs e)
        {
            if (rtb != null && rtb.Selection.Text.Length > 0)
            {
                if (rtb.Selection.GetPropertyValue(Run.TextDecorationsProperty) == null)
                    rtb.Selection.ApplyPropertyValue(Run.TextDecorationsProperty, TextDecorations.Underline);
                else
                    rtb.Selection.ApplyPropertyValue(Run.TextDecorationsProperty, null);
            }
            ReturnFocus();

        }
        #endregion 

        #region Font Type, Color & size 

        //Set font type to selected content
        private void cmbFonts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rtb != null && rtb.Selection.Text.Length > 0)
            {
                rtb.Selection.ApplyPropertyValue(Run.FontFamilyProperty, new FontFamily((cmbFonts.SelectedItem as ComboBoxItem).Tag.ToString()));
            }
            ReturnFocus();
        }

        //Set font size to selected content
        private void cmbFontSizes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rtb != null && rtb.Selection.Text.Length > 0)
            {
                rtb.Selection.ApplyPropertyValue(Run.FontSizeProperty, double.Parse((cmbFontSizes.SelectedItem as ComboBoxItem).Tag.ToString()));
            }
            ReturnFocus();
        }

        //Set font color to selected content
        private void cmbFontColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rtb != null && rtb.Selection.Text.Length > 0)
            {
                string color = (cmbFontColors.SelectedItem as ComboBoxItem).Tag.ToString();

                SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(
                    byte.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(6, 2), System.Globalization.NumberStyles.HexNumber)));

                rtb.Selection.ApplyPropertyValue(Run.ForegroundProperty, brush);
            }
            ReturnFocus();
        }
        #endregion 

        #region Insert UIElements

        //Insert an image into the RichTextBox
        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            InlineUIContainer container = new InlineUIContainer();

            container.Child = MainPage.createImageFromUri(new Uri("/SilverlightTextEditor;component/Images/Desert.jpg", UriKind.RelativeOrAbsolute), 200, 150);

            rtb.Selection.Insert(container);
            ReturnFocus();
        }

        private static Image createImageFromUri(Uri URI, double width, double height)
        {
            Image img = new Image();
            img.Stretch = Stretch.Uniform;
            img.Width = width;
            img.Height = height;
            BitmapImage bi = new BitmapImage(URI);
            img.Source = bi;
            img.Tag = bi.UriSource.ToString();
            return img;
        }

        //Insert a Datagrid into the RichTextBox
        private void btnDatagrid_Click(object sender, RoutedEventArgs e)
        {
            InlineUIContainer container = new InlineUIContainer();

            container.Child = getDataGrid();

            rtb.Selection.Insert(container);
            ReturnFocus();
        }

        private DataGrid getDataGrid()
        {
            DataGrid dg = new DataGrid();
            dg.AutoGenerateColumns = true;
            dg.Width = 500;
            dg.Height = 150;
            dg.ItemsSource = Customer.GetSampleCustomerList();
            dg.Style = (Style)this.Resources["DataGridStyle1"];

            return dg;
        }

        //Insert a Calendar into the RichTextBox
        private void btnCalendar_Click(object sender, RoutedEventArgs e)
        {
            InlineUIContainer container = new InlineUIContainer();

            container.Child = getCalendar();

            rtb.Selection.Insert(container);
            ReturnFocus();
        }

        private Calendar getCalendar()
        {
            Calendar cal = new Calendar();
            cal.Width = 179;
            cal.Height = 169;
            cal.FontFamily = new FontFamily("Portable User Interface");
            cal.Style = (Style)this.Resources["CalendarStyle1"];

            return cal;
        }

        #endregion 

        #region Insert Hyperlink

        //Insert a hyperlink
        private void btnHyperlink_Click(object sender, RoutedEventArgs e)
        {
            InsertURL cw = new InsertURL(rtb.Selection.Text);
            cw.HasCloseButton = false;
            
            //Hook up an event handler to the Closed event on the ChildWindows cw. 
            cw.Closed += (s, args) =>
            {
                if (cw.DialogResult.Value)
                {
                    Hyperlink hyperlink = new Hyperlink();
                    hyperlink.TargetName = "_blank";
                    hyperlink.NavigateUri = new Uri(cw.txtURL.Text);

                    if (cw.txtURLDesc.Text.Length > 0)
                        hyperlink.Inlines.Add(cw.txtURLDesc.Text);
                    else
                        hyperlink.Inlines.Add(cw.txtURL.Text);

                    rtb.Selection.Insert(hyperlink);
                    ReturnFocus();
                }
            };
            cw.Show();
        }
        #endregion 

        #region Clipboard Operations

        //Cut the selected text
        private void btnCut_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(rtb.Selection.Text);
            rtb.Selection.Text = "";
            ReturnFocus();
        }

        //Copy the selected text
        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(rtb.Selection.Text);
            ReturnFocus();
        }

        //paste the text
        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            rtb.Selection.Text = Clipboard.GetText();
            ReturnFocus();
        }
        #endregion 

        #region Print

        //Print the document
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintPreview cw = new PrintPreview();
            cw.ShowPreview(rtb);
            cw.HasCloseButton = false;

            //Hook up a handler to the Closed event before we display the PrintPreview window by calling the Show() method.
            cw.Closed += (t, a) =>
            {
                if (cw.DialogResult.Value)
                {
                    PrintDocument theDoc = new PrintDocument();
                    theDoc.PrintPage += (s, args) =>
                    {
                        args.PageVisual = rtb;
                        args.HasMorePages = false;
                    };

                    theDoc.EndPrint += (s, args) =>
                    {
                        MessageBox.Show("The document printed successfully", "Text Editor", MessageBoxButton.OK);
                    };

                    theDoc.Print("Silverlight 4 Text Editor");
                    ReturnFocus();
                }
            };
            cw.Show();
        }

        #endregion 

        #region LeftToRight FlowDirection

        //Set the flow direction
        public void btnRTL_Checked(object sender, RoutedEventArgs e)
        {
            //Set the button image based on the state of the toggle button. 
            if(btnRTL.IsChecked.Value)
                btnRTL.Content = MainPage.createImageFromUri(new Uri("/SilverlightTextEditor;component/Images/rtl.png", UriKind.RelativeOrAbsolute), 30, 32);
            else
                btnRTL.Content = MainPage.createImageFromUri(new Uri("/SilverlightTextEditor;component/Images/ltr.png", UriKind.RelativeOrAbsolute), 30, 32);

            ApplicationBorder.FlowDirection = (ApplicationBorder.FlowDirection == System.Windows.FlowDirection.LeftToRight) ? System.Windows.FlowDirection.RightToLeft : System.Windows.FlowDirection.LeftToRight;
            ReturnFocus();
            
        }
        #endregion 

        #region XAML Markup
        
        //Set the xamlTb TextBox with the current XAML of the RichTextBox and make it visible. Any changes to the XAML made 
        //in xamlTb is also reflected back on the RichTextBox. Note that the Xaml string returned by RichTextBox.Xaml will 
        //not include any UIElement contained in the current RichTextBox. Hence the UIElements will be lost when we 
        //set the Xaml back again from the xamlTb to the RichTextBox.
        public void btnMarkUp_Checked(object sender, RoutedEventArgs e)
        {
            if (btnMarkUp.IsChecked.Value)
            {
                xamlTb.Visibility = System.Windows.Visibility.Visible;
                xamlTb.IsTabStop = true;
                xamlTb.Text = rtb.Xaml;
            }
            else
            {
                rtb.Xaml = xamlTb.Text;
                xamlTb.Visibility = System.Windows.Visibility.Collapsed;
                xamlTb.IsTabStop = false;
            }

        }

        #endregion 

        #region Read only RichTextBox

        //Make the RichTextBox read-only
        public void btnRO_Checked(object sender, RoutedEventArgs e)
        {
            rtb.IsReadOnly = !rtb.IsReadOnly;

            //Set the button image based on the state of the toggle button.
            if (rtb.IsReadOnly)
                btnRO.Content = MainPage.createImageFromUri(new Uri("/SilverlightTextEditor;component/Images/view.png", UriKind.RelativeOrAbsolute), 29, 32);
            else
                btnRO.Content = MainPage.createImageFromUri(new Uri("/SilverlightTextEditor;component/Images/edit.png", UriKind.RelativeOrAbsolute), 29, 32);
            ReturnFocus();
        }

        #endregion 

        #region Context Menu

        //Though we dont execute any logic on Right Mouse button down, we need to ensure the event is set to be handled to allow
        //the subsequent Right Mouse button up to be raised on the control. The context menu is displayed when the right mouse
        //button up event is raised. 
        private void rtb_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void rtb_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Construct and display the context menu

            RTBContextMenu menu = new RTBContextMenu(rtb);
            menu.Show(e.GetPosition(LayoutRoot));
        }
        #endregion 

        #region Highlight

        private List<Rect> m_selectionRect = new List<Rect>();

        public void btnHighlight_Checked(object sender, RoutedEventArgs e)
        {
            if (btnHighlight.IsChecked.Value)
            {
                TextPointer tp = rtb.ContentStart;
                TextPointer nextTp = null;
                Rect nextRect = Rect.Empty;
                Rect tpRect = tp.GetCharacterRect(LogicalDirection.Forward);
                Rect lineRect = Rect.Empty;

                int lineCount = 1;

                while (tp != null)
                {
                    nextTp = tp.GetNextInsertionPosition(LogicalDirection.Forward);
                    if (nextTp != null && nextTp.IsAtInsertionPosition)
                    {
                        nextRect = nextTp.GetCharacterRect(LogicalDirection.Forward);
                        // this occurs for more than one line
                        if (nextRect.Top > tpRect.Top)
                        {
                            if (m_selectionRect.Count < lineCount)
                                m_selectionRect.Add(lineRect);
                            else
                                m_selectionRect[lineCount - 1] = lineRect;

                            lineCount++;

                            if (m_selectionRect.Count < lineCount)
                                m_selectionRect.Add(nextRect);

                            lineRect = nextRect;
                        }
                        else if (nextRect != Rect.Empty)
                        {
                            if (tpRect != Rect.Empty)
                                lineRect.Union(nextRect);
                            else
                                lineRect = nextRect;
                        }
                    }
                    tp = nextTp;
                    tpRect = nextRect;
                }
                if (lineRect != Rect.Empty)
                {
                    if (m_selectionRect.Count < lineCount)
                        m_selectionRect.Add(lineRect);
                    else
                        m_selectionRect[lineCount - 1] = lineRect;
                }
                while (m_selectionRect.Count > lineCount)
                {
                    m_selectionRect.RemoveAt(m_selectionRect.Count - 1);
                }
            }
            else
            {
                if (highlightRect != null)
                {
                    highlightRect.Visibility = System.Windows.Visibility.Collapsed;
                }
            }

        }

        Rectangle highlightRect;
        MouseEventArgs lastRTBMouseMove;
        private void rtb_MouseMove(object sender, MouseEventArgs e)
        {
            lastRTBMouseMove = e;
            if (btnHighlight.IsChecked.Value)
            {
                foreach (Rect r in m_selectionRect)
                {
                    if (r.Contains(e.GetPosition(highlightCanvas)))
                    {
                        if (highlightRect == null)
                        {
                            highlightRect = CreateHighlightRectangle(r);
                        }
                        else
                        {
                            highlightRect.Visibility = System.Windows.Visibility.Visible;
                            highlightRect.Width = r.Width;
                            highlightRect.Height = r.Height;
                            Canvas.SetLeft(highlightRect, r.Left);
                            Canvas.SetTop(highlightRect, r.Top);
                        }
                    }
                }
            }
        }

        private Rectangle CreateHighlightRectangle(Rect bounds)
        {
            Rectangle r = new Rectangle();
            r.Fill = new SolidColorBrush(Color.FromArgb(75, 0, 0, 200));
            r.Stroke = new SolidColorBrush(Color.FromArgb(230, 0, 0, 254));
            r.StrokeThickness = 1;
            r.Width = bounds.Width;
            r.Height = bounds.Height;
            Canvas.SetLeft(r, bounds.Left);
            Canvas.SetTop(r, bounds.Top);

            highlightCanvas.Children.Add(r);

            return r;

        }
        #endregion 

        #region DragAndDrop

        private void rtb_Drop(object sender, System.Windows.DragEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);

            //the Drop event passes in an array of FileInfo objects for the list of files that were selected and drag-dropped onto the RichTextBox.
            if (e.Data == null)
            {
                ReturnFocus();
                return;
            }

            //This checks if the dropped objects are files and if not, return. 
            IDataObject f = e.Data as IDataObject;

            if (f == null)
            {
                ReturnFocus();
                return;
            }

            object data = f.GetData(DataFormats.FileDrop);
            FileInfo[] files = data as FileInfo[];

            if (files == null)
            {
                ReturnFocus();
                return;
            }

            //Walk through the list of FileInfo objects of the selected and drag-dropped files and parse the .txt and .docx files 
            //and insert their content in the RichTextBox.
            foreach (FileInfo file in files)
            {
                if (file == null)
                {
                    continue;
                }

                if (file.Extension.Equals(".txt"))
                {
                    ParseTextFile(file);
                }
                else if (file.Extension.Equals(".docx"))
                {
                    ParseDocxFile(file);
                }
            }
            ReturnFocus();
        }

        //Create a StreamReader on the text file and read as a string. 
        private void ParseTextFile(FileInfo file)
        {
            Stream sr = file.OpenRead();
            string contents;
            using (StreamReader reader = new StreamReader(sr))
            {
                contents = reader.ReadToEnd();
            }

            rtb.Selection.Text = contents;
            sr.Close();                 
        }

        private void ParseDocxFile(FileInfo file)
        {
            Stream sr = file.OpenRead();
            string contents;

            StreamResourceInfo zipInfo = new StreamResourceInfo(sr, null);
            StreamResourceInfo wordInfo = Application.GetResourceStream(zipInfo, new Uri("word/document.xml", UriKind.Relative));

            using (StreamReader reader = new StreamReader(wordInfo.Stream))
            {
                contents = reader.ReadToEnd();
            }

            XDocument xmlFile = XDocument.Parse(contents);
            XNamespace w = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

            var query = from xp in xmlFile.Descendants(w + "p")
                        select xp;
            Paragraph p = null;
            Run r = null;
            foreach (XElement xp in query)
            {
                p = new Paragraph();
                var query2 = from xr in xp.Descendants(w + "r")
                             select xr;
                foreach (XElement xr in query2)
                {
                    r = new Run();
                    var query3 = from xt in xr.Descendants()
                                 select xt;
                    foreach (XElement xt in query3)
                    {
                        if (xt.Name == (w + "t"))
                            r.Text = xt.Value.ToString();
                        else if (xt.Name == (w + "br"))
                            p.Inlines.Add(new LineBreak());
                    }
                    p.Inlines.Add(r);
                }
                p.Inlines.Add(new LineBreak());
                rtb.Blocks.Add(p);
            }
        }

        private void rtb_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            VisualStateManager.GoToState(this, "DragOver", true);
        }

        private void rtb_DragLeave(object sender, System.Windows.DragEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);        
        }

        #endregion 

        #region FileOperations
        //Clears the contents of the existing file.
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            rtb.Blocks.Clear();
        }

        //Saves the existing file
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string ContentToSave = rtb.Xaml;

            //Check if the file contains any UIElements
            var res = from block in rtb.Blocks
                      from inline in (block as Paragraph).Inlines
                      where inline.GetType() == typeof(InlineUIContainer)
                      select inline;

            //If the file contains any UIElements, it will not be saved
            if (res.Count() != 0)
            {
                MessageBox.Show("Saving documents with UIElements is not supported");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".sav";
            sfd.Filter = "Saved Files|*.sav|All Files|*.*";

            if (sfd.ShowDialog().Value)
            {
                using (FileStream fs = (FileStream)sfd.OpenFile())
                {
                    System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                    byte[] buffer = enc.GetBytes(ContentToSave);
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Close();
                }
            }
        }

        //Opens an existing file
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Saved Files|*.sav|All Files|*.*";

            if (ofd.ShowDialog().Value)
            {
                FileInfo fi = ofd.File;
                StreamReader r = fi.OpenText();
                rtb.Xaml = r.ReadToEnd();
                r.Close();
            }
        }
        #endregion

        #region helper functions

        private void ReturnFocus()
        {
            if (rtb != null)
                rtb.Focus();
        }
        #endregion 

    }
}
