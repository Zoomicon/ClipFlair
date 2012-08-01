//Version: 20120730

using System;
using ClipFlair.Models.Social.Relations;

namespace ClipFlair.Models.Social
{

    public interface IEntity
    {
        Uri uri { get; }
        IRelationCollection IncomingRelations { get; }
        IRelationCollection OutgoingRelations { get; }
    }

}
