//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: SilverTextEditor.xaml.cs
//Version: 20140318

//Originated from Microsoft Silverlight sample (MSPL license)

//Note: localization could use "PublicResxFileCodeGeneratorEx" custom build tool for the "Strings.resx" file
//      from http://resxfilecodegenex.codeplex.com/ if that is fixed to generate public constuctor instead of protected (see issue tracker there)
//      (Currently one needs to open Resources\Strings.Designer.cs and change internal constructor to public)

//TODO: allow to add checkbox controls and remember their state (checked/unchecked) in the text
//TODO: allow adding images (from URL) and remember them at reload (could add as special text and then parse it?)

//using SilverTextEditor.Resources;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Printing;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Xaml;
using System.Xml;
using System.Xml.Linq;
using Utils.Extensions;

namespace SilverTextEditor
{
  public partial class SilverTextEditor : UserControl
  {

    #region --- Constants ---

    public const string LOAD_FILTER = "All Text Files (*.text;*.docx;*.txt)|*.text;*.docx;*.txt|ClipFlair Text Files (*.text)|*.text|Office OpenXML Files (*.docx)|*.docx|Unicode Text Files (*.txt)|*.txt|All Files|*.*";
    public const string SAVE_FILTER = "ClipFlair Text Files (*.text)|*.text|Unicode Text Files (*.txt)|*.txt|All Files|*.*";

    #endregion

    public SilverTextEditor()
    {
      InitializeComponent();
      //DefaultStyleKey = typeof(SilverTextEditor); //if we convert this to Control, need to do this

      //Note: Do not use "Loaded" event handler to load any default text file, it gets called after some caller has instantiated the component and overrides any text that may have been set to it already by the caller
      //Instead can load a previously saved file from a resource at this point, like below:
      //LoadXamlResource("/SilverTextEditor;component/ActivityDescription.text"); //Initialize the RichTextBox. The intial text is stored as XAML at a .text file.
    }

    public RichTextBox Document
    {
      get { return rtb; }
    }

    #region --- Bold, Italics & Underline ---

    //Toggle Bold formatting at the selected content if any, else at the text under the cursor (the current "Run")
    private void btnBold_Click(object sender, RoutedEventArgs e)
    {
      if (rtb == null) return;

      if (HasSelection)
        ToggleSelectionProperty(Run.FontWeightProperty, FontWeights.Bold, FontWeights.Normal);
      else
      {
        Run currentRun = CurrentRun;
        if (currentRun != null)
          currentRun.FontWeight = (currentRun.FontWeight != FontWeights.Bold) ? FontWeights.Bold : FontWeights.Normal;
        else
          rtb.FontWeight = (rtb.FontWeight != FontWeights.Bold) ? FontWeights.Bold : FontWeights.Normal;
      }
      ReturnFocus();
    }

    //Toggle Italic formatting at the selected content if any, else at the text under the cursor (the current "Run")
    private void btnItalic_Click(object sender, RoutedEventArgs e)
    {
      if (rtb == null) return;

      if (HasSelection)
        ToggleSelectionProperty(Run.FontStyleProperty, FontStyles.Italic, FontStyles.Normal);
      else
      {
        Run currentRun = CurrentRun;
        if (currentRun != null)
          currentRun.FontStyle = (currentRun.FontStyle != FontStyles.Italic) ? FontStyles.Italic : FontStyles.Normal;
        else
          rtb.FontStyle = (rtb.FontStyle != FontStyles.Italic) ? FontStyles.Italic : FontStyles.Normal;
      }
      ReturnFocus();
    }

    //Toggle Underline formatting at the selected content if any, else at the text under the cursor (the current "Run")
    private void btnUnderline_Click(object sender, RoutedEventArgs e)
    {
      if (rtb == null) return;

      if (HasSelection)
        ToggleSelectionProperty(Run.TextDecorationsProperty, TextDecorations.Underline, null);
      else
      {
        Run currentRun = CurrentRun;
        if (currentRun != null)
          currentRun.TextDecorations = (currentRun.TextDecorations != TextDecorations.Underline) ? TextDecorations.Underline : null;
        //else
        //  rtb.TextDecorations = (rtb.TextDecorations != TextDecorations.Underline) ? TextDecorations.Underline : null; //TODO: check why such a property doesn't exist
      }
      ReturnFocus();
    }

