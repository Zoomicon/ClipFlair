//Version: 20120520

using ClipFlair.Fields;

namespace ClipFlair.Social.Persons
{
  
  public interface ITeam : 
      IPersonCollection,
      IHasDescription
  {
      IPersonCollection Leaders { get; }
  }

}
