using System;

namespace CPR_Nummer_Opgave
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //opretter en instans af klassen Person og tildeler den til variablen obj.
            Person obj = new Person();

            while (true)
            {
                

                Console.Clear();
                char option = DisplayMenu();

                switch (char.ToUpper(option))
                {
                    case 'S':
                        //Her kalder jeg alle mine metoder inde i Person klassen.
                        Console.Clear();
                        obj.MakeName();
                        Console.Clear();
                        obj.MakeBirth();
                        Console.Clear();
                        obj.GetGender();
                        Console.Clear();
                        obj.ShowPerson();

                        Console.WriteLine("\n\n\npress any key to restart");
                        Console.ReadKey();
                        break;

                    case 'A':
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Ukendt kommando..");
                        break;
                }
            }
        }

        static private char DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("CPR-Number Identification\n");
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("S for Start program");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("A for at afslutte");
            Console.ResetColor();

            ConsoleKeyInfo key = Console.ReadKey(true);
            return key.KeyChar;
        }

    }
}
