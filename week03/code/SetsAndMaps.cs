using System.Diagnostics;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE

        // Create a set to store the words
        var wordSet = new HashSet<string>();
        var result = new List<string>();
        // Loop through the words
        foreach (var word in words)
        {
            // add word to set
            wordSet.Add(word);
            // If the reverse of the word is in the set, then add the word to the result
            if (wordSet.Contains(Reverse(word)) && Reverse(word) != word)
            {
                // Add the word to the result
                result.Add($"{word} & {Reverse(word)}");
            }
        }
        // Return the result
        return [.. result];
    }

    /// <summary>
    /// Reverses the given string.
    /// </summary>
    /// <param name="word">The string to reverse</param>
    /// <returns>The reversed string</returns>
    private static string Reverse(string word)
    {
        char[] charArray = word.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE
            // add the degree to the dictionary witha count of occurences
            if (degrees.ContainsKey(fields[3]))
            {
                degrees[fields[3]]++;
            }
            else
            {
                degrees.Add(fields[3], 1);
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Normalize both strings: lowercase and remove spaces
        string normalized1 = word1.ToLower().Replace(" ", "");
        string normalized2 = word2.ToLower().Replace(" ", "");

        // Early exit if lengths differ (anagrams must be same length)
        if (normalized1.Length != normalized2.Length)
            return false;

        var letterCounts = new Dictionary<char, int>();

        // Count characters in first word
        foreach (var letter in normalized1)
        {
            letterCounts[letter] = letterCounts.GetValueOrDefault(letter, 0) + 1;
        }

        // Subtract character counts based on second word
        foreach (var letter in normalized2)
        {
            if (!letterCounts.TryGetValue(letter, out int count) || count == 0)
                return false;

            letterCounts[letter] = count - 1;
        }

        // No need to check values—all should be zero if same length and above passed
        return true;
    }


    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // create a string outputting each place a earthquake has happened today and its magnitude and return the list of strings
        if (featureCollection != null)
        {
            return featureCollection.Features.Select(feature => $"{feature.Properties.Place} - Mag {feature.Properties.Mag}").ToArray();
        }

        return ["something broke!"];
    }
}