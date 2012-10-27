//Version: 20120730

namespace ClipFlair.Models.Social.Relations
{
 
    public interface IRelation
    {
        string Verb { get; }
        IEntity Entity { get; }
    }

}
