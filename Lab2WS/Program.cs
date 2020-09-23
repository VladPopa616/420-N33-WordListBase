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
                    Console.WriteLine(Constants.select_file_manual);

                    string option = Console.ReadLine() ?? throw new Exception(Constants.string_null);
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
                                Console.WriteLine(Constants.file_path_entry);
                                ExecuteScrambledWordsInFileScenario();
                                value = true;
                                break;
                            case "M":
                                Console.WriteLine(Constants.manual_entry);
                                ExecuteScrambledWordsManualEntryScenario();
                                value = true;
                                break;
                            default:
                                Console.WriteLine(Constants.repeat_option);
                                value = false;
                                continue;

                        }

                    }
                

                    bool restart = false;
                    do { 
                    Console.WriteLine(Constants.restart_msg);
                    String wannaRestart = Console.ReadLine();
                    switch (wannaRestart.ToUpper())
                    {
                        case "Y":
                        case "YES":
                            restart = true;
                            value = true;
                            break;
                        case "N":
                        case "NO":
                            restart = true;
                            value = false;
                            break;
                        default:
                            Console.WriteLine(Constants.repeat_option);
                            restart = false;
                            continue;
                    }
                 }while (!restart);

                } while (!value);
            }

            catch (Exception e)
            {
                Console.WriteLine(Constants.error_msg + e.Message);

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
            Console.WriteLine(Constants.enter_words);
            string listWord = Console.ReadLine();



            bool value = true;

            do
            { 
                Console.WriteLine(Constants.add_words);
                string moreWords = Console.ReadLine();
                switch (moreWords.ToUpper())
                {
                    case "Y":
                    case "YES":
                        Console.WriteLine(Constants.enter_words);
                        string listWord2 = Console.ReadLine();
                        listWord = String.Join(",", listWord, listWord2);
                        value = true;
                        break;
                    case "N":
                    case "NO":
                        value = true;
                        break;
                    default:
                        Console.WriteLine(Constants.repeat_option);
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
