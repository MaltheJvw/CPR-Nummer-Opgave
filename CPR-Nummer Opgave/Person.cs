using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPR_Nummer_Opgave
{
    internal class Person
    {
        //Enum som inholder køn
        public enum GenderType
        {
            Male,
            Female
        }

        //Field
        private string _name;
        private DateTime _birth;
        private int _cpr;

        //En tom ocnstructor, så jeg kan give argumenter senere.
        public Person() { }

        //En constructor med parametere
        public Person(string name, DateTime birth, int cpr, GenderType type) 
        {
            _name = name;
            _birth = birth;
            _cpr = cpr;
            Type = type;
        }

        //Properties som hjælper med skriveadgang og beskyttelse.
        public GenderType Type { get; private set; }

        public string Name {  get { return _name; } }
        
        public DateTime Birth { get { return _birth; } }

        public int Cpr { get { return _cpr;} }

        //En metode der opretter navn 
        public string MakeName()
        {
            Console.Write("Name: ");
            _name = Console.ReadLine();
            return _name;
            
            
        }

        //En metode der tilader intastningen af CPR-Nummer
        public DateTime MakeBirth()
        {
            string cprInput;
            int day, month, year;
            

            //En Do-While loop som looper når man intaster cpr nummer forkert.
            do
            {
                
                Console.Write("Enter the CPR number (ddmmyy-xxxx): ");
                cprInput = Console.ReadLine();
            } while (!ValidateCPRFormat(cprInput) || !ValidateCPRModulus11(cprInput));

            
            //cprInput tager hele dit input, og så bliver day parsed, så den får værdien i position 0 til position 2 - fx dag 05 eller dag 14.
            //Month og year, bliver parsed og får deres værdier.
            day = int.Parse(cprInput.Substring(0, 2));
            month = int.Parse(cprInput.Substring(2, 2));
            year = int.Parse(cprInput.Substring(4, 2));
            _cpr = int.Parse(cprInput.Substring(7, 4));
            

            
            if (year <= DateTime.Now.Year % 100)
            {
                year += 2000; 
            }
            else
            {
                year += 1900; 
            }

            //Opretter jeg en instance af fødselsdag, og retunerer _birth.
            _birth = new DateTime(year, month, day);

            return _birth;

        }

        //Tjekker om CPR er formateret korrekt
        private bool ValidateCPRFormat(string input)
        {
            //Hvis cpr er længere end 10 vil der være fejl
            if (input.Length != 11)
            {
                Console.WriteLine("Cpr is only 10 digits. Please enter the CPR number in the format 'ddmmyy-xxxx'.");
                return false;
            }

            //Tjekker om fødselsdatoen er tal.
            for (int i = 0; i < 6; i++)
            {
                if (!char.IsDigit(input[i]))
                {
                    Console.WriteLine("Cpr is only based on digits. Please enter the CPR number in the format 'ddmmyy-xxxx'.");
                    return false;
                }
            }
            //Tjekker for '-'. Hvis den ikke er skrevet vil der være fejl. 
            if (input[6] != '-')
            {
                Console.WriteLine("You need to use '-' between birthdate and cpr. Please enter the CPR number in the format 'ddmmyy-xxxx'.");
                return false;
            }

            //Tjekker om cpr er tal.
            for (int i = 7; i < 11; i++)
            {
                if (!char.IsDigit(input[i]))
                {
                    Console.WriteLine("Please enter the CPR number in the format 'ddmmyy-xxxx'.");
                    return false;
                }
            }
            //Hvis alt ser godt ud, så vil den retunerer true. 
            return true;
        }


        //Tjekker for modulus 11 
        public bool ValidateCPRModulus11(string cprInput)
        {
            //Laver en array med cprinput uden '-'
            //fjerner '-' så fødselsdag og cpr nummeret ikke er seperat.
            int[] cprDigits = cprInput.Replace("-", "").Select(c => int.Parse(c.ToString())).ToArray();

            //En array af vægtsum
            int[] weights = { 4, 3, 2, 7, 6, 5, 4, 3, 2, 1 };

            int sum = 0;
            //En for loop, hvor sum som starter med 0 vil få værdien af cpr nummeret * med weight. 
            //Sum vil fortsætte med at add det næste cpr * weight, til 10.
            for (int i = 0; i < 10; i++)
            {
                sum += cprDigits[i] * weights[i];
            }

            //Her skal vi finde ud af om vores sum vil blive et helt tal efter at blive divideret med 11.
            // hvis sum ikke er 0 efter udregningen betyder det at Cpr er ugyldigt.
            if (sum % 11 != 0 && sum % 11 != 11)
            {
                Console.WriteLine("Your weightsum can not be divided by 11. The cpr-number is invalid");
                return false;
            }
            return sum % 11 == 0 || sum % 11 == 11;
            
        }

        //En metode der tjekker for køn
        public GenderType GetGender()
        {

            if (Cpr % 2 == 0)
            {
                return GenderType.Female;
            }
            else
            {
                return GenderType.Male;
            }
        }

        //En metode der visser information
        public void ShowPerson()
        {
            Console.WriteLine($"Name: {_name}\nCPR-Number: {_birth}\nGender: {GetGender()}");
        }
    }
}
