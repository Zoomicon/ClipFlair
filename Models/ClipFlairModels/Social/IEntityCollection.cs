//Version: 20120730

using System.Collections.Generic;

namespace ClipFlair.Models.Social
{

    public interface IEntityCollection :
      ICollection<IEntity>,
      IEntity //an entity collection is itself an entity (a node on the relations graph)
    {

    }

}
