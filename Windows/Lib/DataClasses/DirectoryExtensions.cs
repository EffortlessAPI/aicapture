using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading;

public static class DirectoryExtensions
{
    public static async Task ApplySeedReplacementsAsync(this DirectoryInfo rootPath, bool reverseApply = false)
    {
        try
        {
            // Load replacements from the seed file
            var replacements = await LoadReplacementsAsync(rootPath, reverseApply);
            if (replacements == null) return;

            if (!RequiresReplacements(rootPath, reverseApply)) return;

            //Console.WriteLine("Needs replacements");

            // Apply replacements recursively to all files
            foreach (JObject replacement in replacements)
            {
                if (reverseApply)
                {
                    //Console.WriteLine($"REVERSE-REPLACING: {replacement.ToString()}");
                    var findText = $"{replacement["default-replacement-text"]}";
                    var replaceText = $"{replacement["find-text"]}";
                    await ReplaceInFiles(rootPath, findText, replaceText);
                }
                else
                {
                    //Console.WriteLine($"REPLACING: {replacement.ToString()}");
                    var findText = $"{replacement["find-text"]}";
                    var replaceText = $"{replacement["replacement-text"]}";
                    await ReplaceInFiles(rootPath, findText, replaceText);
                }
            }
        }
        finally
        {
            ToggleRequiresReplacements(rootPath, reverseApply);
        }
    }

    private static async Task<JArray> LoadReplacementsAsync(DirectoryInfo rootPath, bool reverseUpdate)
    {
        var seedFilePath = Path.Combine(rootPath.FullName, "ssotme-seed.json");
        if (!File.Exists(seedFilePath)) return null;

        var json = File.ReadAllText(seedFilePath);
        JObject seedDetails = JObject.Parse(json);
        await UpdateReplacementTexts(seedFilePath, seedDetails, reverseUpdate);

        JArray replacements = (JArray)seedDetails["replacements"];

        return replacements;
    }

    private static async Task UpdateReplacementTexts(string seedFilePath, JObject seedDetails, bool reverseUpdate)
    {
        bool changed = false;
        JArray replacements = seedDetails["replacements"] as JArray;
        foreach (JObject replacement in replacements)
        {
            var findText = $"{replacement["find-text"]}";
            var defaultReplacementText = $"{replacement["default-replacement-text"]}";
            var replacementText = $"{replacement["replacement-text"]}";

            // Check if replacement-text is empty and offer to use default-replacement-text
            if (String.IsNullOrEmpty(replacementText) || String.Equals(replacementText, findText, StringComparison.OrdinalIgnoreCase))
            {
                if (!reverseUpdate)
                {
                    Console.WriteLine($"{replacement["description"]}\n'{replacement["find-text"]}' is '{defaultReplacementText}'.\nEnter new text or press ENTER to use the default:");
                    var userInput = Console.ReadLine();
                    if (String.IsNullOrEmpty(userInput))
                    {
                        replacement["replacement-text"] = defaultReplacementText;
                    }
                    else
                    {
                        replacement["replacement-text"] = userInput;
                    }
                    changed = true;
                }
            } else if (reverseUpdate) 
            {
                replacement["replacement-text"] = replacement["find-text"];
                changed = true;
            }
        }

        if (changed)
        {
            // Save updated JSON back to file
            seedDetails["requires-replacements"] = true;
            File.WriteAllText(seedFilePath, seedDetails.ToString());
        }
    }

    private static async Task ReplaceInFiles(DirectoryInfo rootPath, string findText, string replaceText)
    {
        foreach (var file in rootPath.GetFiles("*", SearchOption.AllDirectories))
        {
            if (file.Name == "ssotme-seed.json") continue;
            var content = File.ReadAllText(file.FullName);
            var newContent = Regex.Replace(content, Regex.Escape(findText), replaceText);

            if (newContent != content)
            {
                File.WriteAllText(file.FullName, newContent);
            }
        }
    }

    private static bool RequiresReplacements(DirectoryInfo rootPath, bool reverseApply)
    {
        var seedFilePath = Path.Combine(rootPath.FullName, "ssotme-seed.json");
        if (!File.Exists(seedFilePath)) return false;
        var json = File.ReadAllText(seedFilePath);
        var seedDetails = JObject.Parse(json);

        // Toggle the requires-replacements based on the reverseApply parameter
        bool currentlyRequiresReplacement = (bool)seedDetails["requires-replacements"];
        var requiresReplacement = currentlyRequiresReplacement || reverseApply;
        Console.WriteLine($"REPLACEMENT JUDGEMENT: json.Requires Replacement = {currentlyRequiresReplacement} and {(reverseApply ? "is a reverse replace" : "")}");
        return requiresReplacement;
    }


    private static void ToggleRequiresReplacements(DirectoryInfo rootPath, bool reverseApply)
    {
        var seedFilePath = Path.Combine(rootPath.FullName, "ssotme-seed.json");
        if (!File.Exists(seedFilePath)) return;
        var json = File.ReadAllText(seedFilePath);
        var seedDetails = JObject.Parse(json);

        // Toggle the requires-replacements based on the reverseApply parameter
        bool requiresReplacement = (bool)seedDetails["requires-replacements"];
        if (requiresReplacement == reverseApply) return;
        seedDetails["requires-replacements"] = reverseApply;
        File.WriteAllText(seedFilePath, $"{seedDetails}");
    }
}
