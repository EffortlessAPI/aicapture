using Amazon.Lambda.Core;
using System.Diagnostics;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambda1;

public class Function
{
    
    /// <summary>
    /// A simple function that takes a string and returns both the upper and lower case version of the string.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public string FunctionHandler(string input, ILambdaContext context)
    {
        if (input == null || input == "")
        {
            input = "-help";
        }

        //var npmGlobalLocation = GetNpmGlobalLocation();

        var startInfo = new ProcessStartInfo
        {
            FileName = "node",
            Arguments = $"./aicapture/cli.js {input}", // Update the path according to the package
            RedirectStandardOutput = true,
            RedirectStandardInput = true,
            //RedirectStandardError = true,
            //UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = startInfo };
        process.Start();
        string result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        if (!process.HasExited && process.StandardInput.BaseStream.CanWrite)
        {
            process.StandardInput.WriteLine("a");
            process.StandardInput.Flush();
        }
        return result;
    }
}