    #endregion

    #region --- Font Type, Color & size ---

    //Set font type at the selected content if any, else at the text under the cursor (the current "Run")
    private void cmbFonts_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (rtb == null) return;

      FontFamily fontFamily = new FontFamily((((ComboBox)sender).SelectedItem as ComboBoxItem).Tag.ToString()); //don't use cmbFonts here, event handler is called at object initialization, before that field is assigned

      if (HasSelection)
        ChangeSelectionProperty(Run.FontFamilyProperty, fontFamily);
      else
      {
        Run currentRun = CurrentRun;
        if (currentRun != null)
          currentRun.FontFamily = fontFamily;
        else
          rtb.FontFamily = fontFamily;
      }
      ReturnFocus();
    }

    //Set font size to selected content if any, else at the text under the cursor (the current "Run")
    private void cmbFontSizes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (rtb == null) return;

      double fontSize = double.Parse((((ComboBox)sender).SelectedItem as ComboBoxItem).Tag.ToString()); //don't use cmbFontSizes here, event handler is called at object initialization, before that field is assigned

      if (HasSelection)
        ChangeSelectionProperty(Run.FontSizeProperty, fontSize);
      else
      {
        Run currentRun = CurrentRun;
        if (currentRun != null)
          currentRun.FontSize = fontSize;
        else
          rtb.FontSize = fontSize;
      }
      ReturnFocus();
    }

    //Set font color to selected content if any, else at the text under the cursor (the current "Run")
    private void cmbFontColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (rtb == null) return;

      string color = (((ComboBox)sender).SelectedItem as ComboBoxItem).Tag.ToString(); //don't use cmbFontColors here, event handler is called at object initialization, before that field is assigned

      SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(
        byte.Parse(color.Substring(0, 2), NumberStyles.HexNumber),
        byte.Parse(color.Substring(2, 2), NumberStyles.HexNumber),
        byte.Parse(color.Substring(4, 2), NumberStyles.HexNumber),
        byte.Parse(color.Substring(6, 2), NumberStyles.HexNumber)));

      if (HasSelection)
        ChangeSelectionProperty(Run.ForegroundProperty, brush);
      else
      {
        Run currentRun = CurrentRun;
        if (currentRun != null)
          currentRun.Foreground = brush;
        else
          rtb.Foreground = brush;
      }
      ReturnFocus();
    }

    #endregion

    //TODO: maybe all Insert actions could be grouped under one popup menu item (and also show on right click menu as submenu)

    #region --- Insert Image ---
    //Insert an image into the RichTextBox //COMMENTED OUT SINCE IT CAN'T STORE INLINE UI ELEMENTS (RICHBOX'S XAML PROPERTY DOESN'T INCLUDE THEM)
    private void btnImage_Click(object sender, RoutedEventArgs e)
    {
      InlineUIContainer container = new InlineUIContainer();
      container.Child = new Uri("http://gallery.clipflair.net/image/SocialSignup.jpg", UriKind.RelativeOrAbsolute).CreateImage(); //TODO: should show URL dialog here (remove comment about editmode and clickable hyperlinks dynamically) to enter image URL

      Selection.Insert(container);
  
      ReturnFocus();
    }

    #endregion

    #region --- Insert Timestamp ---

    //Insert timestamp into the RichTextBox
    private void btnTimestamp_Click(object sender, RoutedEventArgs e)
    {
      Selection.Text = "[" + DateTime.UtcNow.ToString() + " UTC" + "]"; //TODO: change this to call a callback function if any is registered to get the "time"
      ReturnFocus();
    }

    #endregion

    #region --- Hyperlinks ---

    //Insert a hyperlink
    private void btnHyperlink_Click(object sender, RoutedEventArgs e)
    {
      InsertURL cw = new InsertURL(Selection.Text);
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

          Selection.Insert(hyperlink);
          ReturnFocus();
        }
      };
      cw.Show();
    }
    #endregion

    #region --- Clipboard ---

    //Cut the selected text
    private void btnCut_Click(object sender, RoutedEventArgs e)
    {
      Clipboard.SetText(Selection.Text);
      Selection.Text = "";
      ReturnFocus();
    }

    //Copy the selected text
    private void btnCopy_Click(object sender, RoutedEventArgs e)
    {
      Clipboard.SetText(Selection.Text);
      ReturnFocus();
    }

    //paste the text
    private void btnPaste_Click(object sender, RoutedEventArgs e)
    {
      Selection.Text = Clipboard.GetText();
      ReturnFocus();
    }
    #endregion

    #region --- Print ---

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
            //could show prompt that the document printed succesfully here
          };

          //theDoc.Print("SilverTextEditor");
          ReturnFocus();
        }
      };
      cw.Show();
    }

    #endregion

    #region --- XAML Markup ---

    //Set the xamlTb TextBox with the current XAML of the RichTextBox and make it visible. Any changes to the XAML made 
    //in xamlTb is also reflected back on the RichTextBox. Note that the Xaml string returned by RichTextBox.Xaml will 
    //not include any UIElement contained in the current RichTextBox. Hence the UIElements will be lost when we 
    //set the Xaml back again from the xamlTb to the RichTextBox.
    public void btnMarkUp_Checked(object sender, RoutedEventArgs e)
    {
        if (btnMarkUp.IsChecked.Value)
        {
            xamlTb.Text = FormattedXaml;
            xamlTb.Visibility = Visibility.Visible;
            rtb.Visibility = Visibility.Collapsed;
            xamlTb.IsTabStop = true;
        }
        else
        {
            Xaml = xamlTb.Text;
            rtb.Visibility = Visibility.Visible;
            xamlTb.Visibility = System.Windows.Visibility.Collapsed;
            xamlTb.IsTabStop = false;
        }
    }

    #endregion

    #region --- Context Menu ---

    //Though we dont execute any logic on Right Mouse button down, we need to ensure the event is set to be handled to allow
    //the subsequent Right Mouse button up to be raised on the control. The context menu is displayed when the right mouse
    //button up event is raised. 
    private void rtb_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (Keyboard.Modifiers == ModifierKeys.None) //ignore right click if modifier keys like CTRL are pressed to not interfere with other actions like zooming
        e.Handled = true;
    }

    private void rtb_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
      //Construct and display the context menu
      if (!e.Handled && (Keyboard.Modifiers == ModifierKeys.None)) //ignore right click if modifier keys like CTRL are pressed to not interfere with other actions like zooming
      {
        RTBContextMenu menu = new RTBContextMenu(rtb);
        menu.Show(e.GetPosition(null)); //TODO: report bug at original code here, was passing LayoutRoot which won't work correctly if the text editor is inside some parent container
      }
    }
    #endregion

    #region --- Highlight ---

    private List<Rect> m_selectionRect = new List<Rect>();

    public void btnHighlight_Checked(object sender, RoutedEventArgs e)
    {
      if (btnHighlight.IsChecked.Value)
      {
        TextPointer tp = Document.ContentStart;
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
          if (m_selectionRect.Count < lineCount)
            m_selectionRect.Add(lineRect);
          else
            m_selectionRect[lineCount - 1] = lineRect;

        while (m_selectionRect.Count > lineCount)
          m_selectionRect.RemoveAt(m_selectionRect.Count - 1);
      }

      else
        if (highlightRect != null)
          highlightRect.Visibility = System.Windows.Visibility.Collapsed;

    }

    Rectangle highlightRect;
    MouseEventArgs lastRTBMouseMove;

    private void rtb_MouseMove(object sender, MouseEventArgs e)
    {
      lastRTBMouseMove = e;
      if (btnHighlight.IsChecked.Value)
        foreach (Rect r in m_selectionRect)
          if (r.Contains(e.GetPosition(highlightCanvas)))
            if (highlightRect == null)
              highlightRect = CreateHighlightRectangle(r);
            else
            {
              highlightRect.Visibility = System.Windows.Visibility.Visible;
              highlightRect.Width = r.Width;
              highlightRect.Height = r.Height;
              Canvas.SetLeft(highlightRect, r.Left);
              Canvas.SetTop(highlightRect, r.Top);
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
        
    #region --- Scrolling ---

    public void ScrollToStart()
    {
      Selection.Select(Document.ContentStart, Document.ContentStart);
    }

    public void ScrollToEnd()
    {
      Selection.Select(Document.ContentEnd, Document.ContentEnd);
    }

    #endregion

    #region --- Properties ---

    public string Xaml
    {
      get { return rtb.Xaml; }
      set
      {
        if (!String.IsNullOrWhiteSpace(value))
        {
          try
          {
            rtb.Xaml = value;
          }
          catch (Exception e)
          {
            MessageBox.Show("Error loading XAML markup: " + e.Message); //NOTE: use better error dialog with details dropdown (reuse ClipFlair.UI.Dialogs library by removing ClipFlair name)
          }
          ScrollToStart();
        }
        else
          Clear();
      } //allows to set null or blank value to clear the RichTextBox
    }

    public string FormattedXaml
    {
      get
      {
        return FormatXml(Xaml);
      }
    }

    private string FormatXml(string stringXml) //TODO: seems to add empty lines to XAML when called
    {
      StringReader stringReader = new StringReader(stringXml);
      //XDocument represents an XML Document, allowing in memory
      // processing of it (primarily via Linq).
      XDocument xDoc = XDocument.Load(stringReader);
      var stringBuilder = new StringBuilder();
      XmlWriter xmlWriter = null;
      try
      {
        var settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.ConformanceLevel = ConformanceLevel.Auto;
        settings.IndentChars = "  ";
        settings.OmitXmlDeclaration = true;
        //The XDocument can write to an XmlWriter. 
        //The writer formats the well-formed xml.
        //If not well-formed no formating will occur.
        xmlWriter = XmlWriter.Create(stringBuilder, settings);
        xDoc.WriteTo(xmlWriter);
      }
      finally
      {
        if (xmlWriter != null)
          xmlWriter.Close();
      }
      return stringBuilder.ToString();
    } 

    private bool IsStorable
    {
      get
      {
        //Check if the file contains any UIElements
        var res = from block in Document.Blocks
                  from inline in (block as Paragraph).Inlines
                  where inline.GetType() == typeof(InlineUIContainer)
                  select inline;

        return (res.Count() == 0); //If the file contains any UIElements, it will not be saved
      }
    }

    #region ToolbarVisible

    /// <summary>
    /// ToolbarVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty ToolbarVisibleProperty =
        DependencyProperty.Register("ToolbarVisible", typeof(bool), typeof(SilverTextEditor),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnToolbarVisibleChanged)));

    /// <summary>
    /// Gets or sets the ToolbarVisible property.
    /// </summary>
    public bool ToolbarVisible
    {
      get { return (bool)GetValue(ToolbarVisibleProperty); }
      set { SetValue(ToolbarVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the ToolbarVisible property.
    /// </summary>
    private static void OnToolbarVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      SilverTextEditor target = (SilverTextEditor)d;
      target.OnToolbarVisibleChanged((bool)e.OldValue, target.ToolbarVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsAvailable property.
    /// </summary>
    protected virtual void OnToolbarVisibleChanged(bool oldToolbarVisible, bool newToolbarVisible)
    {
      Toolbar.Visibility = (newToolbarVisible) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region Editable

    /// <summary>
    /// Editable Dependency Property
    /// </summary>
    public static readonly DependencyProperty EditableProperty =
        DependencyProperty.Register("Editable", typeof(bool), typeof(SilverTextEditor),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnEditableChanged)));

    /// <summary>
    /// Gets or sets the Editable property.
    /// </summary>
    public bool Editable
    {
      get { return (bool)GetValue(EditableProperty); }
      set { SetValue(EditableProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Editable property.
    /// </summary>
    private static void OnEditableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      SilverTextEditor target = (SilverTextEditor)d;
      target.OnEditableChanged((bool)e.OldValue, target.Editable);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsAvailable property.
    /// </summary>
    protected virtual void OnEditableChanged(bool oldEditable, bool newEditable)
    {
      rtb.IsReadOnly = !newEditable;
      ReturnFocus();
    }

    #endregion

    #region RTL

    /// <summary>
    /// RTL Dependency Property
    /// </summary>
    public static readonly DependencyProperty RTLProperty =
        DependencyProperty.Register("RTL", typeof(bool), typeof(SilverTextEditor),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnRTLChanged)));

    /// <summary>
    /// Gets or sets the RTL property.
    /// </summary>
    public bool RTL
    {
      get { return (bool)GetValue(RTLProperty); }
      set { SetValue(RTLProperty, value); }
    }

    /// <summary>
    /// Handles changes to the RTL property.
    /// </summary>
    private static void OnRTLChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      SilverTextEditor target = (SilverTextEditor)d;
      target.OnRTLChanged((bool)e.OldValue, target.RTL);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsAvailable property.
    /// </summary>
    protected virtual void OnRTLChanged(bool oldRTL, bool newRTL)
    {
      RTBGrid.FlowDirection = (newRTL) ? System.Windows.FlowDirection.RightToLeft : System.Windows.FlowDirection.LeftToRight; //not using ApplicationBorder anymore, don't want to flip the toolbar direction too

      //Set the button image based on the state of the toggle button. 
      btnRTL.Content = new Uri(newRTL ? "/SilverTextEditor;component/Images/RTL.png" : "/SilverTextEditor;component/Images/LTR.png", UriKind.RelativeOrAbsolute).CreateImage();

      ReturnFocus();
    }

    #endregion

    #endregion

    #region --- Methods ---

    public void Clear(bool confirm=false)
    {
      if (!confirm || MessageBox.Show("Clear contents?", "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK) //TODO: find parent window //TODO: localize
        Document.Blocks.Clear();
    }
    
    #region Selection

    public TextSelection Selection
    {
      get { return (rtb != null)? rtb.Selection : null; }
    }

    public bool HasSelection {
      get {
        TextSelection selection = Selection;
        return (selection != null)? (selection.Text.Length > 0) : false;
      }
    }

    public void SelectAll()
    {
      rtb.SelectAll();
    }

    private Run CurrentRun
    {
      get
      {
        TextSelection selection = Selection;
        if (selection == null)
          return null;

        TextPointer startpos = selection.Start;
        if (startpos == null)
          return null;

        return startpos.Parent as Run; //if text is empty this will return null (since the Parent is the RichTextBox in that case, which can't be cast to Run)
      }
    }

    public void SelectNone()
    {
      Selection.Select(Selection.Start, Selection.Start);
    }

    public bool SelectAllIfNone()
    {
      if (Selection.Text.Length == 0)
      { //if no selection then select all
        SelectAll();
        return true; //there was no selection, selected all
      }
      else
        return false; //there was some selection
    }

    private object GetSelectionProperty(DependencyProperty prop)
    {
      return Selection.GetPropertyValue(prop);
    }

    private void ChangeSelectionProperty(DependencyProperty prop, object value)
    {
      Selection.ApplyPropertyValue(prop, value);
    }

    private void ToggleSelectionProperty(DependencyProperty prop, object newValue, object toggleValue) 
    {
      object value = GetSelectionProperty(prop);
      ChangeSelectionProperty(prop, ((value != null) && value.Equals(newValue)) ? toggleValue : newValue); //if current property value (of the whole selection) equals new value will set the toggleValue
    }
    
    #endregion

    #region Load

    public void LoadXamlResource(string resourcePath) //doesn't close stream
    {
      LoadXaml(Application.GetResourceStream(new Uri(resourcePath, UriKind.Relative)).Stream);
    }

    public void LoadFile()
    {
      try
      {
        OpenFileDialog ofd = new OpenFileDialog()
        {
          //Multiselect = false, //this is false by default so no need to set it
          Filter = LOAD_FILTER
        };

        if (ofd.ShowDialog() == true)
          Load(ofd.File);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Loading failed: " + ex.Message); //TODO: should find parent window //TODO: localize
        //TODO: maybe should wrap the original exception as inner exception and throw a new one
      }
    }

    public void Load(FileInfo file, bool clearFirst = true)
    {
      if (file == null) return;

      string ext = file.Extension;

      using (Stream stream = file.OpenRead())
        Load(stream, ext, clearFirst);
     }

    public void Load(Stream stream, string filename, bool clearFirst = true)
    {
        if (filename.EndsWith(".text", StringComparison.OrdinalIgnoreCase))
          LoadXaml(stream, clearFirst);

        else if (filename.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
          LoadTxt(stream, clearFirst);

        else if (filename.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
          LoadDocx(stream, clearFirst);

        else
          LoadTxt(stream, clearFirst);
    }

    public void LoadXaml(Stream stream, bool clearFirst = true) //doesn't close stream
    {
      string xamlText = "";
      using (StreamReader reader = new StreamReader(stream))
      xamlText = reader.ReadToEnd();
      
      if (clearFirst) SelectAll(); //need to use this instead of Clear() since we set "Selection.Xaml" below

      if (!String.IsNullOrWhiteSpace(xamlText))
        Selection.Xaml = xamlText;
      else
        Selection.Text = ""; //Xaml property of Selection object doesn't accept empty string (need to set this in order to clear current selection if using clearFirst=false)

      if (clearFirst) ScrollToStart();
    }

    public void LoadTxt(Stream stream, bool clearFirst = true)
    {
      string txt = "";
      using (StreamReader reader = new StreamReader(stream))
        txt = reader.ReadToEnd();

      if (clearFirst) SelectAll(); //need to use this instead of Clear() since we set "Selection.Text" below

      Selection.Text = txt;

      if (clearFirst) ScrollToStart();
    }

    public void LoadDocx(Stream stream, bool clearFirst = true)
    {
      StreamResourceInfo zipInfo = new StreamResourceInfo(stream, null);
      StreamResourceInfo wordInfo = Application.GetResourceStream(zipInfo, new Uri("word/document.xml", UriKind.Relative));

      string contents = "";
      using (StreamReader reader = new StreamReader(wordInfo.Stream))
        contents = reader.ReadToEnd();

      XDocument xmlFile = XDocument.Parse(contents);
      XNamespace w = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

      var query = from xp in xmlFile.Descendants(w + "p")
                  select xp;

      if (clearFirst) Clear();

      foreach (XElement xp in query)
      {
        Paragraph p = new Paragraph();
        var query2 = from xr in xp.Descendants(w + "r")
                     select xr;
        foreach (XElement xr in query2)
        {
          Run r = new Run();
          var query3 = from xt in xr.Descendants()
                       select xt;
          foreach (XElement xt in query3)
            if (xt.Name == (w + "t"))
              r.Text = xt.Value.ToString();
            else if (xt.Name == (w + "br"))
              p.Inlines.Add(new LineBreak());

          p.Inlines.Add(r);
        }

        p.Inlines.Add(new LineBreak());
        Document.Blocks.Add(p);
      }

      if (clearFirst) ScrollToStart();
    }

    #endregion

    #region Save

    public void SaveFile()
    {
      try
      {
        SaveFileDialog sfd = new SaveFileDialog()
        {
          Filter = SAVE_FILTER,
          FilterIndex = 1, //1-based index, not 0-based //do not set this if DefaultExt is used
          //DefaultFileName = "Text", //Silverlight will prompt "Do you want to save Text?" if we set this, but the prompt can go under the main window, so avoid it
          DefaultExt = ".text" //don't set FilterIndex if this is set
        };

        if (sfd.ShowDialog() == true)
          using (Stream stream = sfd.OpenFile())
            Save(stream, sfd.SafeFileName);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Text save failed: " + ex.Message); //TODO: should find parent window //TODO: localize
      }
    }

    public void Save(Stream stream, string filename)
    {
      if (filename.EndsWith(".text", StringComparison.OrdinalIgnoreCase))
        SaveXaml(stream);

      else if (filename.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
        SaveTxt(stream);
      
      else
        SaveTxt(stream);
    }

    public void SaveXaml(Stream stream) //doesn't close stream (expecting to be called via "using" or to close explicitly)
    {
      if (!IsStorable) //If the file contains any UIElements, it will not be saved
        throw new Exception("Saving documents with UIElements is not supported");

      bool didSelectAll = SelectAllIfNone(); //if no selection then select all...

      TextWriter writer = new StreamWriter(stream, Encoding.UTF8);
      writer.Write(Selection.Xaml); //saves current selection
      writer.Flush();

      if (didSelectAll) SelectNone(); //...deselect all if there was no selection before
    }

    public void SaveTxt(Stream stream) //doesn't close stream (expecting to be called via "using" or to close explicitly)
    {
      bool didSelectAll = SelectAllIfNone(); //if no selection then select all...

      TextWriter writer = new StreamWriter(stream, Encoding.UTF8);
      writer.Write(Selection.Text); //saves current selection
      writer.Flush();

      if (didSelectAll) SelectNone(); //...deselect all if there was no selection before
    }

    #endregion

    #region Focus

    private void ReturnFocus()
    {
      if (rtb != null)
        rtb.Focus();
      }

    #endregion

    #endregion

    #region --- Events ---

    //Clears all content
    private void btnClear_Click(object sender, RoutedEventArgs e)
    {
      Clear(confirm: true);
    }

    //Opens an existing file
    private void btnOpen_Click(object sender, RoutedEventArgs e)
    {
      LoadFile();
    }

    //Saves the existing file
    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
      SaveFile();
    }

    #region Drag & Drop

    private void rtb_DragEnter(object sender, DragEventArgs e)
    {
      e.Handled = true;
      VisualStateManager.GoToState(this, "DragOver", true);
    }
    
    private void rtb_DragOver(object sender, System.Windows.DragEventArgs e)
    {
      e.Handled = true;
      //NOP
    }

    private void rtb_DragLeave(object sender, DragEventArgs e)
    {
      e.Handled = true;
      VisualStateManager.GoToState(this, "Normal", true);
    }

    private void rtb_Drop(object sender, DragEventArgs e)
    {
      VisualStateManager.GoToState(this, "Normal", true);

      //the Drop event passes in an array of FileInfo objects for the list of files that were selected and drag-dropped onto the RichTextBox.
      if (e.Data == null)
      {
        ReturnFocus();
        return;
      }

      IDataObject f = e.Data as IDataObject;
      if (f != null) //checks if the dropped objects are files
      {
        object data = f.GetData(DataFormats.FileDrop); //Silverlight 5 only supports FileDrop - GetData returns null if format is not supported
        FileInfo[] files = data as FileInfo[];

        if (files != null && files.Length > 0)
        {
          //TODO: instead of hardcoding which file extensions to ignore, should have this as property of the control (a ; separated string or an array)
          if (files[0].Name.EndsWith(new string[] { ".clipflair", ".clipflair.zip" }, StringComparison.OrdinalIgnoreCase))
            return; 

          e.Handled = true;

          //Walk through the array of FileInfo objects of the selected and drag-dropped files and parse the .txt and .docx files 
          //and insert their content in the RichTextBox.
          try
          {
            foreach (FileInfo file in files)
              Load(file, false);
          }
          catch (Exception ex)
          {
            MessageBox.Show("Loading failed: " + ex.Message); //TODO: find the parent window
            //TODO: maybe should wrap the original exception as inner exception and throw a new one
          }
        }
      }

      ReturnFocus();
    }

    #endregion

    #endregion

  }
}
