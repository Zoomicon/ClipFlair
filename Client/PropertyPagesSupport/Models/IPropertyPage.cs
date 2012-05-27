using System.Windows;

namespace PropertyPagesSupport
{
 
  public interface IPropertyPage
  {
    string Title { get; }
    UIElement UI { get; }
  }

}