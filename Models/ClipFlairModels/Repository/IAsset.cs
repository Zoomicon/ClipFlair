//Version: 20120730

using ClipFlair.Models.Fields;
using ClipFlair.Models.Social;
using ClipFlair.Models.Social.Persons;

namespace ClipFlair.Models.Repository
{
    public interface IAsset :
        IEntity,
        IHasDescription
    {
        IPerson Owner { get; }

    }

}
