// File TypeTokenTooks by Joe McFall, July 2007
// Modifications by M. Covington subsequently

using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;

namespace LexicalDiversity   // was TTRLIB
{
    /// <summary>
    /// This class provides tools for finding type token
    /// ratio and moving-average type-token ratio of a list of
    /// tokenized words.
    /// </summary>
    public static class TypeTokenTools
    {
        /// <summary>
        /// Tokenizes a string.
        /// </summary>
        /// <param name="text">A string to tokenize</param>
        /// <returns>A List of tokens</string></returns>
        public static List<string> Tokenize(string text)
        {
            text = text.ToLower();
            //separate out punctuation
            text = text.Replace(".", " . ");
            text = text.Replace(",", " , ");
            text = text.Replace(":", " : ");
            text = text.Replace("(", " ( ");
            text = text.Replace(")", " ) ");
            text = text.Replace("\"", " \" ");
            text = text.Replace("'", " ' ");
            text = text.Replace("[", " [ ");
            text = text.Replace(";", " ; ");
            text = text.Replace("]", " ] ");
            text = text.Replace("{", " } ");
            text = text.Replace("--", " -- ");
            text = text.Replace("-", " - ");   // added by Covington
            text = text.Replace("?", " ? ");
            text = text.Replace("!", " ! ");
            text = text.Replace("$", " $ ");
            text = text.Replace("/", " / ");


            //separate out contractions
            // Changed by Covington so that contractions, except for 's and 'll, go back together as single words.
            // This is to match MontyTagger.
            // This will still differ from CPIDR word counts because CPIDR does not count 's as a word even though it is a token.

            text = text.Replace("' ll", "will");

            text = text.Replace("n ' t", "n't");
            text = text.Replace(" ' re ", "'re ");
            text = text.Replace("i ' m", "i'm");
            text = text.Replace(" ' d", " 'd");
            text = text.Replace(" ' ve", "'ve");

            //and possessives
            //text = text.Replace(" ' s ", " 's ");

            // text = text.ToLower();

            //split at whitespace
            char[] splitter = new char[] { ' ', '\t', '\n', '\r' };
            string[] splitText = text.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            List<string> tokens = new List<string>(splitText);
            return tokens;
        }


        /// <summary>
        /// Copies a list of strings, removing the elements that do not appear to be words.
        /// Words are assumed to have alphabetic first or second character.
        /// </summary>
        public static List<string> RemoveNonWords(List<string> tokens)
          // Added by M. Covington
        {
          List<string> r = new List<string>(tokens.Count);
          foreach (string s in tokens)
          {
            if ((s.Length >= 1 && Char.IsLetterOrDigit(s[0]))
                 || (s.Length >= 2 && Char.IsLetterOrDigit(s[1]))
               )
            {
              r.Add(s);
            }
          }
          return r;
        }


        /// <summary>
        /// Takes a list of tokens and returns a frequency table
        /// with the count of each token, sorted alphabetically
        /// </summary>
        /// <param name="tokens">An array of string tokens</param>
        /// <returns>A Dictionary representing a word frequency table</returns>
        public static Dictionary<string, int> WordFrequency(List<string> tokens)
        {
            Dictionary<string, int> sortedFreqTable = new Dictionary<string, int>();
            foreach (string word in tokens)
            {
                if (sortedFreqTable.ContainsKey(word))
                {
                    sortedFreqTable[word]++;
                }
                else
                {
                    sortedFreqTable.Add(word, 1);
                }
            }

            //put all sorted values into a regular dictionary
            Dictionary<string, int> freqTable = new Dictionary<string, int>(sortedFreqTable);

            return freqTable;
        }

        /// <summary>
        /// Takes a list of tokens and returns a frequency table
        /// with the count of each token, sorted alphabetically
        /// </summary>
        /// <param name="tokens">An array of string tokens</param>
        /// <returns>A Dictionary representing a word frequency table</returns>
        public static Dictionary<string, int> WordFrequency(string[] tokens)
          // Same as the foregoing but takes an array as arg.
        {
            List<string> tokensList = new List<string>(tokens);
            return WordFrequency(tokensList);
        }

