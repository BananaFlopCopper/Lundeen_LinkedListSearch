using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lundeen_LinkedListSearch
{
    class LetterIndex
    {
        
        public LetterIndex(char letter)
        { Letter = letter; }

        public readonly char Letter;
        public Node Start { get; set; }
        public Node End { get; set; }
        // Total count of all names starting with this letter
        public int Count { get; set; }
    }
}
