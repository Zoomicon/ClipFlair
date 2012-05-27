//Version: 20120520

using ClipFlair.Fields;
using ClipFlair.Social;
using ClipFlair.Social.Persons;

namespace ClipFlair.Repository
{
    public interface IAsset :
        IEntity,
        IHasDescription
    {
        IPerson Owner { get; }

    }

}