        /// <summary>
        /// Calculates Type/Token ratio: Number of types divided by number of tokens.
        /// </summary>
        /// <param name="words">A list of tokens</param>
        /// <returns>A double representing the TTR</returns>
        public static double TypeTokenRatio(List<string> words)
        {
            //first get the frequency table
            Dictionary<string, int> freqs = WordFrequency(words);

            //divide the number of words by the number of dictionary entries
            return (double)freqs.Count / words.Count;
        }


        /// <summary>
        /// Calculates Type/Token ratio: Number of types divided by number of tokens.
        /// </summary>
        /// <param name="words">An array of tokens</param>
        /// <returns>A double representing the TTR</returns>
        public static double TypeTokenRatio(string[] words)
        {
            List<string> wordList = new List<string>(words);
            //first get the frequency table
            Dictionary<string, int> freqs = WordFrequency(wordList);

            //divide the number of words by the number of dictionary entries
            return (double)freqs.Count / words.Length;
        }




        /// <summary>
        /// Calculates Several Varaince of TTR: Number of types divided by number of tokens.
        /// </summary>
        /// <param name="wordsarray">An array of tokens</param>
        /// <returns>A dictionary of different TTR types</returns>
        public static Dictionary<string, double> TypeTokenRatioMetrics(string [] wordsarray)
        {

            Dictionary<string, double> ttrMetrics = new Dictionary<string, double>();

            List<string> wordList = new List<string>(wordsarray);
            //first get the frequency table
            Dictionary<string, int> freqs = WordFrequency(wordList);


            int terms = freqs.Count;
            int words = wordsarray.Length;


            //http://quanteda.io/reference/textstat_lexdiv.html
            ttrMetrics.Add("Tokens", (double)words);
            ttrMetrics.Add("Types", (double)terms);
            ttrMetrics.Add("TTR", (double)terms / words);
            ttrMetrics.Add("RTTR", terms / Math.Sqrt((double)words));
            ttrMetrics.Add("CTTR", terms / Math.Sqrt(2.0 * (double)words));
            ttrMetrics.Add("HerdanC", Math.Log(terms) / Math.Log(words));
            ttrMetrics.Add("SummerIndex", Math.Log(Math.Log(terms)) / Math.Log(Math.Log(words)));
            ttrMetrics.Add("Dugast", Math.Log(Math.Pow(words, 2)) / (Math.Log(words) - Math.Log(terms) + .0000001));
            ttrMetrics.Add("Maas", (Math.Log(words) - Math.Log(terms)) / Math.Pow(Math.Log(words), 2));
            ttrMetrics.Add("Evenness", StdDev(freqs.Values.AsEnumerable(), true));



            return ttrMetrics;

        }





        private static double StdDev(this IEnumerable<int> values,
            bool as_sample)
        {
            // Get the mean.
            double mean = values.Sum() / values.Count();

            // Get the sum of the squares of the differences
            // between the values and the mean.
            var squares_query =
                from int value in values
                select (value - mean) * (value - mean);
            double sum_of_squares = squares_query.Sum();

            if (as_sample)
            {
                return Math.Sqrt(sum_of_squares / (values.Count() - 1));
            }
            else
            {
                return Math.Sqrt(sum_of_squares / values.Count());
            }
        }




        public static double CalcMTLDFrontBack(string[] wordsarray, double threshold = 0.72)
        {
            //calculate the average of doing it forward and backward
            double MLTDforward = CalcMTLD(wordsarray, threshold: threshold);
            Array.Reverse(wordsarray);
            double MLTDbackward = CalcMTLD(wordsarray, threshold: threshold);
            return (MLTDforward + MLTDforward) / 2.0;
        }




