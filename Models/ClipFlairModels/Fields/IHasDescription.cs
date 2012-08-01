//Version: 20120730

using ClipFlair.Models.International;

namespace ClipFlair.Models.Fields
{
  
  public interface IHasDescription
  {
      ILocalizableString Description { get; set; }
  }

}
