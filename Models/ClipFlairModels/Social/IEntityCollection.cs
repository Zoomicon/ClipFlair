//Version: 20120520

using System.Collections.Generic;

namespace ClipFlair.Social
{

    public interface IEntityCollection :
      ICollection<IEntity>,
      IEntity //an entity collection is itself an entity (a node on the relations graph)
    {

    }

}
