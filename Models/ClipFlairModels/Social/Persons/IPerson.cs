//Version: 20120730

using ClipFlair.Models.Social;

namespace ClipFlair.Models.Social.Persons
{
    public interface IPerson : IEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Bio { get; set; }

        IPersonCollection Friends { get; }
        IEntityCollection Likes { get; }

    }

}
