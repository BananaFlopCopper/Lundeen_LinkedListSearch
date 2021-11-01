using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lundeen_LinkedListSearch
{
    class MetaData : IComparable<MetaData>
    {
        public MetaData(char gender, string name, int rank)
        { 
            if(gender >= 'a')
            { gender = (char)((int)gender - 32); }
            Gender = gender;
            //ensure name is a proper noun
            name = name.Trim();
            name = name.ToUpper();
            name = name[0] + name[1..name.Length].ToLower();

            Name = name;
            Rank = rank;
        }
        public readonly char Gender;
        public string Name;
        public readonly int Rank;

        
        public int CompareTo(MetaData person)// too many if statements
        { // is in this order to fix grouping by name, then gender, then finally reputation
            string ThisName = this.Name;
            string OtherName = person.Name;
            // suffix would otherwise make compare to worthless on duplicates
            if (ThisName.Contains("_"))
            {
                int temp = ThisName.IndexOf("_");
                ThisName = ThisName.Remove(temp, ThisName.Length-temp);
            }
            if (OtherName.Contains("_"))
            {
                int temp = OtherName.IndexOf("_");
                OtherName = OtherName.Remove(temp, ThisName.Length - temp);
            }
            // Check every character until it doesn't match
            if (OtherName != ThisName)
            {
                int min = Math.Min(OtherName.Length, ThisName.Length);
                for (int i = 0; i < min; i++)
                {
                    if (OtherName[i] > ThisName[i])
                    { return -1; }
                    if (OtherName[i] < ThisName[i])
                    { return 1; }
                }
                if (this.Name.Length < person.Name.Length)
                { return -1; }
                if (this.Name.Length > person.Name.Length)
                { return 1; }
            }

            //gender check
            if (this.Gender > person.Gender)
            { return 1; }
            if (this.Gender < person.Gender)
            { return -1; }

            // least likely to occur, so end of if statement fortress is the best for performance
            return 0;
        }

    }
}
