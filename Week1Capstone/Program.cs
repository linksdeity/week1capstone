using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Week1Capstone
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Welcome to the Pig latin Translator, would you like to translate?\n\n'y' to continue, anything else to exit: ");
                
            bool quit = false;
            char check = Console.ReadKey(true).KeyChar;

            if(check != 'y' && check != 'Y')
            {
                quit = true;
            }

            if (quit == false)
            {
                while (true)
                {
                    //ask the user for  word
                    Console.Write("\n\nPlease enter a word to translate: ");
                    string userInput = Console.ReadLine();
                    userInput = userInput.ToLower();

                    string pigLatin = "";

                    //call method to return the word in pig latin, ONLY if the word meets the requirements of our other method
                    if (WordCheck(userInput))
                    {
                        pigLatin = ReturnWord(userInput);
                    }
                    else
                    {
                        continue;
                    }

                    Console.Clear();
                    Console.WriteLine("Here is word \"{0}\" in Pig Latin...\n", userInput);
                    Console.WriteLine(pigLatin);

                    Console.Write("\nWould you like to try again?\n'y' for yes, anything else to quit: ");

                    check = Console.ReadKey(true).KeyChar;

                    if (check != 'y' && check != 'Y')
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                    }


                }
            }

            Console.WriteLine("\nGood Bye!!!");
            Console.ReadLine();

        }


        static bool WordCheck(string userInput)
        {

            //check to make sure it is only text
            if (userInput.Length > 45)
            {
                Console.WriteLine("Pneumonoultramicroscopicsilicovolcanoconiosis is the longest English word published in a dictionary.\n" + 
                                  "your word cannot be longer than this. (45 letters)\n");
                return false;
            }
            else if (Regex.IsMatch(userInput, @"^[a-z]+$") == false) //scans lower case since we use ToLower using RegEx
            {
                Console.WriteLine("Please enter letters!");
                return false;
            }
            else
            {
                return true;
            }

            //returns true or false depending on word size and if it is only letters

        }

        
        static string ReturnWord(string userInput)
        {

            string pigLatin = "";

            //store list of vowels
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };


            //break the word into an array
            char[] letters = userInput.ToCharArray();


            //save each letter in the word until a non-vowel is found
            StringBuilder saver = new StringBuilder();
            int counter = 0;

            foreach(char letter in letters)
            {
                if (vowels.Contains(letter) == false)
                {
                    saver.Append(letter);
                    letters[counter] = ' ';
                }
                else
                {
                    break; 
                }

                counter++;
            }

            //if starts with vowel add 'way', otherwise append consonants and add 'ay'
            if (counter == 0)
            {
                pigLatin = userInput + "way";
            }
            else
            {
                string newString = new string(letters);
                newString = newString.Trim();
                newString = newString + saver.ToString() + "ay";

                pigLatin = newString;
            }

            return pigLatin;

        }
        


    }
}
