using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

[System.Serializable    ]
public class PseudoLanguage
{
    private System.Random random = new System.Random(); // System.Random instance
    
    public string[] vowels;
    public string[] consonants;
    public string[] prefixes;
    public string[] suffixes;

    public PseudoLanguage() : this(null, null, null, null)
    { }

    // Constructor that accepts custom values
    public PseudoLanguage(IEnumerable<string> customVowels = null, IEnumerable<string> customConsonants = null, IEnumerable<string> customPrefixes = null, IEnumerable<string> customSuffixes = null)
    {
        Populate(customVowels, customConsonants, customPrefixes, customSuffixes);
    }

    public void Populate(IEnumerable<string> customVowels = null, IEnumerable<string> customConsonants = null, IEnumerable<string> customPrefixes = null, IEnumerable<string> customSuffixes = null)
    {
        vowels =
            customVowels != null && customVowels.Any()
            ? customVowels.ToArray()
            : new string[] { "a", "e", "i", "o", "u", "ai", "eo", "ou" };
        consonants = customConsonants != null && customConsonants.Any()
            ? customConsonants.ToArray()
            : new string[] { "r", "l", "n", "st", "nd", "m", "p", "b" };
        prefixes = customPrefixes != null && customPrefixes.Any()
            ? customPrefixes.ToArray()
            : new string[] { "pre", "sub", "re", "un", "dis", "pro" };
        suffixes = customSuffixes != null && customSuffixes.Any()
            ? customSuffixes.ToArray()
            : new string[] { "ment", "tion", "ance", "able", "ium", "ous" };

    }
    public string Word(int length)
    {
        int wordLength = length; // Control the complexity of the word
        string word = GetRandomElement(prefixes, true);

        for (int i = 0; i < wordLength; i++)
        {
            word += GetSyllable();
        }

        word += GetRandomElement(suffixes, true);
        return word;
    }
    
    public string Sentence(int wordCount, string splitter = " ")
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < wordCount; i++)
        {
            int rand = random.Next(1, 6);
            string word = Word(rand);
            
            if(word.Length <= 1)
            {
                word = vowels[random.Next(0, vowels.Length)];
            }
            if (i > 0)
            {
                builder.Append(splitter);
            }
            builder.Append(word);
        }

        return builder.ToString();
    }


    public string FixedLengthSentence(int maxLength, string splitter = " ")
    {
        StringBuilder builder = new StringBuilder();

        bool init = true;
        while(builder.Length < maxLength)
        {
            int rand = random.Next(1, 12);
            string word = Word(rand);

            if (word.Length <= 1)
            {
                word = vowels[random.Next(0, vowels.Length)];
            }
            if (init)
            {
                init = false;
            }
            else
            {
                builder.Append(splitter);
            }

            builder.Append(word);

            if (builder.Length >= maxLength) break;
        }

        return builder.ToString().Substring(0, maxLength);
    }

    public void RemoveDuplicates()
    {
        vowels = RemoveDuplicatesFromArray(vowels);
        consonants = RemoveDuplicatesFromArray(consonants);
        prefixes = RemoveDuplicatesFromArray(prefixes);
        suffixes = RemoveDuplicatesFromArray(suffixes);
    }

    private string[] RemoveDuplicatesFromArray(string[] array)
    {
        HashSet<string> uniqueSet = new HashSet<string>(array);
        return uniqueSet.ToArray();
    }

    public string GetSyllable()
    {
        int syllableType = random.Next(0, 3);
        switch (syllableType)
        {
            case 0: // CV
                return GetRandomElement(vowels) + GetRandomElement(consonants);
            case 1: // VC
                return GetRandomElement(consonants) + GetRandomElement(vowels);
            case 2: // CVC
                return GetRandomElement(consonants) + GetRandomElement(vowels) + GetRandomElement(consonants);
            default:
                return "";
        }
    }

    public string GetRandomElement(string[] elements, bool optional = false)
    {
        if (optional && random.Next(0, 2) == 0) return "";
        return elements[random.Next(0, elements.Length)];
    }

    public void German()
    {
        Populate(
            new string[] { "a", "e", "i", "o", "u", "ä", "ö", "ü", "ei", "au", "ie" },
            new string[] { "r", "l", "n", "st", "nd", "m", "p", "b", "sch", "ch", "z", "v", "k", "g" },
            new string[] { "ver", "ge", "be", "ent", "um", "aus" },
            new string[] { "heit", "ung", "lich", "bar" }
        );
    }

    public void Swedish()
    {
        Populate(
            new string[] { "a", "e", "i", "o", "u", "y", "å", "ä", "ö", "ei", "au", "öi" },
            new string[] { "r", "l", "n", "st", "nd", "m", "p", "b", "sk", "g", "v", "j", "k", "tj", "gn" },
            new string[] { "av", "be", "för", "an", "över", "under" },
            new string[] { "skap", "ling", "sam" }
        );
    }

    public void French()
    {
        Populate(
            new string[] { "a", "e", "i", "o", "u", "é", "è", "ê", "û", "ô" },
            new string[] { "r", "l", "n", "s", "t", "d", "p", "b", "v", "f", "g" },
            new string[] { "re", "con", "en", "de", "pro" },
            new string[] { "tion", "ment", "age", "ance", "ique" }
        );
    }

    public void Russian()
    {
        Populate(
            new string[] { "а", "е", "и", "о", "у", "ы", "э", "ю", "я" },
            new string[] { "р", "л", "н", "с", "т", "д", "п", "б", "в", "ф", "г", "к", "ч", "ш", "ж" },
            new string[] { "вз", "раз", "недо", "пере", "без" },
            new string[] { "ость", "ение", "ник", "тель" }
        );
    }
    public void Polish()
    {
        Populate(
            new string[] { "a", "e", "i", "o", "u", "ą", "ę", "y" },
            new string[] { "r", "l", "n", "s", "t", "d", "p", "b", "v", "f", "g", "k", "z", "ś", "ć", "ż", "ź", "ł" },
            new string[] { "prze", "nad", "pod", "bez", "wz" },
            new string[] { "ność", "anie", "enie", "twa" }
        );
    }

    public void LoremIpsum()
    {
        Populate(
            new string[] { "a", "e", "i", "o", "u", "ae", "oe", "au", "eu" },
            new string[] { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "qu", "r", "s", "t", "v", "x", "z" },
            new string[] { "ex", "in", "sub", "re", "pro", "ad", "ips", "ups" },
            new string[] { "us", "um", "is", "or", "er", "as", "es" }
        );
    }
    
    public void EuropeanNames()
    {
        vowels = new string[] { "a", "e", "i", "o", "u", "y", "ae", "ie", "ei", "ou", "eu", "ia", "ai" };
        consonants = new string[] { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "r", "s", "t", "v", "w", "z", "st", "sp", "sk", "nd", "nt", "rt", "lt", "rn" };
        suffixes = new string[] { "son", "sen", "ov", "ski", "vich", "stein", "ez", "as", "os", "us", "io", "ar", "er", "ova", "ska", "a", "e", "ia", "ina", "ara", "ora", "essa", "elle" };
    }
}
public static class Randomizer
{
    private static PseudoLanguage language = new PseudoLanguage();
    private static System.Random random = new System.Random(); // System.Random instance


