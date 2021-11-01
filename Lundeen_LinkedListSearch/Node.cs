using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lundeen_LinkedListSearch
{
    class Node
    {
        public Node (MetaData metaData)
        { MetaData = metaData; }

        public Node Next { get; set; }
        public Node Previous { get; set; }

        public readonly MetaData MetaData;
    }
}
