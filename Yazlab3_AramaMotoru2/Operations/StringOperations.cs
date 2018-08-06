using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yazlab3_AramaMotoru2.Operations
{
    public class StringOperations
    {
        public static List<string> GetListBySplit(string source, char split)
        {
            List<string> list = new List<string>();
            string[] words = source.Split(split);
            foreach (string word in words)
                list.Add(word);
            return list;
        }
        public static int GetCountWordInSentence(string html, string word)
        {
            if (word == null || word == "")
                return 0;
            int result = 0;
            //word = String.Format("{0} ", word);
            //int startLength = html.Replace(word, "").Length;
            //int endLength = html.Length;
            //result = (endLength - startLength) / (word.Length);
            string[] wordList = html.Split(' ');
            foreach(string item in wordList)
            {
                if (item == word)
                    result++;
            }
            return result;
        }
        public static int GetCountWordInSentence(string source, List<string> words)
        {
            if (words == null)
                return 0;
            int sourceLength = 1, targetLength = 1, result = 0;
            sourceLength = source.Length;
            foreach(string word in words)
            {
                targetLength = source.Replace(word, null).Length;
                result += (sourceLength - targetLength) / (word.Length);
            }
            return result;
        }
        public static string GetLowerEnglishCompatible(string source)
        {
            char[] sourceLetter = { 'ğ', 'ü', 'ı', 'ş', 'ç', 'ö' };
            char[] targetLetter = { 'g', 'u', 'i', 's', 'c', 'o' };
            source = source.ToLower();
            for (int i = 0; i < sourceLetter.Length; i++)
            {
                source = source.Replace(sourceLetter[i], targetLetter[i]);
            }
            return source;
        }
        public static string GetUpperEnglishCompatible(string source)
        {
            char[] sourceLetter = { 'Ğ', 'Ü', 'İ', 'Ş', 'Ç', 'Ö' };
            char[] targetLetter = { 'G', 'U', 'I', 'S', 'C', 'O' };
            source = source.ToUpper();
            for (int i = 0; i < sourceLetter.Length; i++)
            {
                source = source.Replace(sourceLetter[i], targetLetter[i]);
            }
            return source;
        }
        public static string GetLowerTurkishCompatible(string source)
        {
            char[] sourceLetter = { 'g', 'u', 'i', 's', 'c', 'o' };
            char[] targetLetter = { 'ğ', 'ü', 'ı', 'ş', 'ç', 'ö' };
            source = source.ToLower();
            for (int i = 0; i < sourceLetter.Length; i++)
            {
                source = source.Replace(sourceLetter[i], targetLetter[i]);
            }
            return source;
        }
        public static string GetUpperTurkishCompatible(string source)
        {
            char[] sourceLetter = { 'G', 'U', 'I', 'S', 'C', 'O' };
            char[] targetLetter = { 'Ğ', 'Ü', 'İ', 'Ş', 'Ç', 'Ö' };
            source = source.ToUpper();
            for (int i = 0; i < sourceLetter.Length; i++)
            {
                source = source.Replace(sourceLetter[i], targetLetter[i]);
            }
            return source;
        }
        public static List<string> GetDifferentWords(List<string> entity)
        {
            List<string> different = new List<string>();
            foreach (string item in entity)
            {
                if (!different.Contains(item))
                    different.Add(item);
            }
            return different;
        }
        public static List<string> GetDifferentWords(List<string> entitySource, List<string> entityAdditional)
        {
            List<string> different = new List<string>();
            foreach (string item in entityAdditional)
            {
                if (!entitySource.Contains(item))
                    different.Add(item);
            }
            return different;
        }
        public static List<string> GetLanguageCompatible(string word)
        {
            List<string> entity = new List<string>();
            entity.Add(word.ToLower());
            entity.Add(word.ToUpper());
            entity.Add(GetLowerEnglishCompatible(word.ToLower()));
            entity.Add(GetUpperEnglishCompatible(word.ToUpper()));
            entity.Add(GetLowerTurkishCompatible(word.ToLower()));
            entity.Add(GetUpperTurkishCompatible(word.ToUpper()));
            return entity;
        }
        public static List<string> GetLanguageLowerCompatible(string word)
        {
            List<string> entity = new List<string>();
            entity.Add(word.ToLower());
            entity.Add(GetLowerEnglishCompatible(word.ToLower()));
            entity.Add(GetLowerTurkishCompatible(word.ToLower()));
            return entity;
        }
    }
}