//Version: 20120520

using ClipFlair.Social;

namespace ClipFlair.Social.Persons
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
