//Version: 20120730

using ClipFlair.Models.Fields;

namespace ClipFlair.Models.Social.Persons
{
  
  public interface ITeam : 
      IPersonCollection,
      IHasDescription
  {
      IPersonCollection Leaders { get; }
  }

}
