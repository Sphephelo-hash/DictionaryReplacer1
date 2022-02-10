using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryReplacer
{
    public class DictionaryReplacer
    {
        public string Replace(string sentence, Dictionary<string,string> dictionary)
        {
            if(string.IsNullOrEmpty(sentence) || dictionary.Equals(null) ){
                return string.Empty;
            }            
            return ReplaceWords(sentence,dictionary);
        }

        public string ReplaceWords(string sentence, Dictionary<string, string> dictionary)
        {
            string[] wordList = sentence.Split(' ');
            List<string> keys = GetKeys(wordList);
            List<string> values = FindValuesInDictionary(dictionary, keys);
            string replacedWords = ReplaceDictionary(wordList, values);
            return replacedWords;
        }

        public string TrimDollarSigns(string word)
        {            
            return word.Replace('$', ' ').Trim(); 
        }

        public List<string> GetKeys(string[] wordList)
        {
            List<string> keys = new List<string>();
            foreach(string word in wordList)
            {
                if (word.Contains("$"))
                {
                    keys.Add(TrimDollarSigns(word).Trim());
                }
            }
            return keys;
         }

        public List<string> FindValuesInDictionary(Dictionary<string,string> dictionary, List<string> keys)
        {
            List<string> valuesFound = new List<string>();
            foreach (string key in keys)
            {
                valuesFound.Add(dictionary[key]);
            }
            return valuesFound;
        }

        public string ReplaceDictionary(string[]wordList, List<string>valuesFound)
        {
            int countValues =0;
            string ReplacedWords = "";
            for(int i=0;i<wordList.Length;i++)
            {
                if (wordList[i].Contains("$"))
                {
                    ReplacedWords += " "+valuesFound[countValues];
                    countValues++;
                }
                else
                {
                   ReplacedWords += " "+ wordList[i];
                }
            }
            return ReplacedWords.Trim();
        }
    }
}