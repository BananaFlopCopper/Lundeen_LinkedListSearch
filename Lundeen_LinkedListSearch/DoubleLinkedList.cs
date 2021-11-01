using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lundeen_LinkedListSearch
{
    class DoubleLinkedList
    {
        private static LetterIndex[] _alphabetIndexes = new LetterIndex[26]
        {
            new LetterIndex('a'), new LetterIndex('b'), new LetterIndex('c'), new LetterIndex('d'),
            new LetterIndex('e'), new LetterIndex('f'), new LetterIndex('g'), new LetterIndex('h'),
            new LetterIndex('i'), new LetterIndex('j'), new LetterIndex('k'), new LetterIndex('l'),
            new LetterIndex('m'), new LetterIndex('n'), new LetterIndex('o'), new LetterIndex('p'),
            new LetterIndex('q'), new LetterIndex('r'), new LetterIndex('s'), new LetterIndex('t'),
            new LetterIndex('u'), new LetterIndex('v'), new LetterIndex('w'), new LetterIndex('x'),
            new LetterIndex('y'), new LetterIndex('z')
        };
        private static Node _head;
        private static Node _tail;

        public static int Count = 0;
        public static int FemaleCount = 0;
        public static int MaleCount = 0;
        public static Node MostPopularFemaleName;
        public static Node MostPopularMaleName;
        public static bool IsReadingFromFile;
        public static void Prompt(Node NewNode, int DupeCount) {
            string input = " ";
            if (!IsReadingFromFile)
            { input = " "; }
            else 
            { input = "y"; }
            
            while (input[0] != 'y' && input[0] != 'n')
            {
                Console.Clear();
                Console.WriteLine("(y/n) add suffix _" + DupeCount + "(y) or discard new name " + NewNode.MetaData.Name + "?(n)");
                input = Console.ReadLine();
                if(input == "")
                {
                    input = " ";
                }
            }
            if (input[0] == 'y') {
                NewNode.MetaData.Name += "_" + DupeCount;
            }
            
            Count--;
        }

        // Insertion Code
        public static Node Insert(Node newNode)
        {

            if (_head == null)
            {
                _tail = newNode;// loop should be fine, since it will correct on lists larger than 1
                return Addhead(newNode); 
            }

            if (_head.MetaData.CompareTo(newNode.MetaData) == 1)
            { return Addhead(newNode); }

            else
            { 
                if(_tail == null)
                {
                    _tail = newNode;
                    return _head;
                }
                AddNode(newNode); 
            }

            return _head;
        }
        private static Node Addhead(Node newNode)
        { // if _head is null then setting next to _head won't matter
            newNode.Next = _head;

            if (_head != null)
            { _head.Previous = newNode; }
            _head = newNode;
            int index = _head.MetaData.Name[0] - 'A';
            _alphabetIndexes[index].Start = _head;
            if(_alphabetIndexes[index].End == null)
            {
                _alphabetIndexes[index].End = _head;
            }

            StatisticTracker(newNode);
            return _head;
        }

        private static void AddNode(Node newNode)
        {
            Node Current = _head;
            int Index = AlphabetCheck(newNode.MetaData.Name);
            if (Index != -1)
            {
                Node end;
                Current = _alphabetIndexes[Index].Start;
                end = _alphabetIndexes[Index].End;
                ForwardInsert(Current, newNode, _tail); 
                return;
            }
            BasicInsert(Current, newNode);
            return;
        }
        private static void ForwardInsert(Node Current, Node NewNode, Node End)
        {
            int index = NewNode.MetaData.Name[0] - 'A';

            int DupeCount = 0;
            while (Current != End)
            {

                int comp = Current.MetaData.CompareTo(NewNode.MetaData);
                switch (comp)
                {
                    case 1: // push current back and place newNode here
                        if (DupeCount != 0)
                        { Prompt(NewNode, DupeCount); }
                        StatisticTracker(NewNode);

                        Node Previous = Current.Previous;
                        Previous.Next = NewNode;
                        NewNode.Previous = Previous;
                        NewNode.Next = Current;
                        Current.Previous = NewNode;
                        ChangeAlphabetNode(NewNode);
                        
                        return;

                    case 0: // copy
                        DupeCount++;
                        Current = Current.Next;
                        break;

                    default: // continue
                        Current = Current.Next;
                        break;
                }
            }
            if (Current.Next == null)
            {
                if (DupeCount != 0)
                { Prompt(NewNode, DupeCount); }
                StatisticTracker(NewNode);

                Current.Next = NewNode;
                NewNode.Previous = Current;
                _tail = NewNode;
                ChangeAlphabetNode(NewNode);

                return;
            }
            if (DupeCount != 0)
            { Prompt(NewNode, DupeCount); }
            StatisticTracker(NewNode);

            Node Prev = Current.Previous;
            Prev.Next = NewNode;
            NewNode.Previous = Prev;
            NewNode.Next = Current;
            Current.Previous = NewNode;
            ChangeAlphabetNode(NewNode);

            return;
        }
        private static void BasicInsert(Node Current, Node NewNode)
        {
            StatisticTracker(NewNode);
            int index;
            while (Current.Next != null)
            {
                int comp = Current.MetaData.CompareTo(NewNode.MetaData);
                switch (comp)
                {
                    case 1: // push current back and place newNode here
                        Node Previous = Current.Previous;
                        Previous.Next = NewNode;
                        NewNode.Previous = Previous;
                        NewNode.Next = Current;
                        Current.Previous = NewNode;
                        //Create alphabet group
                        index = NewNode.MetaData.Name.ToLower()[0] - 'a';
                        _alphabetIndexes[index].Start = NewNode;
                        _alphabetIndexes[index].End = NewNode;

                        return;

                    default: // continue, no duplicates to count since alphabet group does not exist
                        Current = Current.Next;
                        break;
                }
            }
            Current.Next = NewNode;
            NewNode.Previous = Current;
            _tail = NewNode;
            //Create alphabet group
            index = NewNode.MetaData.Name.ToLower()[0] - 'a';
            _alphabetIndexes[index].Start = NewNode;
            _alphabetIndexes[index].End = NewNode;
            return;
        }
        
        // Helpers (technically searches)
        private static void StatisticTracker(Node newNode)
        {
            Count++;
            if (newNode.MetaData.Gender == 'F')
            {
                FemaleCount++;
                if (MostPopularFemaleName == null || MostPopularFemaleName.MetaData.Rank < newNode.MetaData.Rank)
                {
                    MostPopularFemaleName = newNode;
                }
            }
            else
            {
                MaleCount++;
                if (MostPopularMaleName == null || MostPopularMaleName.MetaData.Rank < newNode.MetaData.Rank)
                {
                    MostPopularMaleName = newNode;
                }
            }
        }
        private static void ChangeAlphabetNode(Node NewNode)
        {
            int index = NewNode.MetaData.Name[0] - 'A';
            if (NewNode.Previous == _alphabetIndexes[index].End)
            {
                _alphabetIndexes[index].End = NewNode;
            }
            if (NewNode.Next == _alphabetIndexes[index].Start)
            {
                _alphabetIndexes[index].Start = NewNode;
            }
        }
        private static int AlphabetCheck(string name)
        {
            int index = name.ToLower()[0] - 'a';
            if(_alphabetIndexes[index].Start != null)
            { return index; }

            return -1;
        }
        

        // Search Code (not technically)
        public static List<Node> Search(string name)
        {
            List<Node> nodes = new List<Node>();
            int index = AlphabetCheck(name);
            if (index != -1) {

                // determine front or back entrance
                if (name.ToLower()[1] < 'm')
                { // start from front
                    Node Current = _alphabetIndexes[index].Start;
                    while (Current != _alphabetIndexes[index].End)
                    {
                        if(Current.MetaData.Name.ToLower() == name.ToLower())
                        {
                            nodes.Add(Current);
                        }
                        Current = Current.Next;
                    }
                }

                else
                {// start from back
                    Node Current = _alphabetIndexes[index].End;
                    while (Current != _alphabetIndexes[index].Start)
                    {
                        if (Current.MetaData.Name.ToLower() == name.ToLower())
                        {
                            nodes.Add(Current);
                        }
                        Current = Current.Previous;
                    }
                }
            }
            return nodes;
            //// if there are no matches
            //return null;
        }
    }
}
