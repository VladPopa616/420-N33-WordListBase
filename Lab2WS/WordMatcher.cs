using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Lab2WS
{
    class WordMatcher
    {
        public List<MatchedWord> Match(string[] scrambledWords, string[] wordList)
        {
            List<MatchedWord> matchedWords = new List<MatchedWord>();

            foreach (string scrambledWord in scrambledWords)
            {
                foreach (string word in wordList)
                {
                    if (scrambledWord.Equals(word, StringComparison.OrdinalIgnoreCase))
                    {
                        //matchedWords.Add(BuildMatchedWord(scrambledWord, word));

                        matchedWords.Add(new MatchedWord() { ScrambledWord = scrambledWord, Word = word });

                    }
                    else
                    {
                        char[] scrambled = scrambledWord.ToCharArray();
                        char[] words = word.ToCharArray();

                        Array.Sort(scrambled);
                        Array.Sort(words);

                        string scordered = "";

                        foreach (var chr in scrambled)
                        {
                            scordered += chr.ToString();
                        }

                        string wordered = "";

                        foreach (var chr in words)
                        {
                            wordered += chr.ToString();
                        }

                        if (scordered.Equals(wordered, StringComparison.OrdinalIgnoreCase))
                        {
                            matchedWords.Add(BuildMatchedWord(scrambledWord, word));
                        }
                    }

                }
            }




            MatchedWord BuildMatchedWord(string scrambledWord, string word)
            {
                MatchedWord matchedWord = new MatchedWord()
                {
                    ScrambledWord = scrambledWord,
                    Word = word
                };

                return matchedWord;
            }
            return matchedWords;

        }
          


    }
}
