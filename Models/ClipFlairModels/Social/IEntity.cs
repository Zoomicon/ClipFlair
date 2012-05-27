//Version: 20120520

using System;
using ClipFlair.Social.Relations;

namespace ClipFlair.Social
{

    public interface IEntity
    {
        Uri uri { get; }
        IRelationCollection IncomingRelations { get; }
        IRelationCollection OutgoingRelations { get; }
    }

}
