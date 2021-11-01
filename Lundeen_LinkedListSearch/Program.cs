using System;
using System.Collections.Generic;

namespace Lundeen_LinkedListSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Node Head = ReadFile.ReadTextFile();
            // Menu
            bool End = false;
            while(!End)
            {
                string Answers =  "1234";
                string input = " ";
                while(!Answers.Contains(input[0]))
                {
                    Console.WriteLine(
                    "1) Search by Name.\n" +
                    "2) See Statistics (total, total male/female, most popular male/female name).\n" +
                    "3) Add a new Node.\n\n" +

                    "4) Exit LinkedListSearch.\n");

                    input = Console.ReadLine();
                    if(input == "")
                    { input = " "; }
                    Console.Clear();
                }
                switch (input[0])
                {
                    // Search
                    case '1':
                        string name = "";
                        while(name == null || name == "")
                        {
                            Console.WriteLine("Enter a name to search for: ");
                            name = Console.ReadLine();
                            Console.Clear();
                            if(name.Length < 2)
                            { name = ""; }
                        }
                        List<Node> Persons = DoubleLinkedList.Search(name);
                        if(Persons != null)
                        {
                            for (int i = 0; i < Persons.Count; i++)
                            {
                                NodePrint(Persons[i]);
                            }
                            Console.ReadLine();
                            Console.Clear();
                        } else
                        {
                            Console.WriteLine("Name Could not be found.");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;
                    // Statistics
                    case '2':
                        Console.WriteLine(
                            "Total Count: "+DoubleLinkedList.Count + "\n" +
                            "Female Total Count: " + DoubleLinkedList.FemaleCount +"\n" +
                            "Male Total Count: " + DoubleLinkedList.MaleCount + "\n" +
                            "1) View Most popular male/female names.\n");
                        string choice = Console.ReadLine();
                        if(choice == "")
                        { break; }
                        if(choice[0] == '1')
                        {
                            if(DoubleLinkedList.MostPopularMaleName != null)
                            {
                                Console.WriteLine("Most Popular Male Name: \n");
                                NodePrint(DoubleLinkedList.MostPopularMaleName);
                            }
                            else
                            {
                                Console.WriteLine( "There is no most popular male name.");
                            }
                            if(DoubleLinkedList.MostPopularFemaleName != null)
                            {
                                Console.WriteLine("Most Popular Female Name: \n");
                                NodePrint(DoubleLinkedList.MostPopularFemaleName);
                            }
                            else
                            {
                                Console.WriteLine("There is no most popular female name.");
                            }
                            
                            
                            Console.ReadLine();
                            Console.Clear();
                        }
                        Console.Clear();
                        break;
                    // Add
                    case '3':
                        string NewName = "";
                        string gender = "";
                        string preRank = "";
                        int rank = -1;
                        while (NewName.Length < 2) {
                            Console.WriteLine("Enter a name: ");
                            Console.WriteLine("\n");
                            NewName = Console.ReadLine();
                            Console.Clear();
                        }
                        while (!gender.Contains('m') && !gender.Contains('f'))
                        {
                            Console.WriteLine("Enter a Gender (m/f -> Male/Female): ");
                            gender = Console.ReadLine();
                            Console.WriteLine("\n");
                            Console.Clear();
                        }
                        while (rank <= 0) {
                            Console.WriteLine("Enter a Rank: ");
                            preRank = Console.ReadLine();
                            if(preRank.Length < int.MaxValue.ToString().Length)
                            {
                                try
                                {
                                    rank = int.Parse(preRank);
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Line is not only numbers. Press any key to retry");
                                    Console.ReadLine();
                                }
                            } else
                            {
                                Console.WriteLine("Line contains too many characters. Press any key to retry");
                                Console.ReadLine();
                            }
                            
                            Console.Clear();
                        }
                        Node newNode = null;
                        string charAnswer = "";
                        while (charAnswer == "" || ((charAnswer[0] != 'y') && (charAnswer[0] != 'n'))) {
                            Console.WriteLine("Is this correct(y/n)?");
                            MetaData person = new MetaData(gender[0], NewName, rank);
                            newNode = new Node(person);
                            NodePrint(newNode);
                            charAnswer = Console.ReadLine();
                            Console.Clear();
                        }
                        if (charAnswer[0] == 'y')
                        {
                                Head = DoubleLinkedList.Insert(newNode);   
                        }
                        break;
                    // Exit
                    case '4':
                        End = true;
                        break;
                }


            }
        }
        public static void NodePrint(Node node)
        {
            string Gender = "";
            if(node.MetaData.Gender == 'F')
            {
                Gender = "Female";
            }else
            {
                Gender = "Male";
            }
            Console.WriteLine(
                "Name: " + node.MetaData.Name + "\n" +
                "Gender: " + Gender + "\n" +
                "Rank: " + node.MetaData.Rank + "\n");

        }
    }
}
