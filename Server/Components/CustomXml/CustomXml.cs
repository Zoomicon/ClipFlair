//Filename: CustomXml.cs
//Version: 20130905

//from article http://www.eggheadcafe.com/articles/20060603.asp

using System.ComponentModel;
using System.Web.UI;
using System.Xml.Xsl;

namespace PAB.WebControls
{
  [DefaultProperty("DocumentUrl")]
  [ToolboxData("<{0}:CustomXml1 runat=server></{0}:CustomXml1>")]
  public class CustomXml : System.Web.UI.WebControls.Xml
  {

    #region --- Fields ---

    private string _xslUrl;
    private string _documentUrl;

    #endregion

    #region --- Properties ---

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("")]
    [Localizable(true)]
    public string DocumentUrl
    {
      get
      {
        return _documentUrl;
      }

      set
      {
        _documentUrl = value;
        System.Xml.XPath.XPathDocument xDoc = new System.Xml.XPath.XPathDocument(_documentUrl);
        this.XPathNavigator = xDoc.CreateNavigator();
      }
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("")]
    [Localizable(true)]
    public string XslUrl
    {
      get
      {
        return _xslUrl;
      }

      set
      {
        _xslUrl = value;
        System.Xml.XPath.XPathDocument xslDoc = new System.Xml.XPath.XPathDocument(_xslUrl);
        System.Xml.XPath.XPathNavigator xslNav = xslDoc.CreateNavigator();
        XslTransform xsltran = new XslTransform();
        xsltran.Load(xslNav);
        this.Transform = xsltran;
      }
    }

    #endregion

  }
}