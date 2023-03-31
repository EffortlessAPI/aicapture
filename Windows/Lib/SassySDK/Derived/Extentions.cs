using System;
using SassyMQ.Lib.RabbitMQ.Payload;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Collections.Generic;
using RabbitMQ.Client.Events;
using SassyMQ.SSOTME.Lib.RabbitMQ;
using System.Globalization;
using SSoTme.OST.Lib.Extensions;

namespace SassyMQ.Lib.RabbitMQ
{
    public static class ExtensionMethods
    {

        public static bool IsPing(this BasicDeliverEventArgs bdea)
        {
            return (bdea.RoutingKey.SafeToString().ToLower().Contains(".ping"));
        }

        public static String SafeToString(this object value)
        {
            if (ReferenceEquals(value, null)) return String.Empty;
            else return value.ToString();
        }

        public static String ToPropperCase(this String value)
        {
            value = value.SafeToString();
            if (value.Length <= 1) return value.ToUpper();
            else return String.Join("", value.Substring(0, 1).ToUpper(), value.Substring(1));
        }

        public static String Pluralize(this String singlularText)
        {
            if (String.IsNullOrEmpty(singlularText)) return String.Empty;
            return SSOTMEExtensions.Pluralizer.Pluralize(singlularText.SafeToString());
        }

        public static bool IsSingular(this String singularCandidate)
        {
            if (String.IsNullOrEmpty(singularCandidate)) return false;
            return SSOTMEExtensions.Pluralizer.IsSingular(singularCandidate.SafeToString());
        }

        public static bool IsPlural(this String pluralCandidate) {
            return !pluralCandidate.IsSingular();
        }

        public static String Singuluralize(this String pluralText)
        {
            if (String.IsNullOrEmpty(pluralText)) return String.Empty;
            return SSOTMEExtensions.Pluralizer.Singularize(pluralText.SafeToString());
        }

        //public static void HandleInvoke<T>(this Form form, object sender, InvokeEventArgs<T> e)
        //    where T : StandardPayload<T>, new()
        //{
        //    if (ReferenceEquals(form, null)) e.MethodDelegate.Invoke(sender, e.PayloadEventArgs);
        //    else form.Invoke(new EventHandler<PayloadEventArgs<T>>(e.MethodDelegate), sender, e.PayloadEventArgs);
        //}

        public static String GuidToKey(this Guid guid)
        {
            return guid.ToString().ToLower().Trim("{}".ToCharArray()).Replace("-", "");
        }

        public static String GuidToLongKey(this Guid guid)
        {
            return String.Format("{0}{1}", guid.GuidToKey(), Guid.NewGuid().GuidToKey());
        }

        public static String HashPrivateKey(this String privateKey)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(privateKey);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public static List<String> GetAllFiles(this DirectoryInfo di)
        {
            List<String> files = new List<String>();

            foreach (var f in di.GetFiles())
            {
                files.Add(f.FullName);
            }
            foreach (var d in di.GetDirectories())
            {
                files.AddRange(d.GetAllFiles());
            }

            return files;
        }

        public static bool IsLexiconTerm<T>(this StandardPayload<T> payload, LexiconTermEnum termKey)
            where T : StandardPayload<T>, new()
        {
            LexiconTerm term = Lexicon.Terms[termKey];
            return (payload.RoutingKey == term.RoutingKey);
        }

        //public static bool IsReadOnlyFileSystem(this string path)
        //{
        //    try
        //    {
        //        using (var testFile = File.Create(Path.Combine(path, "test.tmp")))
        //        {
        //            // Do nothing, just testing
        //        }
        //        File.Delete(Path.Combine(path, "test.tmp"));
        //        return false;
        //    }
        //    catch (Exception ex) // Catch other exceptions and log them
        //    {
        //        Console.WriteLine($"Unexpected exception in IsReadOnlyFileSystem: {ex}");
        //        var buildDir = $"/tmp/{Guid.NewGuid()}";
        //        if (!Directory.Exists(buildDir))
        //        {
        //            Directory.CreateDirectory(buildDir);
        //        }
        //        var aicaptureJson = "{ }";
        //        File.WriteAllText(Path.Combine(buildDir, "aicapture.json"), aicaptureJson);
        //        Environment.CurrentDirectory = buildDir;
        //        return true; // Assume read-only in case of unexpected exceptions
        //    }
        //}
    }
}