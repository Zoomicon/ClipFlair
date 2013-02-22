//from article http://www.eggheadcafe.com/articles/20060603.asp

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Xml;
using System.Xml.Xsl;

namespace PAB.WebControls 
{
    [DefaultProperty("DocumentUrl")]
    [ToolboxData("<{0}:CustomXml1 runat=server></{0}:CustomXml1>")]
    public class CustomXml : System.Web.UI.WebControls.Xml
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]       
        private string _documentUrl;
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

        private string _xslUrl;
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
    }
}