        private static double CalcMTLD(string[] wordsarray, double threshold)
        {

            HashSet<string> terms = new HashSet<string>();
            ulong word_counter = 0;
            double term_count = 0;
            double factor_count = 0;
            double ttr = 0.0;

            for (int i = 0; i < wordsarray.Length; i++)
            {
                word_counter++;
                if (!terms.Contains(wordsarray[i]))
                {
                    terms.Add(wordsarray[i]);
                    term_count++;
                }

                ttr = term_count / word_counter;

                if (ttr <= threshold)
                {
                    word_counter = 0;
                    term_count = 0;
                    terms = new HashSet<string>();
                    factor_count++;
                }
            }

            // calculate factor fraction for the last leg as the ratio of how far away ttr is from
            // unit, to how far away threshold is to unit
            if (word_counter > 0) factor_count += (1.0 - ttr) / (1 - threshold);

            //what to do if ttr stayed above threshold the entire time
            if (factor_count == 0)
            {
                ttr = term_count / wordsarray.Length;
                if (ttr == 1)
                {
                    factor_count++;
                }
                else
                {
                    factor_count += (1.0 - ttr) / (1 - threshold);
                }


            }

            return (wordsarray.Length / factor_count);
        }







        /// <summary>
        /// Array in which the TTRs of individual windows are stored for
        /// subsequent use when the MATTR is computed.
        /// Positions near the beginning are left null.
        /// </summary>
        //public static double[] WindowTTRs;


        /// <summary>
        /// Moving average type token ratio.  Computes and returns the MATTR for 
        /// the whole text and also stuffs individual window TTRs into the
        /// array WindowTTRs for possible use later.
        /// </summary>
        /// <param name="words">A list of words</param>
        /// <param name="w">Window size for moving average</param>
        /// <returns>A double representing the moving average TTR</returns>
        public static double MATTR(string text, int w)
        {
            List<string> tokens = Tokenize(text);
            return MATTR(tokens, w);
        }


        /// <summary>
        /// Moving average type token ratio.  Computes and returns the MATTR for 
        /// the whole text and also stuffs individual window TTRs into the
        /// array WindowTTRs for possible use later.
        /// </summary>
        /// <param name="words">A list of words</param>
        /// <param name="w">Window size for moving average</param>
        /// <returns>A double representing the moving average TTR</returns>
        public static double MATTR(List<string> words, int w)
        {

            double[] WindowTTRs = new double[words.Count];

          
            //if window is greater than count of words, return ttr
            if (w > words.Count)
            {
                return TypeTokenRatio(words);
            }

            Queue<string> window = new Queue<string>();
            //first queue up the first w words in words
            //i will be the indexer through the whole process
            int i = 0;
            for (; i < w; i++)
            {
                window.Enqueue(words[i]);
            }

            //get avg for queue
            double movingAvg = TypeTokenRatio(window.ToArray());

            //get frequency table for queue
            Dictionary<string, int> freqs = WordFrequency(window.ToArray());

            int j = words.Count;

            //until you get to the last index
            while (i < j)
            {
                //dequeue the first word
                string firstWord = window.Dequeue();

                //check the freq of this word
                int firstWordFreq = freqs[firstWord];

                //if frequency is 1, remove it from freqs
                if (firstWordFreq == 1)
                {
                    freqs.Remove(firstWord);
                }
                //otherwise, subtract 1 from its frequency
                else
                {
                    freqs[firstWord]--;
                }

                //get the next word fromt the word list
                string nextWord = words[i];

                //see if next word is in the freq table
                //if it is...
                if (freqs.ContainsKey(nextWord))
                {
                    //add one to its count...
                    freqs[nextWord]++;
                }
                else//...
                {
                    //add it to the table
                    freqs.Add(nextWord, 1);
                }

                //enqueue the next word
                window.Enqueue(nextWord);

                //ttr for this window is number of entries in the freq table
                //divided by the window size

                WindowTTRs[i] = (double)freqs.Count / w;


                //add window ttr to moving avg
                movingAvg += (double)WindowTTRs[i];

                //iterate
                i++;
            }

            //avg the moving avg
            //should divide movingAvg by number of
            //iterations (i - w + 1)
            movingAvg /= (i - w + 1);

            return movingAvg;

        }

    }
}
