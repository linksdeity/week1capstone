using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Week1Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Welcome to the Pig Latin Translator, would you like to translate?\n\n'y' to continue, ANYTHING ELSE to exit: ");
                
            bool quit = false;
            char check = Console.ReadKey(true).KeyChar;
            int caseStorer = 0;

            if(check != 'y' && check != 'Y')
            {
                quit = true;
            }

            if (quit == false)
            {
                while (true)
                {
                    //ask the user for line or word
                    Console.Write("\n\nPlease enter a line to translate: ");
                    string userInput = Console.ReadLine();
                                                                          
                    //added so we can scan whole lines instead of words
                    string[] words = userInput.Split(' '); 
                    string pigLatin = "";

                    //take each string from the array and run it through the check and conversion
                    foreach (string word in words)
                    {
                        //store the case type to apply after changing to Pig Latin
                        //0 = lower
                        //1 = upper
                        //2 = title
                        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                        if (word.Equals(word.ToLower()))
                        {
                            caseStorer = 0;
                        }
                        else if (word.Equals(word.ToUpper()))
                        {
                            caseStorer = 1;
                        }
                        else if (word.Equals(textInfo.ToTitleCase(word)))
                        {
                            caseStorer = 2;
                        }

                        string lowerWord = word.ToLower();
                        pigLatin = pigLatin + ReturnWord(lowerWord, caseStorer) + " ";
                    } 

                    Console.Clear();
                    Console.WriteLine("Here is the line \"{0}\" in Pig Latin...\n", userInput);
                    Console.WriteLine(pigLatin);

                    Console.Write("\nWould you like to try again?\n'y' for yes, ANYTHING ELSE to quit: ");
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

            Console.WriteLine("\n\n>>>>>  Goodbye!!!  <<<<<");
            Console.ReadKey();
        }


        static string ReturnWord(string userInput, int caseStorer)
        {
            
            //place to store the completed phrase or word
            string pigLatin = "";

            //store list of vowels
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

            //break the word into an array
            char[] letters = userInput.ToCharArray();

            //save each letter in the word until a non-vowel is found
            StringBuilder saver = new StringBuilder();
            int counter = 0;
            
            //adding some code to save punctuation and add it back at the end, after updating the word
            string punctuation = "";
            bool hasPunct = false;

            if (Regex.IsMatch(letters[userInput.Length - 1].ToString(), @"^[!.?,]$"))
            {
                hasPunct = true;
                punctuation = letters[userInput.Length - 1].ToString();
                letters[userInput.Length - 1] = ' ';

                //If we stored punctuation, we must now patch out the white space we added by removing it
                string patchWord = "";

                foreach(char letter in letters)
                {
                    patchWord += letter.ToString();
                }  
                
                patchWord = Regex.Replace(patchWord, @" ", "");
                letters = patchWord.ToCharArray();
            }

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

            //this can be a seperate method - pass counter info, newstring, letters & return word with correct pig latin
            //if starts with vowel add 'way', otherwise append consonants and add 'ay' --------------------------------------------------------+++
            if (counter == 0)
            {
                if (hasPunct == false)
                {
                    string newString = new string(letters);
                    newString = newString.Trim();

                    pigLatin = CapsChecker(newString + "way", caseStorer);
                }
                else
                {
                    string newString = new string(letters);
                    newString = newString.Trim();

                    pigLatin = CapsChecker(newString + "way", caseStorer) + punctuation;
                }
            }
            else
            {
                if (hasPunct == false)
                {
                    string newString = new string(letters);
                    newString = newString.Trim();
                    newString = newString + saver.ToString() + "ay";

                    pigLatin = CapsChecker(newString, caseStorer);
                }
                else
                {
                    string newString = new string(letters);
                    newString = newString.Trim();
                    newString = newString + saver.ToString() + "ay" + punctuation;

                    pigLatin = CapsChecker(newString, caseStorer);
                }
            }//---------------------------------------------------------------------------------------------------------------------------------+++
            
            return pigLatin;
        }

        //new method so we can save UPPER, lower, or Title ase - and apply it
        //handles the three types laid out in the rubric, but treats random uPpEr and LoWEr as Title Case
        static string CapsChecker(string word, int capsType)
        {
            // takes an int to represent the caps type and piglatin word, applies the caps type and returns the piglatin word
            //0 = lower case
            //1 = all caps
            //2 = title case

            string fixedCap = "";
            //this line to help with converting strings to title case 
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            switch (capsType)
            { 
                case 0:
                    // all lower case
                    fixedCap = word.ToLower();
                    break;
                case 1:
                    // all caps
                    fixedCap = word.ToUpper();
                    break;
                case 2:
                    // title case
                    fixedCap = textInfo.ToTitleCase(word);
                    break;
                default:
                    fixedCap = word;
                    break;
            }
            return fixedCap;
        }
    }
}
