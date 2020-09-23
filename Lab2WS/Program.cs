using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2WS
{
    class Program
    {
        private static readonly FileReader fileReader = new FileReader();
        private static readonly WordMatcher wordMatcher = new WordMatcher();

        static void Main(string[] args)
        {

            try
            {
                bool value = true;

                
                do
                {
                    Console.WriteLine("Enter the scrambled words manually or as a file: f - file, m = manual");

                    string option = Console.ReadLine() ?? throw new Exception("String is null");
                    //option.ToUpper();

                    //string option = "";

                    // while ((!option.Equals ("M")) || (!option.Equals("F"))|| (!option.Equals("f")) || (!option.Equals("m")))
                    //    {
                    //             Console.WriteLine("Enter the scrambled words manually or as a file: f - file, m = manual");
                    //            
                    //             string option2 = Console.ReadLine().Trim() ?? throw new Exception("String is null");
                    //            option2.ToUpper();
                    //            option = option2;
                    //            Console.WriteLine(option);
                    //}

                    {
                        switch (option.ToUpper())
                        {
                            case "F":
                                Console.WriteLine("Enter the full path and filename >");
                                ExecuteScrambledWordsInFileScenario();
                                value = true;
                                break;
                            case "M":
                                Console.WriteLine("Enter word(s) separated by a comma");
                                ExecuteScrambledWordsManualEntryScenario();
                                value = true;
                                break;
                            default:
                                Console.WriteLine("The entered option was not recognized. Please try again.");
                                value = false;
                                continue;

                        }

                    }
                } while (!value);

                    Console.WriteLine("Do you want to restart? Y/N");
                    String wannaRestart = Console.ReadLine();

                    bool restart = false;
                    do { 
                    switch (wannaRestart.ToUpper())
                    {
                        case "Y":
                            restart = true;
                            value = true;
                            break;
                        case "N":
                            restart = true;
                            value = false;
                            break;
                        default:
                            Console.Write("Did not understand, can you repeat? Y/N");
                            restart = false;
                            continue;
                    }
                 }while (!restart) ;

            }

            catch (Exception e)
            {
                Console.WriteLine("Sorry an error has occurred.. " + e.Message);

            }
            


        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            string fileName = Console.ReadLine();
            string[] scrambledWords = fileReader.Read(fileName);
            DisplayMatchedScrambledWords(scrambledWords);
        }

        private static void ExecuteScrambledWordsManualEntryScenario()
        {
            // 1 get the user's input - comma separated string containing scrambled words
            // 2 Extract the words into a string (red,blue,green) 
            // 3 Call the DisplayMatchedUnscrambledWords method passing the scrambled words string array
            string listWord = Console.ReadLine();
            


            Console.WriteLine("Any more words to add? Y/N");
            string moreWords = Console.ReadLine();

            bool value = true;

            do
            {
                switch (moreWords.ToUpper())
                {
                    case "Y":
                        Console.WriteLine("Enter the words:");
                        string listWord2 = Console.ReadLine();
                        listWord = String.Join(",", listWord, listWord2);
                        value = true;
                        break;
                    case "N":
                        value = true;
                        break;
                    default:
                        Console.WriteLine("Not an option, please enter Y/N");
                        value = false;
                        continue;
                }
            } while (!value);

            string[] splitList = listWord.Split(' ', ',', '.');
            DisplayMatchedScrambledWords(splitList);
            
        }

        private static void DisplayMatchedScrambledWords(string[] scrambledWords)
        {
            string[] wordList = fileReader.Read(@"wordlist.txt"); // Put in a constants file. CAPITAL LETTERS.  READONLY.

            List<MatchedWord> matchedWords = wordMatcher.Match(scrambledWords, wordList);

            if (matchedWords == null)
            {
                Console.WriteLine(Constants.file_path_invalid);
            }

            else if (matchedWords.Any())
            {
                foreach(var mw in matchedWords)
                {
                    Console.WriteLine(Constants.matched_found, mw.ScrambledWord, mw.Word);
                }
            }
            else
            {
                Console.WriteLine(Constants.no_match_found);
            }
    
        }
    }
}
