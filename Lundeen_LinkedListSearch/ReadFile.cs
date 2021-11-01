using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Lundeen_LinkedListSearch
{
   static class ReadFile
    {
        // .Replace is here to ensure that the binary will look for the file in the sourcecode folders instead of the bin
        public static readonly string fileLocation = 
            (Environment.CurrentDirectory + 
            "\\TextSourceFolder\\yob2019.txt")
            .Replace("bin\\Debug\\net5.0\\", "");

        private static StreamReader read = new StreamReader(fileLocation);

        public static Node ReadLine()
        {   
            if(!read.EndOfStream)
            {
                string textFileInput = read.ReadLine();
                return ParsePerson(textFileInput);
            }
            return null;
        }

        public static Node ReadTextFile()
        {
            DoubleLinkedList.IsReadingFromFile = true;
            Node Head = null;
            while (!read.EndOfStream)
            {
                
                string textFileInput = read.ReadLine();
                Head = DoubleLinkedList.Insert(
                    ParsePerson(textFileInput)
                    );
            }
            DoubleLinkedList.IsReadingFromFile = false;
            return Head;
        }

        private static Node ParsePerson(string textFileInput)
        {
            // break text line into person constants
            string Name = textFileInput[0..(textFileInput.IndexOf(','))];
            textFileInput = textFileInput.Remove(0, textFileInput.IndexOf(',') + 1);

            char Gender = textFileInput[0];
            textFileInput = textFileInput.Remove(0, 2);

            int rank = int.Parse(textFileInput);

            MetaData P = new MetaData(Gender, Name, rank);
            Node N = new Node(P);
            return N;
        }

    }
}
