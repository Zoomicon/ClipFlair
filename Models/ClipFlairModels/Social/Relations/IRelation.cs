//Version: 20120520

namespace ClipFlair.Social.Relations
{
 
    public interface IRelation
    {
        string Verb { get; }
        IEntity Entity { get; }
    }

}