    public static Color Color(float min = 0.0f, float max = 1.0f, float alpha = 1f)
    {
        float r = min + (float)random.NextDouble() * (max - min);
        float g = min + (float)random.NextDouble() * (max - min);
        float b = min + (float)random.NextDouble() * (max - min);

        return new Color(r, g, b, alpha);
    }
    public static string String(int length, string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
    {
        char[] stringChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
    public static string PhoneNumber()
    {
        return $"+{random.Next(0, 10)}{random.Next(5, 16)}";
    }

    public static string Email()
    {
        string name = String(random.Next(4, 16));
        string provider = String(random.Next(4, 12), "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        string country = String(random.Next(2, 4), "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        return $"{name}@{provider}.{country}";
    }

    public static string Website()
    {
        string provider = String(random.Next(4, 19), "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        string country = String(random.Next(2, 4), "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        return $"https://www.{provider}.{country}";
    }
    public static string Name()
    {
        PseudoLanguage baselanguage = new PseudoLanguage();

        baselanguage.EuropeanNames();

        return baselanguage.Word(random.Next(4, 12));
    }

    public static string Word()
    {
        return language.Word(random.Next(4, 12)); // Use System.Random here
    }

    public static string Word(int length)
    {
        return language.Word(length);
    }

    public static string Word(int min, int max)
    {
        return language.Word(random.Next(min, max)); // Use System.Random here
    }

    public static string Sentence(int min, int max)
    {
        return Sentence(random.Next(min, max)); // Use System.Random here
    }
    public static string Sentence(int words)
    {
        return language.Sentence(words);
    }
    public static bool Bool(float probability = 0.5f)
    {
        return random.NextDouble() > probability; // Use System.Random here
    }

    public static string GenerateWord(int length)
    {
        return language.Word(length);
    }

    public static ETargetPlayer ETargetPlayer()
    {
        // Generate a random value within the range of the enum
        int randomValue = random.Next(0, 6); // Use System.Random here

        // Cast the random integer to the ETargetPlayer enum
        return (ETargetPlayer)randomValue;
    }
}